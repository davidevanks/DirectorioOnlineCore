using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDirectorioWeb.Controllers
{
    public class IndexController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public IndexController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(string personalUrl)
        {
            int? BussinessId = 0;

            BussinessId = _unitOfWork.Business.GetBusinessIdByPersonalUrl(personalUrl);
       

          return  Redirect($"Negocios/Negocios/GetDetailByBussinesId/{BussinessId}");
        }
    }
}
