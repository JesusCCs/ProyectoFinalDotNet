using System;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProyectoFinal.BL.Contracts;
using ProyectoFinal.Core.DTO;
using ProyectoFinal.DAL.Models;
using ProyectoFinal.DAL.Models.Auth;
using ProyectoFinal.DAL.Repositories.Contracts;

namespace ProyectoFinal.BL.Implementations
{
    public class GinmasioBl : IGinmasioBl
    {
        private readonly IRepository<Gimnasio> _repository;
        private readonly IMapper _mapper;

        public GinmasioBl(IRepository<Gimnasio> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GimnasioDetallesDto> Create(GimnasioCreateDto gimnasioCreate, Guid authId)
        {
            var gimnasioInfo = _mapper.Map<Gimnasio>(gimnasioCreate);
            gimnasioInfo.AuthId = authId;

            var gimnasio = await _repository.Create(gimnasioInfo);
            
            return _mapper.Map<GimnasioDetallesDto>(gimnasio);
        }

        public async Task<IEnumerable<GimnasioListaDto>> GetAll()
        {
            var list = await _repository.GetAll();
            return _mapper.Map<IEnumerable<GimnasioListaDto>>(list);
        }

        public async Task<GimnasioDetallesDto> GetById(Guid id)
        {
            var entity = await _repository.GetById(id);
            return _mapper.Map<GimnasioDetallesDto>(entity);
        }
        
        public async Task<GimnasioDetallesDto> GetByAuthId(Guid? guidAuth)
        {
            var entity = await _repository.GetByCondition(gimnasio => gimnasio.AuthId == guidAuth);
            return _mapper.Map<GimnasioDetallesDto>(entity);
        }

        public async Task<bool> Update(Guid id, GimnasioUpdateDto gimnasio)
        {
            try
            {
                var entity = _mapper.Map<Gimnasio>(gimnasio);
                return await _repository.Update(entity);
            }
            catch (Exception)
            {
                return false;
            }
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