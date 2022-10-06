using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiserSoft.DATA
{
    public class Agricultores
    {
        public String Agr_Cedula { get; set; }
        public String Agr_Nombre { get; set; }
        public String Agr_Apellidos { get; set; }
        public String Agr_Alias { get; set; }
        public String Agr_Descripcion { get; set; }
        public int Agr_Distrito { get; set; }
        public String Agr_Telefono { get; set; }
        public int Agr_Feria_A_La_Que_Asiste { get; set; }
        public byte[] Agr_Foto { get; set; }
        public int Agr_Estado { get; set; }

    }
}
