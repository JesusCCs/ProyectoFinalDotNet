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
    public class AnuncioBl : IAnuncioBl
    {
        private readonly IMapper _mapper;
        private readonly FileManager _fileManager;
        private readonly IRepository<Anuncio> _repository;

        public AnuncioBl(IMapper mapper, FileManager fileManager, IRepository<Anuncio> repository)
        {
            _mapper = mapper;
            _fileManager = fileManager;
            _repository = repository;
        }
        
        public async Task<AnuncioDetallesResponse> Create(AnuncioCreateRequest request)
        {
            var anuncioInfo = _mapper.Map<Anuncio>(request);
            
            var anuncio = await _repository.Create(anuncioInfo);
            
            anuncio.Recurso = await _fileManager.Upload(request.Recurso, FileType.Anuncio);
            await _repository.Update(anuncio);
            
            return _mapper.Map<AnuncioDetallesResponse>(anuncio);
        }

        public async Task<IEnumerable<AnuncioDatesResponse>> GetDates()
        {
            var list = await _repository.GetAll();
            return _mapper.Map<IEnumerable<AnuncioDatesResponse>>(list);
        }

        public async Task<AnuncioDetallesResponse> GetById(Guid id)
        {
            var entity = await _repository.GetById(id);
            return _mapper.Map<AnuncioDetallesResponse>(entity);
        }

        public async Task<IEnumerable<AnunciosGimnasioResponse>> GetAllFrom(Guid id)
        {
            var lista = await _repository.GetByCondition(anuncio => anuncio.GimnasioId == id);
            return _mapper.Map<IEnumerable<AnunciosGimnasioResponse>>(lista);
        }
    }
}