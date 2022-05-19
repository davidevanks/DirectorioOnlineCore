using DataAccess.Models;
using DataAccess.Repository.IRepository;
using Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repository
{
    public class ImagesBusinessRepository : Repository<ImagenesNegocio>, IImagesBusinessRepository
    {
        #region Private Fields

        private readonly DirectorioOnlineCoreContext _db;

        #endregion Private Fields

        #region Public Constructors

        public ImagesBusinessRepository(DirectorioOnlineCoreContext db) : base(db)
        {
            _db = db;
        }

        #endregion Public Constructors

        #region Public Methods

        public List<ImagenesNegocioViewModel> GetImagesByBusinessId(int id)
        {
            var images = _db.ImagenesNegocios.AsQueryable();

            var query = (from im in images
                         where im.IdNegocio == id
                         select new ImagenesNegocioViewModel
                         {
                             Id = im.Id,
                             IdNegocio = im.IdNegocio,
                             Image = im.Image,
                             IdUserCreate = im.IdUserCreate,
                             CreateDate = im.CreateDate
                         }).ToList();

            return query;
        }

        public List<ImagenesNegocio> GetRangeImagesToDeleteByBusinessId(int id)
        {
            var images = _db.ImagenesNegocios.AsQueryable();

            var query = (from im in images
                         where im.IdNegocio == id
                         select new ImagenesNegocio
                         {
                             Id = im.Id,
                             IdNegocio = im.IdNegocio,
                             Image = im.Image
                         }).ToList();

            return query;
        }

        public void InsertList(List<ImagenesNegocio> imagenes)
        {
            _db.ImagenesNegocios.AddRange(imagenes);
        }

        public void RemoveList(List<ImagenesNegocio> imagenes)
        {
            _db.ImagenesNegocios.RemoveRange(imagenes);
        }

        public void Update(ImagenesNegocio image)
        {
            var objFromDb = _db.ImagenesNegocios.FirstOrDefault(s => s.Id == image.Id);
            if (objFromDb != null)
            {
                objFromDb.Image = image.Image;
                objFromDb.IdNegocio = image.IdNegocio;
                objFromDb.IdUserUpdate = image.IdUserUpdate;
                objFromDb.UpdateDate = image.UpdateDate;
            }
        }

        #endregion Public Methods
    }
}