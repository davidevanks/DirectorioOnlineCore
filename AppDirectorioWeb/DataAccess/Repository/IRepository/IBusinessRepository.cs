using DataAccess.Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IBusinessRepository : IRepository<Negocio>
    {
        void Update(Negocio negocio);
        List<BusinessOwnerViewModel> GetListBusinessByOwners(string idOwner);

        BusinessOwnerViewModel GetBusinessById(int id);
    }
}
