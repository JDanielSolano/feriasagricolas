using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DATA;

namespace WiserSoft.DAL.Interfaces
{
   public interface IMedidas
    {
        List<Medidas> ListarMedidas();
        Medidas BuscarMedidas(int Mdd_Id);
        void InsertarMedidas(Medidas medidas);
        void ActualizarMedidas(Medidas medidas);
        void EliminarMedidas(int Mdd_Id);
    }
}
