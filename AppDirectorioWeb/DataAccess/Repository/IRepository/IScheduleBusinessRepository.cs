﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IScheduleBusinessRepository : IRepository<HorarioNegocio>
    {
        void Update(HorarioNegocio schedule);
        void InsertList(List<HorarioNegocio> schedules);
    }
}