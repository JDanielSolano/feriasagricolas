using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DATA;

namespace WiserSoft.DAL.Interfaces
{
    public interface IProductos_Por_Agricultor
    {
        List<Productos_Por_Agricultor> ListarProductos_Por_Agricultor();
        Productos_Por_Agricultor BuscarProductos_Por_Agricultor(int Ppa_Id);
        void InsertarProductos_Por_Agricultor(Productos_Por_Agricultor producto);
        void ActualizarProductos_Por_Agricultor(Productos_Por_Agricultor producto);
        void EliminarProductos_Por_Agricultor(int Ppa_Id);
    }
}
