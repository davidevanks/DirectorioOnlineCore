﻿using DataAccess.Models;
using Models.ViewModels;
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
        List<HorarioNegocioViewModel> GetScheduleListByBusinessId(int id);

        List<HorarioNegocioViewModel> GetScheduleListToEditByBusinessId(int id);

        void UpdateList(List<HorarioNegocio> schedules);
    }
}
