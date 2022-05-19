using DataAccess.Models;
using Models.ViewModels;
using System.Collections.Generic;

namespace DataAccess.Repository.IRepository
{
    public interface IFeatureNegocioRepository : IRepository<FeatureNegocio>
    {
        #region Public Methods

        List<FeatureNegocioViewModel> GetListFeaturesByBusinessId(int id);

        List<FeatureNegocioViewModel> GetListFeaturesToEditByBusinessId(int id);

        void InsertList(List<FeatureNegocio> features);

        void Update(FeatureNegocio feature);

        void UpdateList(List<FeatureNegocio> features);

        #endregion Public Methods
    }
}