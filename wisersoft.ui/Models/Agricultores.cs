using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WiserSoft.UI.Models
{
    public class Agricultores
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Cédula es requerido")]
        [MinLength(9, ErrorMessage = "Debe contener mínimo 9 caracteres")]
        public String Agr_Cedula { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Nombre es requerido")]
        [MinLength(2, ErrorMessage = "Debe contener mínimo 2 caracteres")]
        [MaxLength(100, ErrorMessage = "Debe contener máximo 100 caracteres")]
        public String Agr_Nombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Apellido es requerido")]
        [MinLength(2, ErrorMessage = "Debe contener mínimo 2 caracteres")]
        [MaxLength(100, ErrorMessage = "Debe contener máximo 100 caracteres")]
        public String Agr_Apellidos { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El alias es requerido")]
        [MinLength(2, ErrorMessage = "Debe contener mínimo 2 caracteres")]
        [MaxLength(100, ErrorMessage = "Debe contener máximo 100 caracteres")]
        public String Agr_Alias { get; set; }

        public String Agr_Descripcion { get; set; }
        public int Agr_Distrito { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Teléfono es requerido")]
        [Phone(ErrorMessage = "Debe ingresar un número válido")]
        [MinLength(8, ErrorMessage = "Debe contener mínimo 8 caracteres")]
        public String Agr_Telefono { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Teléfono es requerido")]
        public int Agr_Feria_A_La_Que_Asiste { get; set; }

        public byte[] Agr_Foto { get; set; }

        public int Agr_Estado { get; set; }

        public Distritos Distritos { get; set; }
        public Cantones Cantones { get; set; }
        public Provincias Provincias { get; set; }

        public Ferias_Agricolas Ferias_Agricolas { get; set; }

        public List<Productos_Por_Agricultor> Productos { get; set; }
    }
}