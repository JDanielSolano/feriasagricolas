using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiserSoft.DATA
{
    public class Clasificacion
    {
        [AutoIncrement]
        public int Clasi_Id { get; set; }
        public String Clasi_Nombre { get; set; }
    }
}
