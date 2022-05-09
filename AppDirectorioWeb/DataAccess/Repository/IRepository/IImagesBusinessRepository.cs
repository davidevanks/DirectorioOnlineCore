using DataAccess.Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IImagesBusinessRepository : IRepository<ImagenesNegocio>
    {
        void Update(ImagenesNegocio image);
        void InsertList(List<ImagenesNegocio> imagenes);
        void RemoveList(List<ImagenesNegocio> imagenes);
        List<ImagenesNegocioViewModel> GetImagesByBusinessId(int id);
    }
}
