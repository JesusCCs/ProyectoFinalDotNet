using System;
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
        private readonly IMapper _mapper;

        public UsuarioBl(IRepository<Usuario> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Create(UsuarioCreateRequest create, Guid authId)
        {
            var usuario = _mapper.Map<Usuario>(create);
            usuario.AuthId = authId;

            usuario = await _repository.Create(usuario);

            return usuario.Id;
        }

        public async Task<Guid> GetIdByAuthId(Guid authId)
        {
            return (await _repository.GetByCondition(usuario => usuario.AuthId == authId)).Id;
        }
    }
}