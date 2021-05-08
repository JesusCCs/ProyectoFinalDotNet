using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ProyectoFinal.BL.Contracts;
using ProyectoFinal.BL.Helpers;
using ProyectoFinal.Core;
using ProyectoFinal.Core.DTO;
using ProyectoFinal.DAL.Models;
using ProyectoFinal.DAL.Repositories.Contracts;

namespace ProyectoFinal.BL.Implementations
{
    public class AnucioBl : IAnucioBl
    {
        private readonly IMapper _mapper;
        private readonly FileManager _fileManager;
        private readonly IRepository<Anuncio> _repository;

        public AnucioBl(IMapper mapper, FileManager fileManager, IRepository<Anuncio> repository)
        {
            _mapper = mapper;
            _fileManager = fileManager;
            _repository = repository;
        }
        
        public async Task<Anuncio> Create(AnuncioCreateRequest request)
        {
            var anuncioInfo = _mapper.Map<Anuncio>(request);
            
            var anuncio = await _repository.Create(anuncioInfo);
            
            anuncio.Recurso = await _fileManager.Upload(request.Recurso, FileType.Anuncio);
            await _repository.Update(anuncio);
            
            return anuncio;
        }

        public async Task<IEnumerable<AnuncioCheckAllResponse>> CheckDates()
        {
            var list = await _repository.GetAll();
            return _mapper.Map<IEnumerable<AnuncioCheckAllResponse>>(list);
        }

        public async Task<AnuncioGetByIdResponse> GetById(Guid id)
        {
            var entity = await _repository.GetById(id);
            return _mapper.Map<AnuncioGetByIdResponse>(entity);
        }
    }
}