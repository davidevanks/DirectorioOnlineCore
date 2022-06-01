using DataAccess.Models;
using Models.ViewModels;
using System.Collections.Generic;

namespace DataAccess.Repository.IRepository
{
    public interface IScheduleBusinessRepository : IRepository<HorarioNegocio>
    {
        #region Public Methods

        List<HorarioNegocioViewModel> GetScheduleListByBusinessId(int id);

        List<HorarioNegocioViewModel> GetScheduleListToEditByBusinessId(int id);

        void InsertList(List<HorarioNegocio> schedules);

        void Update(HorarioNegocio schedule);

        void UpdateList(List<HorarioNegocio> schedules);

        #endregion Public Methods
    }
}