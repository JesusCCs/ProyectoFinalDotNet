﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoFinal.Core.DTO;
using ProyectoFinal.DAL.Models;

namespace ProyectoFinal.BL.Contracts
{
    public interface IAnuncioBl
    {
        Task<Guid> Create(AnuncioCreateStep1Request request);
        Task<IEnumerable<AnuncioDatesResponse>> GetDates();
        Task<AnuncioDetallesResponse> GetById(Guid id);
        Task<IEnumerable<AnunciosGimnasioResponse>> GetAllFrom(Guid id);
    }
}