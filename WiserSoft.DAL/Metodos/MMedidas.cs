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
    public class MMedidas : IMedidas
    {
        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;
        public MMedidas()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion, SqlServerDialect.Provider);
        }
        public void ActualizarMedidas(Medidas medidas)
        {
            _db = _conexion.Open();
            _db.Update(medidas);
        }

        public Medidas BuscarMedidas(int Mdd_Id)
        {
            _db = _conexion.Open();
            return _db.Select<Medidas>(x => x.Mdd_Id == Mdd_Id).FirstOrDefault();
        }

        public void EliminarMedidas(int Mdd_Id)
        {
            _db = _conexion.Open();
            _db.Delete<Medidas>(x => x.Mdd_Id == Mdd_Id);
        }

        public void InsertarMedidas(Medidas medidas)
        {
            _db = _conexion.Open();
            _db.Insert(medidas);
        }

        public List<Medidas> ListarMedidas()
        {
            _db = _conexion.Open();
            return _db.Select<Medidas>();
        }
    }
}
