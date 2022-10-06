using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiserSoft.DATA
{
    public class Compras
    {
        [AutoIncrement]
        public int Com_Id { get; set; }
        public int Com_Id_Canasta { get; set; }
        public int Com_Id_Producto_Por_Agricultor { get; set; }
        public int Com_Cantidad { get; set; }
    }
}
