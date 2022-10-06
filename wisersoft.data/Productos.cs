using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiserSoft.DATA
{
    public class Productos
    {
        [AutoIncrement]
        public int Pdt_id { get; set; }

        public string Pdt_nombre { get; set; }

        public int Pdt_unidad_de_medida { get; set; }

        public decimal Pdt_precio { get; set; }

        public int Pdt_tipo { get; set; }

        public byte[] Pdt_foto { get; set; }
    }
}
