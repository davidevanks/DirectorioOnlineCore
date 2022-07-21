using DataAccess.Models;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Utiles;

namespace AppDirectorioWeb.Areas.Ventas.Controllers
{
    [Area("Ventas")]
    public class FacturacionController : Controller
    {
        private readonly DirectorioOnlineCoreContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;
        public FacturacionController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, DirectorioOnlineCoreContext db, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
            _emailSender = emailSender; 
        }
        // GET: FacturacionController
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = SP.Role_Admin)]
        public IActionResult UpInvoice(int id)
        {
            FacturaViewModel invoice = new FacturaViewModel();
          

            var fac = _unitOfWork.Factura.GetDetailInvoice(id);

            if (fac == null)
            {
                return NotFound();
            }

            invoice.User = fac.User;
            invoice.UserId = fac.UserId;
            invoice.NoAutorizacion = fac.NoAutorizacion;
            invoice.PlanSuscripcion = fac.PlanSuscripcion;
            invoice.IdFactura = fac.IdFactura;

            return View(invoice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SP.Role_Admin)]
        public IActionResult UpInvoice(FacturaViewModel model)
        {
            if (ModelState.IsValid)
            {

                Factura updFact = new Factura();
                updFact.Id = model.IdFactura;
                updFact.FechaPago = DateTime.Now;
                updFact.FacturaEnviada = true;
                updFact.FacturaPagada = true;
                updFact.FechaActualizacion = DateTime.Now;
                updFact.IdUserUpdate = HttpContext.Session.GetString("UserId");
                updFact.NoAutorizacionPago = model.NoAutorizacion;






                _unitOfWork.Factura.Update(updFact);
                


                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<bool> SendInvoiceAsync(int idFactura)
        {
            string MailText = "";
            string returnUrl = null;
            returnUrl ??= Url.Content("~/");

            FacturaViewModel factura = new FacturaViewModel();
            factura = _unitOfWork.Factura.GetDetailInvoice(idFactura);
            //aqui quede
            string FilePath = Directory.GetCurrentDirectory() + "\\wwwroot\\EmailTemplates\\html_invoice_email_template.html";
            StreamReader str = new StreamReader(FilePath);
            MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[negocioName]", business.NombreNegocio);



            await _emailSender.SendEmailAsync(user.Email, "Factura suscripción Brujula Pyme", MailText);
        }

        #region API_CALLS
        [HttpGet]
        public IActionResult GetInvoices(string userId)
        {
            List<FacturaViewModel> invoiceList = new List<FacturaViewModel>();
            invoiceList = _unitOfWork.Factura.GetInvoice("");
            return Json(new { data = invoiceList });
        }
        #endregion

    }
}
