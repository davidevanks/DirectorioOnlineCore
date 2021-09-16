﻿using Models.Dto;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface IColorRepository
    {
        Task<IEnumerable<Colors>> GetAllColor();
        Task<Colors> GetColor(int id);
        Task<bool> InsertColor(ColorsCreateDto model);
        Task<bool> UpdateColor(ColorsUpdateDto model);
        Task<bool> DeleteColor(ColorsDeleteDto model);
    }
}
