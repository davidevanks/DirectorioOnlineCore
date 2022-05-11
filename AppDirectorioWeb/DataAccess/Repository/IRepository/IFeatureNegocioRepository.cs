using DataAccess.Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IFeatureNegocioRepository : IRepository<FeatureNegocio>
    {
        void Update(FeatureNegocio feature);
        void InsertList(List<FeatureNegocio> features);
        List<FeatureNegocioViewModel> GetListFeaturesByBusinessId(int id);
        List<FeatureNegocioViewModel> GetListFeaturesToEditByBusinessId(int id);
    }
}
