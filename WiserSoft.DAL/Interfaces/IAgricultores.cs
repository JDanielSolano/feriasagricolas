using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DATA;

namespace WiserSoft.DAL.Interfaces
{
    public interface IAgricultores
    {
        List<Agricultores> ListarAgricultores();
        Agricultores BuscarAgricultor(String Agr_Cedula);
        void InsertarAgricultor(Agricultores agricultor);
        void ActualizarAgricultor(Agricultores agricultor);
        void EliminarAgricultor(String Agr_Cedula);
    }
}
