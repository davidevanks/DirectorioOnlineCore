using DataAccess.Models;
using DataAccess.Repository.IRepository;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utiles;

namespace AppDirectorioWeb.Areas.Ventas.Controllers
{
    [Area("Ventas")]
    public class ItemCatalogoProdServController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public ItemCatalogoProdServController(
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
        [Authorize]
        public IActionResult Index(int idConfigCat)
        {
         

            if (idConfigCat==0)
            {
                ViewBag.idConfigCat = HttpContext.Session.GetString("idConfigCat");
            }
            else
            {
                ViewBag.idConfigCat = idConfigCat;

                HttpContext.Session.SetString("idConfigCat", idConfigCat.ToString());
            }

            return View();
        }

        public IActionResult SeeCatProd(int idConfigCat)
        {
            //revisar descuento y mejorar UI
            List<ItemCatalogoViewModel> model = new List<ItemCatalogoViewModel>();
            model = _unitOfWork.ItemCatalogo.GetItemsCatalogoForBueyrs(idConfigCat);
            return View(model);
        }


        [Authorize(Roles = SP.Role_BusinesAdmin + "," + SP.Role_Admin)]
        public IActionResult AddUpdItemCat(int? id)
        {
         
            ItemCatalogoViewModel model = new ItemCatalogoViewModel();
            if (id == null)
            {

                model.Id = 0;
                model.Activo = true;
                model.IdConfigCatalogo = Convert.ToInt32(HttpContext.Session.GetString("idConfigCat"));

                model.Categories = _unitOfWork.Category.GetAll().Where(x => x.IdPadre == 43 && x.Activo == true).Select(x => new { x.Id, x.Nombre }).Select(i => new SelectListItem
                {
                    Text = i.Nombre,
                    Value = i.Id.ToString()
                });

                return View(model);
            }

            var c = _unitOfWork.ItemCatalogo.GetItemCatalogoById((int)id);

            if (c == null)
            {
                return NotFound();
            }

            model = c;
            model.Categories = _unitOfWork.Category.GetAll().Where(x => x.IdPadre == 43 && x.Activo == true).Select(x => new { x.Id, x.Nombre }).Select(i => new SelectListItem
            {
                Text = i.Nombre,
                Value = i.Id.ToString()
            });
            //ViewBag.CuponActive = _unitOfWork.Cuponera.VerifyActiveCupon(c.IdNegocio);
            return View(model);

        }



        [Authorize(Roles = SP.Role_BusinesAdmin + "," + SP.Role_Admin)]
        public IActionResult GetDetailsRead(int? id)
        {

            ItemCatalogoViewModel model = new ItemCatalogoViewModel();
           

            var c = _unitOfWork.ItemCatalogo.GetItemCatalogoById((int)id);

            if (c == null)
            {
                return NotFound();
            }

            model = c;
            model.Categories = _unitOfWork.Category.GetAll().Where(x => x.IdPadre == 43 && x.Activo == true).Select(x => new { x.Id, x.Nombre }).Select(i => new SelectListItem
            {
                Text = i.Nombre,
                Value = i.Id.ToString()
            });
            //ViewBag.CuponActive = _unitOfWork.Cuponera.VerifyActiveCupon(c.IdNegocio);
            return View(model);

        }


        [Authorize(Roles = SP.Role_BusinesAdmin + "," + SP.Role_Admin)]
        [HttpPost]
        public IActionResult AddUpdItemCat(ItemCatalogoViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    //creación
                    _unitOfWork.ItemCatalogo.Add(new ItemCatalogo
                    {
                        IdCategoriaItem=model.IdCategoriaItem,
                        IdConfigCatalogo=model.IdConfigCatalogo,
                        IdUsuarioCreacion = HttpContext.Session.GetString("UserId"),
                        NombreItem=model.NombreItem,
                        DescripcionItem=model.DescripcionItem,
                        PrecioUnitario=model.PrecioUnitario,
                        TieneDescuento=model.TieneDescuento,
                        PorcentajeDescuento=model.PorcentajeDescuento,
                        FechaCreacion=DateTime.Now,
                        Activo=model.Activo,
                        ImagenItem= SaveItemPicture(model).Result,

                    });
                }
                else
                {
                    var imageStateActual = _unitOfWork.ItemCatalogo.Get(model.Id).ImagenItem;
                    //update
                    _unitOfWork.ItemCatalogo.Update(new ItemCatalogo
                    {
                        Id = model.Id,
                        IdCategoriaItem = model.IdCategoriaItem,
                        IdConfigCatalogo = model.IdConfigCatalogo,
                        NombreItem = model.NombreItem,
                        DescripcionItem = model.DescripcionItem,
                        PrecioUnitario = model.PrecioUnitario,
                        TieneDescuento = model.TieneDescuento,
                        PorcentajeDescuento = model.PorcentajeDescuento,
                        Activo = model.Activo,
                        ImagenItem = (model.PictureItem != null && model.PictureItem.Length > 0) ? SaveItemPicture(model).Result : imageStateActual,
                        FechaActualizacion = DateTime.Now,
                        IdUsuarioActualizacion = HttpContext.Session.GetString("UserId")
                    });
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            model.Categories = _unitOfWork.Category.GetAll().Where(x => x.IdPadre == 43 && x.Activo == true).Select(x => new { x.Id, x.Nombre }).Select(i => new SelectListItem
            {
                Text = i.Nombre,
                Value = i.Id.ToString()
            });
            return View(model);
        }

        [HttpGet]
        public IActionResult GetItemsCat(int idConfigCat)
        {
            var ItemsCat = _unitOfWork.ItemCatalogo.GetItemsCatalogo(idConfigCat);

            return Json(new { data = ItemsCat });
        }

        public async Task<string> SaveItemPicture(ItemCatalogoViewModel model)
        {
            string uniqueFileName = "";
            try
            {

                if (model.PictureItem != null && model.PictureItem.Length > 0)
                {

                    uniqueFileName = Guid.NewGuid().ToString() + model.PictureItem.FileName;

                    Stream stream = model.PictureItem.OpenReadStream();
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
                        ).Child("businessCatalog")
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


        [HttpGet]
        [Authorize(Roles = SP.Role_BusinesAdmin + "," + SP.Role_Admin)]
        public async Task<IActionResult> DeleteItemPic(int id)
        {
            var Item = _unitOfWork.ItemCatalogo.Get(id);
            if (Item == null)
            {
                return Json(new { success = false, message = "Error al borrar" });
            }

            //firebase logic


            try
            {
                string fileName = Item.ImagenItem.Replace("https://firebasestorage.googleapis.com/v0/b/brujulapyme-1bb75.appspot.com/o/businessCatalog%2F", "");


                fileName = fileName.Substring(0, fileName.IndexOf('?'));
                var auth = new FirebaseAuthProvider(new FirebaseConfig(FirebaseSetting.ApiKey));
                var a = await auth.SignInWithEmailAndPasswordAsync(FirebaseSetting.AuthEmail, FirebaseSetting.AuthPassword);

                //cancellation token
                var cancellation = new CancellationTokenSource();

                var del = new FirebaseStorage(
                    FirebaseSetting.Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true

                    }
                    ).Child("businessCatalog").Child(fileName).DeleteAsync();


                //firebaselogic

                Item.ImagenItem = "";
                _unitOfWork.ItemCatalogo.Update(Item);
                _unitOfWork.Save();

                return Json(new { success = true, message = "Se borro imagén exitosamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
                throw;
            }



        }
    }
}
