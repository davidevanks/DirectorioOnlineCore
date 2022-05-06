using DataAccess.Models;
using DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ImagesBusinessRepository : Repository<ImagenesNegocio>, IImagesBusinessRepository
    {
        private readonly DirectorioOnlineCoreContext _db;
        public ImagesBusinessRepository(DirectorioOnlineCoreContext db) : base(db)
        {
            _db = db;
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
    }
}
