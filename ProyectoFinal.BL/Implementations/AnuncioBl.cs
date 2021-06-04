#nullable enable
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
        private readonly IRepository<Anuncio> _repoAd;
        private readonly IRepository<AnunciosUsuario> _repoAdsUser;

        public AnuncioBl(IMapper mapper, FileManager fileManager, IRepository<Anuncio> repository,
            IRepository<AnunciosUsuario> repoAdsUser)
        {
            _mapper = mapper;
            _fileManager = fileManager;
            _repoAd = repository;
            _repoAdsUser = repoAdsUser;
        }

        public async Task<Guid> Create(AnuncioCreateRequest request)
        {
            var recurso = await _fileManager.Upload(request.Recurso, FileType.Anuncio);

            var anuncio = await _repoAd.Create(new Anuncio
            {
                GimnasioId = request.GimnasioId,
                Recurso = recurso,
                Tipo = request.Recurso.GetTipo()
            });

            return anuncio.Id;
        }

        public async Task<AnuncioBaseResponse> UpdateDetails(Guid id, AnuncioUpdateDetailsRequest request)
        {
            var anuncio = await _repoAd.GetById(id);

            anuncio.Inicio = request.Inicio;
            anuncio.Fin = request.Fin;
            anuncio.ReproduccionesLimite = request.ReproduccionesLimite;

            await _repoAd.Update(anuncio);

            return _mapper.Map<AnuncioBaseResponse>(anuncio);
        }

        public async Task<Guid> UpdateRecurso(Guid id, AnuncioUpdateRecursoRequest request)
        {
            var anuncio = await _repoAd.GetById(id);

            var nuevoRecurso = await _fileManager.Upload(request.Recurso, FileType.Anuncio);

            _fileManager.Remove(anuncio.Recurso, FileType.Anuncio);

            anuncio.Recurso = nuevoRecurso;
            anuncio.Tipo = request.Recurso.GetTipo();
            await _repoAd.Update(anuncio);

            return anuncio.Id;
        }

        public async Task<AnuncioBaseResponse?> SetStatus(Guid id, bool finalizado)
        {
            var anuncio = await _repoAd.GetById(id);

            if (!finalizado)
            {
                _fileManager.Remove(anuncio.Recurso, FileType.Anuncio);
                await _repoAd.Delete(anuncio);
                return null;
            }

            anuncio.Finalizado = true;
            await _repoAd.Update(anuncio);
            anuncio.Recurso = _fileManager.Get(anuncio.Recurso, FileType.Anuncio);
            
            return _mapper.Map<AnuncioBaseResponse>(anuncio);
        }

        public async Task<bool> CheckDates(DateTime inicio, DateTime fin)
        {
            var list = await _repoAd.GetAll(
                anuncio => anuncio.Finalizado && anuncio.Activo!.Value 
                           && (inicio.Date <= anuncio.Inicio!.Value.Date && anuncio.Inicio!.Value.Date <= fin.Date 
                               || inicio.Date <= anuncio.Fin!.Value.Date && anuncio.Fin!.Value.Date <= fin.Date));

            return !list.Any();
        }

        public async Task<AnuncioDetallesResponse> GetById(Guid id)
        {
            var entity = await _repoAd.GetById(id);
            return _mapper.Map<AnuncioDetallesResponse>(entity);
        }

        public async Task<IEnumerable<AnuncioBaseResponse>> GetAllFrom(Guid id)
        {
            var lista = await _repoAd.GetAll(
                a => a.GimnasioId == id && a.Finalizado, "", a => a.FechaCreado);

            foreach (var anuncio in lista)
            {
                anuncio.Recurso = _fileManager.Get(anuncio.Recurso, FileType.Anuncio);
            }

            return _mapper.Map<IEnumerable<AnuncioBaseResponse>>(lista);
        }

        public async Task<AnuncioToWatchResponse?> GetAdToWatch()
        {
            var found = await _repoAd.GetByCondition(
                anuncio => anuncio.Activo!.Value && anuncio.Finalizado && anuncio.Inicio!.Value.Date <= DateTime.Today &&
                           DateTime.Today <= anuncio.Fin!.Value.Date);

            if (found is null) return null;

            found.Recurso = _fileManager.Get(found.Recurso, FileType.Anuncio, true);
            return _mapper.Map<AnuncioToWatchResponse>(found);
        }

        public async Task MarkAdAsWatched(Guid anuncioId, AnuncioWatchedRequest request)
        {
            var anuncio = await _repoAd.GetById(anuncioId);
            if (anuncio is null) return;

            // Actualizamos el anuncio en sí
            anuncio.Reproducciones += 1;

            if (anuncio.Reproducciones >= anuncio.ReproduccionesLimite)
            {
                anuncio.Activo = false;
            }

            await _repoAd.Update(anuncio);

            // Comprobamos la relación entre el anuncio y el usuario que lo ve
            var relation = await _repoAdsUser.GetByCondition(
                r => r.UsuarioId == request.UsuarioId && r.AnucioId == anuncioId);

            // Actualizamos la relación si ya existe
            if (relation != null)
            {
                relation.Reproducciones += 1;
                await _repoAdsUser.Update(relation);
                return;
            }

            // O se crea en caso que no estuviese aún
            relation = new AnunciosUsuario
            {
                Reproducciones = 1,
                AnucioId = anuncioId,
                UsuarioId = request.UsuarioId,
            };

            await _repoAdsUser.Create(relation);
        }

        public async Task Update(Guid id, AnuncioUpdateRequest request)
        {
            var anuncio = await _repoAd.GetById(id);
            if (anuncio is null) return;

            anuncio.Activo = request.Activo;

            await _repoAd.Update(anuncio);
        }
    }
}