using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WiserSoft.UI.Models
{
    public class Ferias_Agricolas
    {
        public int Fa_Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Nombre de referia es requerido")]
        [MinLength(2, ErrorMessage = "El número mínimo de caracteres es de 2")]
        [MaxLength(100, ErrorMessage = "El número máximo de caracteres es de 100")]
        public String Fa_Nombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Distrito es requerido")]
        public int Fa_Distrito { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Latitud es requerido")]
        public String Fa_Latitud { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Longitud es requerido")]
        public String Fa_longitud { get; set; }
        
        public Distritos Distritos { get; set; }
        public Cantones Cantones { get; set; }
        public Provincias Provincias { get; set; }
    }
}