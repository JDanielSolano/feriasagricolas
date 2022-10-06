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
    public class MAgricultores : IAgricultores
    {
        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;

        public MAgricultores()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion,
                SqlServerDialect.Provider);
        }

        public void ActualizarAgricultor(Agricultores agricultor)
        {
            _db = _conexion.Open();
            _db.Update(agricultor);
        }

        public Agricultores BuscarAgricultor(String _Agr_Cedulad)
        {
            _db = _conexion.Open();
            return _db.Select<Agricultores>(x => x.Agr_Cedula == _Agr_Cedulad).FirstOrDefault();
        }

        public void EliminarAgricultor(String Agr_Cedula)
        {
            _db = _conexion.Open();
            _db.Delete<Agricultores>(x => x.Agr_Cedula == Agr_Cedula);
        }

        public void InsertarAgricultor(Agricultores agricultor)
        {
            _db = _conexion.Open();
            _db.Insert(agricultor);
        }

        public List<Agricultores> ListarAgricultores()
        {
            _db = _conexion.Open();
            return _db.Select<Agricultores>();
        }
    }
}
