using DataAccess.Models;
using DataAccess.Repository.IRepository;
using Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repository
{
    public class BusinessRepository : Repository<Negocio>, IBusinessRepository
    {
        #region Private Fields

        private readonly DirectorioOnlineCoreContext _db;

        #endregion Private Fields

        #region Public Constructors

        public BusinessRepository(DirectorioOnlineCoreContext db) : base(db)
        {
            _db = db;
        }

        public BusinessOwnerViewModel GetBusinessById(int id)
        {
            var business = _db.Negocios.AsQueryable();
            var businessCategory = _db.CatCategoria.AsQueryable();//.Where(x => x.IdPadre == 20);
            var businessStatus = _db.CatCategoria.AsQueryable(); //.Where(x => x.IdPadre == 16);
            var deparment = _db.CatDepartamentos.AsQueryable();
            var users = _db.Users.AsQueryable();
            var usersDetails = _db.UserDetails.AsQueryable();

            var query = (from b in business
                         join bc in businessCategory on b.IdCategoria equals bc.Id
                         join bStatus in businessStatus on b.Status equals bStatus.Id
                         join dep in deparment on b.IdDepartamento equals dep.Id
                         join u in users on b.IdUserOwner equals u.Id
                         join ud in usersDetails on u.Id equals ud.UserId
                         where b.Id == id
                         select new BusinessOwnerViewModel
                         {
                             Id = b.Id,
                             FullName = ud.FullName,
                             PictureProfile=ud.UserPicture,
                             TelefonoWhatsApp=b.TelefonoWhatsApp,
                             TwitterUrl=b.TwitterUrl,
                             FacebookUrl=b.FacebookUrl,
                             InstagramUrl=b.InstagramUrl,
                             LinkedInUrl=b.LinkedInUrl,
                             HasDelivery=(bool)b.HasDelivery,
                             PedidosYa=(bool)b.PedidosYa,
                             Piki=(bool)b.Piki,
                             Hugo=(bool)b.Hugo,
                             EmailNegocio=b.EmailNegocio,
                             Email = u.UserName,
                             NombreNegocio = b.NombreNegocio,
                             DireccionNegocio = b.DireccionNegocio,
                             TelefonoNegocio1 = b.TelefonoNegocio1,
                             TelefonoNegocio2=b.TelefonoNegocio2,
                             DescripcionNegocio=b.DescripcionNegocio,
                             IdCategoria = bc.Id.ToString(),
                             categoryBusinessName = bc.Nombre,
                             Status = bStatus.Id,
                             statusName = bStatus.Nombre,
                             IdDepartamento = dep.Id.ToString(),
                             departmentName = dep.Nombre,
                             CreateDateString = b.CreateDate.ToShortDateString(),
                             IdUserOwner = b.IdUserOwner
                         }).FirstOrDefault();

            return query;
        }

        public BussinesViewModel GetBusinessToEditById(int id)
        {
            var business = _db.Negocios.AsQueryable();
            var businessCategory = _db.CatCategoria.AsQueryable();//.Where(x => x.IdPadre == 20);
            var businessStatus = _db.CatCategoria.AsQueryable(); //.Where(x => x.IdPadre == 16);
            var deparment = _db.CatDepartamentos.AsQueryable();
            var users = _db.Users.AsQueryable();
            var usersDetails = _db.UserDetails.AsQueryable();

            var query = (from b in business
                         join bc in businessCategory on b.IdCategoria equals bc.Id
                         join bStatus in businessStatus on b.Status equals bStatus.Id
                         join dep in deparment on b.IdDepartamento equals dep.Id
                         join u in users on b.IdUserOwner equals u.Id
                         join ud in usersDetails on u.Id equals ud.UserId
                         where b.Id == id
                         select new BussinesViewModel
                         {
                             Id = b.Id,
                             TelefonoWhatsApp = b.TelefonoWhatsApp,
                             TwitterUrl = b.TwitterUrl,
                             FacebookUrl = b.FacebookUrl,
                             InstagramUrl = b.InstagramUrl,
                             LinkedInUrl = b.LinkedInUrl,
                             SitioWebNegocio=b.SitioWebNegocio,
                             HasDelivery = (bool)b.HasDelivery,
                             PedidosYa = (bool)b.PedidosYa,
                             Piki = (bool)b.Piki,
                             Hugo = (bool)b.Hugo,
                             EmailNegocio = b.EmailNegocio,
                             NombreNegocio = b.NombreNegocio,
                             DireccionNegocio = b.DireccionNegocio,
                             TelefonoNegocio1 = b.TelefonoNegocio1,
                             TelefonoNegocio2 = b.TelefonoNegocio2,
                             DescripcionNegocio = b.DescripcionNegocio,
                             IdCategoria = bc.Id.ToString(),
                             Status = bStatus.Id,
                             IdDepartamento = dep.Id.ToString(),
                             IdUserOwner = b.IdUserOwner,
                             LogoNegocio=b.LogoNegocio,
                             Tags=b.Tags,
                             IdUserCreate=b.IdUserCreate,
                             CreateDate=b.CreateDate
                         }).FirstOrDefault();

            return query;
        }

        public List<BusinessOwnerViewModel> GetListBusinessByOwners(string idOwner)
        {
           
            var business = _db.Negocios.AsQueryable();
            var businessCategory = _db.CatCategoria.AsQueryable();//.Where(x => x.IdPadre == 20);
            var businessStatus = _db.CatCategoria.AsQueryable(); //.Where(x => x.IdPadre == 16);
            var deparment = _db.CatDepartamentos.AsQueryable();
            var users = _db.Users.AsQueryable();
            var usersDetails = _db.UserDetails.AsQueryable();

            var query = (from b in business
                         join bc in businessCategory on b.IdCategoria equals bc.Id
                         join bStatus in businessStatus on b.Status equals bStatus.Id
                         join dep in deparment on b.IdDepartamento equals dep.Id
                         join u in users on b.IdUserOwner equals u.Id
                         join ud in usersDetails on u.Id equals ud.UserId
                         select new BusinessOwnerViewModel { 
                         Id=b.Id,
                         FullName=ud.FullName,
                         Email=u.UserName,
                         NombreNegocio=b.NombreNegocio,
                         IdCategoria=bc.Id.ToString(),
                         categoryBusinessName=bc.Nombre,
                         Status=bStatus.Id,
                         statusName=bStatus.Nombre,
                         IdDepartamento=dep.Id.ToString(),
                         departmentName=dep.Nombre,
                         CreateDateString = b.CreateDate.ToShortDateString(),
                         IdUserOwner=b.IdUserOwner
                         });

            if (idOwner!= "-1")
            {
                query = query.Where(x => x.IdUserOwner == idOwner);
            }

            return query.ToList();
        }

        #endregion Public Constructors

        #region Public Methods

        public void Update(Negocio negocio)
        {
            var objFromDb = _db.Negocios.FirstOrDefault(s => s.Id == negocio.Id);
            if (objFromDb != null)
            {
                objFromDb.Id = negocio.Id;
                //objFromDb.IdUserOwner = negocio.IdUserOwner;
                objFromDb.NombreNegocio = negocio.NombreNegocio;
                objFromDb.DescripcionNegocio = negocio.DescripcionNegocio;
                objFromDb.Tags = negocio.Tags;
                objFromDb.IdCategoria = negocio.IdCategoria;
                objFromDb.IdDepartamento = negocio.IdDepartamento;
                objFromDb.DireccionNegocio = negocio.DireccionNegocio;
                objFromDb.TelefonoNegocio1 = negocio.TelefonoNegocio1;
                objFromDb.TelefonoNegocio2 = negocio.TelefonoNegocio2;
                objFromDb.TelefonoWhatsApp = negocio.TelefonoWhatsApp;
                objFromDb.EmailNegocio = negocio.EmailNegocio;
                objFromDb.SitioWebNegocio = negocio.SitioWebNegocio;
                objFromDb.LinkedInUrl = negocio.LinkedInUrl;
                objFromDb.FacebookUrl = negocio.FacebookUrl;
                objFromDb.InstagramUrl = negocio.InstagramUrl;
                objFromDb.TwitterUrl = negocio.TwitterUrl;
                objFromDb.HasDelivery = negocio.HasDelivery;
                objFromDb.PedidosYa = negocio.PedidosYa;
                objFromDb.Hugo = negocio.Hugo;
                objFromDb.Piki = negocio.Piki;
                objFromDb.LogoNegocio = negocio.LogoNegocio;
                objFromDb.Status = negocio.Status;
                objFromDb.IdUserUpdate = negocio.IdUserUpdate;
                objFromDb.UpdateDate = negocio.UpdateDate;
            }
        }

        #endregion Public Methods
    }
}