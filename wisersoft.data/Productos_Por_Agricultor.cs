using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;

namespace WiserSoft.DATA
{
    public class Productos_Por_Agricultor
    {
        [AutoIncrement]
        public int Ppa_Id { get; set; }
        public int Ppa_Id_Producto { get; set; }
        public String Ppa_Id_Agricultor { get; set; }
    }
}
