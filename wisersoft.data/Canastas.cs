using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiserSoft.DATA
{
    public class Canastas
    {
        [AutoIncrement]
        public int Can_id { get; set; }
        public string Can_nombre { get; set; }
        public DateTime Can_fecha_de_creacion { get; set; }
        public string Can_usuario { get; set; }
        public int Can_feria { get; set; }

    }
}
