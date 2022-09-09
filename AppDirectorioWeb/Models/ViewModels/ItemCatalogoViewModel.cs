using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class ItemCatalogoViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe pertenecer a un catalogo padre")]
        public int IdConfigCatalogo { get; set; }
        [Required(ErrorMessage = "La categoria es requerida")]
        [Display(Name = "Categoría", Prompt = "A que categoría pertenece el articulo/servicio?")]
        public int IdCategoriaItem { get; set; }
        [Required(ErrorMessage = "El nombre del producto/servicio es requerido")]
        [Display(Name = "Nombre producto/servicio", Prompt = "Nombre del producto/servicio")]
        public string NombreItem { get; set; }

        public string CodigoRef { get; set; }
        [Required(ErrorMessage = "La descripción del producto/servicio es requerido")]
        [Display(Name = "Descripción producto/servicio", Prompt = "Descripción del producto/servicio")]
        public string DescripcionItem { get; set; }
        [Required(ErrorMessage = "El precio es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Valor inválido")]
        public decimal PrecioUnitario { get; set; }
        [Required]
        public bool TieneDescuento { get; set; }
        public string DescripcionTieneDescuento { get; set; }
        public int PorcentajeDescuento { get; set; }
        public string ImagenItem { get; set; }
        [Display(Name = "Disponible")]
        public bool Activo { get; set; }
        public string DescripcionActivo { get; set; }
        public IFormFile PictureItem { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public string IdUsuarioActualizacion { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

    }
}
