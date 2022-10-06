using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WiserSoft.UI.Models
{
    public class Compras
    {
        public int Com_Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Canasta es requerido")]
        public int Com_Id_Canasta { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Producto por agricultor es requerido")]
        public int Com_Id_Producto_Por_Agricultor { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Cantidad es requerido")]
        public int Com_Cantidad { get; set; }

        public Productos Producto { get; set; }
    }
}