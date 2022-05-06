using DataAccess.Models;
using DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ScheduleBusinessRepository : Repository<HorarioNegocio>, IScheduleBusinessRepository
    {
        private readonly DirectorioOnlineCoreContext _db;
        public ScheduleBusinessRepository(DirectorioOnlineCoreContext db) : base(db)
        {
            _db = db;
        }
        public void InsertList(List<HorarioNegocio> schedules)
        {
            _db.HorarioNegocios.AddRange(schedules);
        }

        public void Update(HorarioNegocio schedule)
        {
            var objFromDb = _db.HorarioNegocios.FirstOrDefault(s => s.Id == schedule.Id);
            if (objFromDb != null)
            {
                objFromDb.HoraDesde = schedule.HoraDesde;
                objFromDb.HoraHasta = schedule.HoraHasta;
                objFromDb.IdNegocio = schedule.IdNegocio;
                objFromDb.IdDia = schedule.IdDia;
                objFromDb.Active = schedule.Active;
                objFromDb.IdUserUpdate = schedule.IdUserUpdate;
                objFromDb.UpdateDate = schedule.UpdateDate;

            }
        }
    }
}
