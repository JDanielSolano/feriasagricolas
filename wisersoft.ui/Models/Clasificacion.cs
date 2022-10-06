﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WiserSoft.UI.Models
{
    public class Clasificacion
    {
        public int Clasi_Id { get; set; }

        [Required(ErrorMessage = "Nombre clasificación es requerido")]
        [MinLength(2, ErrorMessage = "El número mínimo de caracteres es de 2")]
        [MaxLength(100, ErrorMessage = "El número máximo de caracteres es de 100")]
        public String Clasi_Nombre { get; set; }

        public int usadoEnFiltro { get; set; }
    }
}