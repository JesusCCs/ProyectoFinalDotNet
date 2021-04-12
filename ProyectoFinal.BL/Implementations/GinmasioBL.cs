using System;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoFinal.BL.Contracts;
using ProyectoFinal.Core.DTO;
using ProyectoFinal.DAL.Models;
using ProyectoFinal.DAL.Repositories.Contracts;

namespace ProyectoFinal.BL.Implementations
{
    public class GinmasioBL : IGinmasioBL
    {
        private readonly IRepository<Gimnasio> _repository;
        private readonly IRepositoryAuth<Gimnasio> _repositoryAuth;
        private readonly IMapper _mapper;

        public GinmasioBL(IRepository<Gimnasio> repository, IRepositoryAuth<Gimnasio> repositoryAuth, IMapper mapper)
        {
            _repository = repository;
            _repositoryAuth = repositoryAuth;
            
            _mapper = mapper;
        }

        public async Task<GimnasioDetallesDto> Login(GimnasioLoginDto login)
        {
            var entity = _mapper.Map<Gimnasio>(login);
            
            entity = await _repositoryAuth.Login(entity);
            return _mapper.Map<GimnasioDetallesDto>(entity);
        }
        
        public async Task<GimnasioDetallesDto> Create(GimnasioCreateDto gimnasio)
        {
            var entity = _mapper.Map<Gimnasio>(gimnasio);
            
            // Comprobamos si existe el email/login antes de añadirlo
            var existencia = await _repositoryAuth.CheckExistence(entity);
            if (existencia != 0) return null;
            
            entity = await _repository.Create(entity);
            return _mapper.Map<GimnasioDetallesDto>(entity);
        }

        public async Task<IEnumerable<GimnasioListaDto>> GetAll()
        {
            var list = await _repository.GetAll();
            return _mapper.Map<IEnumerable<GimnasioListaDto>>(list);
        }
        
        public async Task<GimnasioDetallesDto> GetById(Guid id)
        {
            var entity = await _repository.Get(id);
            return _mapper.Map<GimnasioDetallesDto>(entity);
        }
        
        public async Task<bool> Update(Guid id, GimnasioUpdateDto gimnasio)
        {
            try
            {
                var entity = _mapper.Map<Gimnasio>(gimnasio);
                return await _repository.Update(entity);
            }
            catch (Exception _)
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
            catch (Exception _)
            {
                return false;
            }
        }
    }
}