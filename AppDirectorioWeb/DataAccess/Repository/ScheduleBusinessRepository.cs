using DataAccess.Models;
using DataAccess.Repository.IRepository;
using Models.ViewModels;
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

        public List<HorarioNegocioViewModel> GetScheduleListByBusinessId(int id)
        {
           var days = _db.CatCategoria.AsQueryable();
           var scheduleBusiness = _db.HorarioNegocios.AsQueryable();

            var query = (from sb in scheduleBusiness join d in days on sb.IdDia equals d.Id
                         where sb.IdNegocio==id
                         select new HorarioNegocioViewModel { 
                         Id=sb.Id,
                         IdDia=sb.IdDia,
                         Day=d.Nombre,
                         HoraDesde=Convert.ToDateTime(sb.HoraDesde).ToShortTimeString(),
                         HoraHasta = Convert.ToDateTime(sb.HoraHasta).ToShortTimeString(),
                         Active=sb.Active,
                         IdUserCreate = sb.IdUserCreate,
                         CreateDate = sb.CreateDate
                         }).ToList();
           
            return query;

           
        }

        public List<HorarioNegocioViewModel> GetScheduleListToEditByBusinessId(int id)
        {
            var days = _db.CatCategoria.AsQueryable();
            var scheduleBusiness = _db.HorarioNegocios.AsQueryable();

            var query = (from sb in scheduleBusiness
                         join d in days on sb.IdDia equals d.Id
                         where sb.IdNegocio == id
                         select new HorarioNegocioViewModel
                         {
                             Id = sb.Id,
                             IdDia = sb.IdDia,
                             Day = d.Nombre,
                             HoraDesde = sb.HoraDesde,
                             HoraHasta = sb.HoraHasta,
                             Active = sb.Active,
                             IdUserCreate = sb.IdUserCreate,
                             CreateDate = sb.CreateDate
                         }).ToList();

            return query;

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
                //objFromDb.IdNegocio = schedule.IdNegocio;
                //objFromDb.IdDia = schedule.IdDia;
                objFromDb.Active = schedule.Active;
                objFromDb.IdUserUpdate = schedule.IdUserUpdate;
                objFromDb.UpdateDate = schedule.UpdateDate;

            }
        }

        public void UpdateList(List<HorarioNegocio> schedules)
        {
            _db.HorarioNegocios.UpdateRange(schedules);
        }
    }
}
