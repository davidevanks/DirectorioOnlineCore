using DataAccess.Models;
using DataAccess.Repository.IRepository;
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

        #endregion Public Constructors

        #region Public Methods

        public void Update(Negocio negocio)
        {
            var objFromDb = _db.Negocios.FirstOrDefault(s => s.Id == negocio.Id);
            if (objFromDb != null)
            {
                objFromDb.Id = negocio.Id;
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
                objFromDb.IdUserCreate = negocio.IdUserCreate;
                objFromDb.CreateDate = negocio.CreateDate;
                objFromDb.IdUserUpdate = negocio.IdUserUpdate;
                objFromDb.UpdateDate = negocio.UpdateDate;
            }
        }

        #endregion Public Methods
    }
}