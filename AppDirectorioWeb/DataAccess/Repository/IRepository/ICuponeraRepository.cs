﻿using DataAccess.Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface ICuponeraRepository : IRepository<CuponNegocio>
    {
        void Update(CuponNegocio cuponNegocio);

        List<CuponeraViewModel> GetCupons(string userId);

        bool VerifyActiveCupon(int idNegocio);

        CuponeraViewModel GetCuponById(int id);
    }
}
