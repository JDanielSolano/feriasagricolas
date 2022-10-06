using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WiserSoft.UI.Models
{
    public class Clientes
    {
        [DisplayName("Cédula")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Cédula es requerido")]
        [MinLength(9, ErrorMessage = "Debe contener mínimo 9 caracteres")]
        public String Cl_Cedula { get; set; }
        [DisplayName("Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nombre es requerido")]
        [MinLength(2, ErrorMessage = "Debe contener mínimo 2 caracteres")]
        [MaxLength(100, ErrorMessage = "Debe contener máximo 100 caracteres")]
        public String Cl_Nombre { get; set; }
        [DisplayName("Apellidos")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Apellidos es requerido")]
        [MinLength(2, ErrorMessage = "Debe contener mínimo 2 caracteres")]
        [MaxLength(100, ErrorMessage = "Debe contener máximo 100 caracteres")]
        public String Cl_Apellidos { get; set; }
        [DisplayName("Distrito")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Distrito es requerido")]
        public int Cl_Distrito { get; set; }
        [DisplayName("Teléfono")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Teléfono es requerido")]
        [MinLength(2, ErrorMessage = "Debe contener mínimo 8 caracteres")]
        [Phone(ErrorMessage ="Ingrese un número válido")]
        public String Cl_Telefono { get; set; }
        [DisplayName("Correo Electrónico")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Teléfono es requerido")]
        [EmailAddress(ErrorMessage ="Ingrese un correo válido")]
        public String Cl_Correo { get; set; }
        [DisplayName("Clave")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password es requerido")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
           ErrorMessage = "El password debe ser mínimo de 8 caracteres, debe contener al menos un número, una mayúscula y una minúscula ")]
        [MaxLength(100, ErrorMessage = "El número máximo de caracteres es de 100")]
        public String Cl_Password { get; set; }
        [DisplayName("Estado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Estado es requerido")]
        public int Cl_Estado { get; set; }
        [DisplayName("Usuario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nombre de usuario es requerido")]
        [MinLength(2, ErrorMessage = "Debe contener mínimo 2 caracteres")]
        [MaxLength(100, ErrorMessage = "Debe contener máximo 100 caracteres")]
        public String Cl_Usuario { get; set; }
        
        public Distritos Distritos { get; set; }
        public Cantones Cantones { get; set; }
        public Provincias Provincias { get; set; }
        [DisplayName("Repetir Clave")]
        [NotMapped]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirmar password es requerido")]
        [Compare("Cl_Password", ErrorMessage = "Claves no coinciden.")]
        public String ConfirmPassowrd { get; set; }
        public String Cl_Latitude { get; set; }
        public String Cl_Longitud { get; set; }
    }
}