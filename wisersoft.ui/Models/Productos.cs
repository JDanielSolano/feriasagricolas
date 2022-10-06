using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WiserSoft.UI.Models
{
    public class Productos
    {
        public int Pdt_id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Nombre de producto es requerido")]
        [MinLength(2, ErrorMessage = "El número mínimo de caracteres es de 2")]
        [MaxLength(100, ErrorMessage = "El número máximo de caracteres es de 100")]
        public string Pdt_nombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Unidad de medida es requerido")]
        public int Pdt_unidad_de_medida { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Precio es requerido.")]
        public decimal Pdt_precio { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Tipo es requerido.")]
        public int Pdt_tipo { get; set; }

       
        public byte[] Pdt_foto { get; set; }
        
        public Medidas Medidas { get; set; }
        public Clasificacion Clasificacion { get; set; }
    }
}