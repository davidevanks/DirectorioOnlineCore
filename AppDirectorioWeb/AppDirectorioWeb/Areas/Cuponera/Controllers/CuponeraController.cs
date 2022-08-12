using DataAccess.Models;
using DataAccess.Repository.IRepository;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utiles;

namespace AppDirectorioWeb.Areas.Cuponera.Controllers
{
    [Area("Cuponera")]
    public class CuponeraController : Controller
    {
        
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public CuponeraController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork)  
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
           
    }
        
        [Authorize(Roles = SP.Role_BusinesAdmin + "," + SP.Role_Admin)]
        public IActionResult Index()
        {
          
                return View();
        }

        [Authorize(Roles = SP.Role_BusinesAdmin)]
        public IActionResult Add(int? id)
        {
           
            CuponeraViewModel model = new CuponeraViewModel();
            if (id==null)
            {
                string ownerId = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;
                model.Id = 0;
                model.IdNegocio = _unitOfWork.Business.GetBusinessByIdOwner(ownerId).Id;
                return View(model);
            }

            var c = _unitOfWork.Cuponera.GetCuponById((int)id);

            if (c == null)
            {
                return NotFound();
            }

            model = c;

            return View(model);
        }

        [Authorize(Roles = SP.Role_BusinesAdmin)]
        [HttpPost]
        public IActionResult Add(CuponeraViewModel model)
        {
            string urlImageCupon = "";
            if (ModelState.IsValid)
            {
                if (model.Id==0)
                {
                    //creación
                    _unitOfWork.Cuponera.Add(new CuponNegocio
                    {
                         IdNegocio=model.IdNegocio,
                         DescripcionPromocion=model.DescripcionPromocion,
                         DescuentoPorcentaje=model.DescuentoPorcentaje,
                         DescuentoMonto=model.DescuentoMonto,
                         MonedaMonto=model.MonedaMonto,
                         ValorCupon=model.ValorCupon,
                         CantidadCuponDisponible=model.CantidadCuponDisponible,
                         ImagenCupon=  SaveCuponPicture(model).Result,
                         FechaExpiracionCupon=Convert.ToDateTime(model.FechaExpiracionCupon),
                         Status=model.Status,
                         FechaCreacion=DateTime.Now,
                         IdUsuarioCreacion= HttpContext.Session.GetString("UserId")
                    });
                }
                else
                {
                    //update
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
              
            return View(model);
        }

        #region ExtraFunctions
        public async Task<string> SaveCuponPicture(CuponeraViewModel model)
        {
            string uniqueFileName = "";
            try
            {

                if (model.PictureCupon.Length > 0)
                {
                    
                    uniqueFileName = Guid.NewGuid().ToString() +  model.PictureCupon.FileName;

                    Stream stream = model.PictureCupon.OpenReadStream();
                    //firebase logic to upload file
                    var auth = new FirebaseAuthProvider(new FirebaseConfig(FirebaseSetting.ApiKey));
                    var a = await auth.SignInWithEmailAndPasswordAsync(FirebaseSetting.AuthEmail, FirebaseSetting.AuthPassword);


                    //cancellation token
                    var cancellation = new CancellationTokenSource();

                    var upload = new FirebaseStorage(
                        FirebaseSetting.Bucket,
                        new FirebaseStorageOptions
                        {
                            AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                            ThrowOnCancel = true

                        }
                        ).Child("businessCupon")
                        .Child($"{uniqueFileName}")
                        .PutAsync(stream, cancellation.Token);



                    uniqueFileName = await upload;
                    
                }

            }
            catch (Exception ex)
            {

                throw;
            }



            return uniqueFileName;
        }
        #endregion
        #region Api
        [HttpGet]
        public IActionResult GetCupons()
        {
            string userId = "";
            //verificar que tipo de usuario esta consultabdo esto
            //hacer logica para traer los cupones, lgica para bussinesAdmin y Admin site
            if (User.IsInRole(SP.Role_BusinesAdmin))
            {
                userId = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;
            }

            var cupons = _unitOfWork.Cuponera.GetCupons(userId);

            return Json(new { data = cupons });
        }
        #endregion
    }
}
