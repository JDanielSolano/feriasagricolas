using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DATA;


namespace WiserSoft.DAL.Interfaces
{
    public interface IFerias_Agricolas
    {
        List<Ferias_Agricolas> ListarFerias_Agricolas();
        Ferias_Agricolas BuscarFerias_Agricolas(int fa_id);
        void InsertarFerias_Agricolas(Ferias_Agricolas ferias_agricolas);
        void ActualizarFerias_Agricolas(Ferias_Agricolas ferias_agricolas);
        void EliminarFerias_Agricolas(int fa_id);
    }
}
