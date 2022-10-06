using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DATA;

namespace WiserSoft.DAL.Interfaces
{
    public interface IClasificacion
    { 
        List<Clasificacion> ListarClasificaciones();
        Clasificacion BuscarClasificacion(int clasi_Id);
        void InsertarClasificacion(Clasificacion clasificacion);
        void ActualizarClasificacion(Clasificacion clasificacion);
        void EliminarClasificacion(int clasi_Id);
    }
}
