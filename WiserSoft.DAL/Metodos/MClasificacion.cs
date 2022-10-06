using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DATA;
using WiserSoft.DAL.Interfaces;
using ServiceStack.OrmLite;
using System.Data;

namespace WiserSoft.DAL.Metodos
{
    public class MClasificacion : IClasificacion
    {
        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;

        public MClasificacion()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion,
                SqlServerDialect.Provider);
        }

        public void ActualizarClasificacion(Clasificacion clasificacion)
        {
            _db = _conexion.Open();
            _db.Update(clasificacion);
        }

        public Clasificacion BuscarClasificacion(int clasi_Id)
        {
            _db = _conexion.Open();
            return _db.Select<Clasificacion>(x => x.Clasi_Id == clasi_Id).FirstOrDefault();
        }

        public void EliminarClasificacion(int clasi_Id)
        {
            _db = _conexion.Open();
            _db.Delete<Clasificacion>(x => x.Clasi_Id == clasi_Id);
        }

        public void InsertarClasificacion(Clasificacion clasificacion)
        {
            _db = _conexion.Open();
            _db.Insert(clasificacion);
        }

        public List<Clasificacion> ListarClasificaciones()
        {
            _db = _conexion.Open();
            return _db.Select<Clasificacion>();
        }
    }
}
