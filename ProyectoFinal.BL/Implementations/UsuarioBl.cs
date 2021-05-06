using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ProyectoFinal.BL.Contracts;
using ProyectoFinal.Core.DTO;
using ProyectoFinal.DAL.Models;
using ProyectoFinal.DAL.Repositories.Contracts;

namespace ProyectoFinal.BL.Implementations
{
    public class UsuarioBl : IUsuarioBl
    { 
        private readonly IRepository<Usuario> _repository;
        private readonly IRepositoryAuth<Usuario> _repositoryAuth;
        private readonly IMapper _mapper;

        public UsuarioBl(IRepository<Usuario> repository, IRepositoryAuth<Usuario> repositoryAuth, IMapper mapper)
        {
            _repository = repository;
            _repositoryAuth = repositoryAuth;
            
            _mapper = mapper;
        }

        public async Task<UsuarioDetallesDto> Login(UsuarioLoginDto login)
        {
            var entity = _mapper.Map<Usuario>(login);
            
            entity = await _repositoryAuth.Login(entity);
            return _mapper.Map<UsuarioDetallesDto>(entity);
        }
        
        public async Task<UsuarioDetallesDto> Create(UsuarioCreateDto gimnasio)
        {
            var entity = _mapper.Map<Usuario>(gimnasio);
            
            // Comprobamos si existe el email/login antes de añadirlo
            var existencia = await _repositoryAuth.CheckExistence(entity);
            if (existencia != 0) return null;
            
            entity = await _repository.Create(entity);
            return _mapper.Map<UsuarioDetallesDto>(entity);
        }

        public async Task<IEnumerable<UsuarioListaDto>> GetAll()
        {
            var list = await _repository.GetAll();
            return _mapper.Map<IEnumerable<UsuarioListaDto>>(list);
        }
        
        public async Task<UsuarioDetallesDto> GetById(Guid id)
        {
            var entity = await _repository.GetById(id);
            return _mapper.Map<UsuarioDetallesDto>(entity);
        }
        
        public async Task<bool> Update(Guid id, UsuarioUpdateDto dto)
        {
            try
            {
                var entity = _mapper.Map<Usuario>(dto);
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
                return await _repository.Delete(new Usuario {Id = id});
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Guid> GetIdByAuthId(Guid authId)
        {
            return (await _repository.GetByCondition(usuario => usuario.AuthId == authId)).Id;
        }
    }
}