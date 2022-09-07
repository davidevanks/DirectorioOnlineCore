using DataAccess.Models;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utiles;

namespace AppDirectorioWeb.Areas.Ventas.Controllers
{
    [Area("Ventas")]
    public class CatalogoProductServiceController : Controller
    {

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public CatalogoProductServiceController(
             UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = SP.Role_BusinesAdmin + "," + SP.Role_Admin)]
        public IActionResult AddUpdCatConfig(int? id)
        {
            string ownerId = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;
            ConfigCatalogoViewModel model = new ConfigCatalogoViewModel();
            if (id == null)
            {
            
                model.Id = 0;
                model.IdNegocio = _unitOfWork.Business.GetBusinessByIdOwner(ownerId).Id;
                model.IdUsuarioCreacion = ownerId;
                model.FechaCreacion = DateTime.Now;
                model.lstTipoPagos = new List<CatTipoPagoXcatalogoConfigViewModel>();
                model.lstTipoPagos = _unitOfWork.CatConfigPordServ.GetLstTipoPagoByIdCatConfig(id);
                return View(model);
            }

            var c = _unitOfWork.CatConfigPordServ.GetConfigCatById(id);

            if (c == null)
            {
                return NotFound();
            }

            model = c;
            model.FechaActualizacion = DateTime.Now;
            model.IdUsuarioActualizacion = ownerId;
            model.lstTipoPagos = _unitOfWork.CatConfigPordServ.GetLstTipoPagoByIdCatConfig(id);
            //ViewBag.CuponActive = _unitOfWork.Cuponera.VerifyActiveCupon(c.IdNegocio);
            return View(model);
           
        }

        [Authorize(Roles = SP.Role_BusinesAdmin + "," + SP.Role_Admin)]
        [HttpPost]
        public IActionResult AddUpdCatConfig(ConfigCatalogoViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    //creación
                   _unitOfWork.CatConfigPordServ.Add(new ConfigCatalogo
                    {
                        Id=model.Id,
                        IdMoneda=model.IdMoneda,
                        IdNegocio=model.IdNegocio,
                        IdTipoCatalogo=model.IdTipoCatalogo,
                        IdUsuarioCreacion=model.IdUsuarioCreacion,
                        FechaCreacion=model.FechaCreacion,
                        NombreCatalogo=model.NombreCatalogo,
                        DescuentoMasivo=model.DescuentoMasivo,
                        PorcentajeDescuentoMasivo=model.PorcentajeDescuentoMasivo,
                        Activo=model.Activo,
                        
                    });

                    _unitOfWork.Save();

                    foreach (var item in model.lstTipoPagos)
                    {
                        _unitOfWork.TipoPagoXcatConfig.Add(new CatTipoPagoXcatalogoConfig
                        {
                            IdCatConfigProdServ=model.Id,
                            IdTipoPago=item.IdTipoPago,
                            Active=item.Active
                        });

                        _unitOfWork.Save();
                    }
                }
                else
                {
                   
                    //update
                    _unitOfWork.CatConfigPordServ.UpdateCatConfig(new ConfigCatalogo
                    {
                        Id = model.Id,
                        IdMoneda = model.IdMoneda,
                        IdNegocio = model.IdNegocio,
                        IdTipoCatalogo = model.IdTipoCatalogo,
                        IdUsuarioCreacion = model.IdUsuarioCreacion,
                        FechaCreacion = model.FechaCreacion,
                        NombreCatalogo = model.NombreCatalogo,
                        DescuentoMasivo = model.DescuentoMasivo,
                        PorcentajeDescuentoMasivo = model.PorcentajeDescuentoMasivo,
                        Activo = model.Activo,
                    });

                    foreach (var item in model.lstTipoPagos)
                    {
                        _unitOfWork.TipoPagoXcatConfig.Update(new CatTipoPagoXcatalogoConfig
                        {
                          
                            Active = item.Active
                        });

                        _unitOfWork.Save();
                    }


                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult GetLstConfigCat()
        {
            //aqui quede. falta agregar los tipos de pago al lstConfigCat y probar que guarda, edita y carga los datos.luego probar validaciones
            //y resto de boton editar OJO
            int? idNegocio = null;
            //verificar que tipo de usuario esta consultabdo esto
            //hacer logica para traer los cupones, lgica para bussinesAdmin y Admin site
            if (User.IsInRole(SP.Role_BusinesAdmin))
            {
                idNegocio =Convert.ToInt32(HttpContext.Session.GetString("idNegocioUser"));
            }

            var configCat = _unitOfWork.CatConfigPordServ.lstConfigCat(idNegocio);

            foreach (var item in configCat)
            {
                item.NombreTipoPagos = _unitOfWork.CatConfigPordServ.GetStringNamesTipoPago(item.Id);
            }

            return Json(new { data = configCat });
        }
    }
}
