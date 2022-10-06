using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DATA;

namespace WiserSoft.DAL.Interfaces
{
    public interface ICanastas
    {
        List<Canastas> ListarCanastas();
        Canastas BuscarCanasta(int Can_Id);
        void InsertarCanasta(Canastas canasta);
        void ActualizarCanasta(Canastas canasta);
        void EliminarCanasta(int Can_Id);
    }
}
