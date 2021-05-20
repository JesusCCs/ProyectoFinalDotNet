using System;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoFinal.BL.Contracts;
using ProyectoFinal.BL.Helpers;
using ProyectoFinal.Core;
using ProyectoFinal.Core.DTO;
using ProyectoFinal.Core.Exceptions;
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
            
            // Guardamos la tarifa en céntimos (la cantidad que llega es en €)
            gimnasioInfo.Tarifa *= 100;
            
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
            var list = await _repository.GetAll();
            return _mapper.Map<IEnumerable<GimnasioGetAllResponse>>(list);
        }

        public async Task<GimnasioGetByIdResponse> GetById(Guid id)
        {
            var entity = await _repository.GetById(id);
            return _mapper.Map<GimnasioGetByIdResponse>(entity);
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
            var gimnasio = await _repository.GetById(request.Id);
            var gimnasioModificado = _mapper.Map<Gimnasio>(request);
            
            if (request.Logo != null && !request.DeleteLogo)
            {
                if (gimnasio.Logo != null) _fileManager.Remove(gimnasio.Logo, FileType.Logo);
                await _fileManager.Upload(request.Logo, FileType.Logo);
            }
            
            var actualizacionExitosa = await _repository.Update(gimnasioModificado);
            if (request.DeleteLogo) _fileManager.Remove(gimnasio.Logo, FileType.Logo);

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