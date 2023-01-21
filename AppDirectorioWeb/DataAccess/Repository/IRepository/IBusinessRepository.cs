using DataAccess.Models;
using Models.ViewModels;
using System.Collections.Generic;

namespace DataAccess.Repository.IRepository
{
    public interface IBusinessRepository : IRepository<Negocio>
    {
        #region Public Methods

        BusinessOwnerViewModel GetBusinessById(int id);
        int? GetBusinessIdByPersonalUrl(string personalUrl);

        int GetCountBusinessIdByPersonalUrl(string personalUrl);

        BussinesViewModel GetBusinessToEditById(int id);

        List<BusinessOwnerViewModel> GetListBusinessByOwners(string idOwner);
        Negocio GetBusinessByIdOwner(string idOwner);

        void Update(Negocio negocio);



        #endregion Public Methods
    }
}