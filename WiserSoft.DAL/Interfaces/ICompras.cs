using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DATA;

namespace WiserSoft.DAL.Interfaces
{
    public interface ICompras
    {
        List<Compras> ListarCompras();
        List<Compras> ListarComprasPorCanasta(int id_canasta);
        Compras BuscarCompras(int Com_Id);
        void InsertarCompras(Compras Compras);
        void ActualizarCompras(Compras Compras);
        void EliminarCompras(int Com_Id);
    }
}
