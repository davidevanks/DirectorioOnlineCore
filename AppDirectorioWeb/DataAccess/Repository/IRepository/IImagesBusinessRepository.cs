using DataAccess.Models;
using Models.ViewModels;
using System.Collections.Generic;

namespace DataAccess.Repository.IRepository
{
    public interface IImagesBusinessRepository : IRepository<ImagenesNegocio>
    {
        #region Public Methods

        List<ImagenesNegocioViewModel> GetImagesByBusinessId(int id);

        List<ImagenesNegocio> GetRangeImagesToDeleteByBusinessId(int id);

        void InsertList(List<ImagenesNegocio> imagenes);

        void RemoveList(List<ImagenesNegocio> imagenes);

        void Update(ImagenesNegocio image);

        #endregion Public Methods
    }
}