using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiserSoft.DATA
{
    public class Medidas
    {
        [AutoIncrement]
        public int Mdd_Id { get; set; }
        public String Mdd_Nombre { get; set; }
    }
}
