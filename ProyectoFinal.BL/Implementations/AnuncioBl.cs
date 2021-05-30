using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProyectoFinal.BL.Contracts;
using ProyectoFinal.Core;
using ProyectoFinal.Core.DTO;
using ProyectoFinal.Core.Helpers;
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

        public async Task<Guid> Create(AnuncioCreateRequest request)
        {
            var recurso = await _fileManager.Upload(request.Recurso, FileType.Anuncio);

            var anuncio = await _repository.Create(new Anuncio
            {
                GimnasioId = request.GimnasioId,
                Recurso = recurso,
                Tipo = request.Recurso.GetTipo()
            });

            return anuncio.Id;
        }

        public async Task<AnuncioBaseResponse> UpdateDetails(Guid id, AnuncioUpdateDetailsRequest request)
        {
            var anuncio = await _repository.GetById(id);

            anuncio.Inicio = request.Inicio;
            anuncio.Fin = request.Fin;
            anuncio.ReproduccionesLimite = request.ReproduccionesLimite;

            await _repository.Update(anuncio);

            return _mapper.Map<AnuncioBaseResponse>(anuncio);
        }

        public async Task<Guid> UpdateRecurso(Guid id, AnuncioUpdateRecursoRequest request)
        {
            var anuncio = await _repository.GetById(id);

            var nuevoRecurso = await _fileManager.Upload(request.Recurso, FileType.Anuncio);

            _fileManager.Remove(anuncio.Recurso, FileType.Anuncio);

            anuncio.Recurso = nuevoRecurso;
            anuncio.Tipo = request.Recurso.GetTipo();
            await _repository.Update(anuncio);

            return anuncio.Id;
        }

        public async Task<AnuncioBaseResponse> ConfirmCreation(Guid id, bool finalizado)
        {
            var anuncio = await _repository.GetById(id);

            anuncio.Finalizado = finalizado;
            await _repository.Update(anuncio);
            
            return _mapper.Map<AnuncioBaseResponse>(anuncio);
        }

        public async Task<bool> CheckDates(DateTime inicio, DateTime fin)
        {
            var list = await _repository.GetAll(anuncio => anuncio.Finalizado && anuncio.Activo.Value 
                        && ( (anuncio.Inicio > inicio && inicio < anuncio.Fin) || (anuncio.Inicio > fin && fin < anuncio.Fin) ));
            
            return !list.Any();
        }

        public async Task<AnuncioDetallesResponse> GetById(Guid id)
        {
            var entity = await _repository.GetById(id);
            return _mapper.Map<AnuncioDetallesResponse>(entity);
        }

        public async Task<IEnumerable<AnuncioBaseResponse>> GetAllFrom(Guid id)
        {
            var lista = await _repository.GetAll(anuncio => anuncio.GimnasioId == id && anuncio.Finalizado);
            return _mapper.Map<IEnumerable<AnuncioBaseResponse>>(lista);
        }
    }
}