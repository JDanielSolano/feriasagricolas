using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WiserSoft.UI.Models
{
    public class Productos_Por_Agricultor
    {

        public int Ppa_Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Producto es requerido")]
        public int Ppa_Id_Producto { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Agricultor es requerido")]
        public String Ppa_Id_Agricultor { get; set; }

        public Productos Producto { get; set; }
        public Agricultores Agricultores { get; set; }
    }
}