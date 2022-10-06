using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DATA;

namespace WiserSoft.DAL.Interfaces
{
    public interface IProductos
    {
        List<Productos> ListarProductos();
        Productos BuscarProductos(int Ptd_Id);
        void InsertarProductos(Productos Producto);
        void ActualizarProductos(Productos Producto);
        void EliminarProductos(int Ptd_Id);
    }
}
