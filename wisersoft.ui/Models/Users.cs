using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WiserSoft.UI.Models
{
   public class Users
    {
        [Required(ErrorMessage = "Nombre usuario es requerido")]
        [MinLength(5, ErrorMessage = "El número mínimo de caracteres es de 2")]
        [MaxLength(100, ErrorMessage = "El número máximo de caracteres es de 100")]
        public String Us_User_Name { get; set; }

        [Required(ErrorMessage = "Nombre completo es requerido")]
        [MinLength(2, ErrorMessage = "El número mínimo de caracteres es de 2")]
        [MaxLength(100, ErrorMessage = "El número máximo de caracteres es de 100")]
        public String Us_Nombre { get; set; }

        [Required(ErrorMessage = "Password es requerido")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
            ErrorMessage = "El password debe ser mínimo de 8 caracteres, debe contener al menos un número, una mayúscula y una minúscula ")]
        [MaxLength(100, ErrorMessage = "El número máximo de caracteres es de 100")]
        public String Us_Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Confirmar Password es requerido")]
        [Compare("Us_Password", ErrorMessage = "Password no coinciden.")]
        public String ConfirmPassowrd { get; set; }
    }
}