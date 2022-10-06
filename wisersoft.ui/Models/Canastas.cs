using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WiserSoft.UI.Models
{
    public class Canastas
    {

        public int Can_id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Nombre es requerido")]
        public string Can_nombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Fecha de creación es requerido")]
        public DateTime Can_fecha_de_creacion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Nombre usuario es requerido")]
        public string Can_usuario { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Feria es requerido")]
        public int Can_feria { get; set; }

        public int seleccionada { get; set; }
    }
}