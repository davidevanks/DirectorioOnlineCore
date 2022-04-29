﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using Models.ViewModels;

namespace DataAccess.Repository.IRepository
{
   public  interface IDepartamentRepository : IRepository<CatDepartamento>
    {
        void Update(CatDepartamento department);
    }
}
