using DataAccess.Models;
using Models.ViewModels;
using System.Collections.Generic;

namespace DataAccess.Repository.IRepository
{
    public interface IBusinessRepository : IRepository<Negocio>
    {
        #region Public Methods

        BusinessOwnerViewModel GetBusinessById(int id);

        BussinesViewModel GetBusinessToEditById(int id);

        List<BusinessOwnerViewModel> GetListBusinessByOwners(string idOwner);

        void Update(Negocio negocio);

        #endregion Public Methods
    }
}