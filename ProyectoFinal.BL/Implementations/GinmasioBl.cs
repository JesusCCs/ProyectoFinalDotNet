﻿using System;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoFinal.BL.Contracts;
using ProyectoFinal.Core;
using ProyectoFinal.Core.DTO;
using ProyectoFinal.Core.Exceptions;
using ProyectoFinal.Core.Helpers;
using ProyectoFinal.DAL.Models;
using ProyectoFinal.DAL.Repositories.Contracts;

namespace ProyectoFinal.BL.Implementations
{
    public class GinmasioBl : IGinmasioBl
    {
        private readonly IMapper _mapper;
        private readonly FileManager _fileManager;
        private readonly IRepository<Gimnasio> _repository;

        public GinmasioBl(IRepository<Gimnasio> repository, IMapper mapper, FileManager fileManager)
        {
            _mapper = mapper;
            _fileManager = fileManager;
            _repository = repository;
        }

        public async Task<Gimnasio> Create(GimnasioCreateRequest request, Guid authId)
        {
            var gimnasioInfo = _mapper.Map<Gimnasio>(request);
            gimnasioInfo.AuthId = authId;
            
            // Creamos identificador público
            var now = DateTime.Now;
            var zeroDate = DateTime.MinValue.AddHours(now.Hour).AddMinutes(now.Minute).AddSeconds(now.Second).AddMilliseconds(now.Millisecond);
            var identificador = "#" + (zeroDate.Ticks / 10000).ToString().PadLeft(9,'0');

            gimnasioInfo.Identificador = identificador;

            var gimnasio = await _repository.Create(gimnasioInfo);

            if (request.Logo is null) return gimnasio;
            
            // Si hay logo se tiene que subir y añadirlo al gimnasio
            var logo = await _fileManager.Upload(request.Logo, FileType.Logo);
            gimnasio.Logo = logo;
            await _repository.Update(gimnasio);
            
            return gimnasio;
        }

        public async Task<IEnumerable<GimnasioGetAllResponse>> GetAll()
        {
            var list = await _repository.GetAll(
                g => g.Activo.Value && g.Auth.EmailConfirmed, "", g => g.Nombre, "asc");
            
            foreach (var gimnasio in list)
            {
                gimnasio.Logo = _fileManager.Get(gimnasio.Logo, FileType.Logo, true);
            }
            
            return _mapper.Map<IEnumerable<GimnasioGetAllResponse>>(list);
        }

        public async Task<GimnasioGetByIdResponse> GetById(Guid id)
        {
            var entity = await _repository.GetById(id, "Auth");

            entity.Logo = _fileManager.Get(entity.Logo, FileType.Logo);
            
            return _mapper.Map<GimnasioGetByIdResponse>(entity);
        }
        
        public async Task<GimnasioMobileResponse> GetByIdMobile(Guid id)
        {
            var entity = await _repository.GetById(id);

            entity.Logo = _fileManager.Get(entity.Logo, FileType.Logo, true);
            
            return _mapper.Map<GimnasioMobileResponse>(entity);
        }

        public async Task<GimnasioGetByIdResponse> GetByAuthId(Guid guidAuth)
        {
            var entity = await _repository.GetByCondition(gimnasio => gimnasio.AuthId == guidAuth);
            return _mapper.Map<GimnasioGetByIdResponse>(entity);
        }

        public async Task<Guid> GetIdByAuthId(Guid guidAuth)
        {
            var entity = await _repository.GetByCondition(gimnasio => gimnasio.AuthId == guidAuth);
            return entity.Id;
        }

        public async Task Update(Guid id, GimnasioUpdateRequest request)
        {
            var gimnasio = await _repository.GetById(request.Id, "Auth");
            
            gimnasio = _mapper.Map(request, gimnasio);
            
            if (request.Logo != null)
            {
                if (gimnasio.Logo != null) _fileManager.Remove(gimnasio.Logo, FileType.Logo);
                var logo = await _fileManager.Upload(request.Logo, FileType.Logo);
                gimnasio.Logo = logo;
            }
            
            var actualizacionExitosa = await _repository.Update(gimnasio);

            if (!actualizacionExitosa) throw new UpdateFailedException();
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                return await _repository.Delete(new Gimnasio {Id = id});
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}