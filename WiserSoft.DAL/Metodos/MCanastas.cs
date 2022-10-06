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
    public class MCanastas : ICanastas
    {
        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;

        public MCanastas()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion,
                SqlServerDialect.Provider);
        }
        public void ActualizarCanasta(Canastas canasta)
        {
            _db = _conexion.Open();
            _db.Update(canasta);
        }

        public Canastas BuscarCanasta(int _Can_Id)
        {
            _db = _conexion.Open();
            return _db.Select<Canastas>(x => x.Can_id == _Can_Id).FirstOrDefault();
        }

        public void EliminarCanasta(int _Can_Id)
        {
            _db = _conexion.Open();
            _db.Delete<Canastas>(x => x.Can_id == _Can_Id);
        }

        public void InsertarCanasta(Canastas canasta)
        {
            _db = _conexion.Open();
            _db.Insert(canasta);
        }

        public List<Canastas> ListarCanastas()
        {
            _db = _conexion.Open();
            return _db.Select<Canastas>();
        }
    }
}
