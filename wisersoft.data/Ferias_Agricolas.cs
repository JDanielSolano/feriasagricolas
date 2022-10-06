using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiserSoft.DATA
{
    public class Ferias_Agricolas
    {
        [AutoIncrement]
        public int Fa_Id { get; set; }
        public String Fa_Nombre { get; set; }
        public int Fa_Distrito { get; set; }
        public String Fa_Latitud { get; set; }
        public String Fa_longitud { get; set; }
    }
}
