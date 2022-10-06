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
    public class MFerias_Agricolas : IFerias_Agricolas
    {
        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;

        public MFerias_Agricolas()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion,
              SqlServerDialect.Provider);
        }

        public List<Ferias_Agricolas> ListarFerias_Agricolas()
        {
            _db = _conexion.Open();
            return _db.Select<Ferias_Agricolas>();
        }

        public Ferias_Agricolas BuscarFerias_Agricolas(int fa_id)
        {
            _db = _conexion.Open();
            return _db.Select<Ferias_Agricolas>(x => x.Fa_Id == fa_id).FirstOrDefault();
        }

        public void InsertarFerias_Agricolas(Ferias_Agricolas ferias_agricolas)
        {
            _db = _conexion.Open();
            _db.Insert(ferias_agricolas);
        }

        public void ActualizarFerias_Agricolas(Ferias_Agricolas ferias_agricolas)
        {
            _db = _conexion.Open();
            _db.Update(ferias_agricolas);
        }

        public void EliminarFerias_Agricolas(int fa_id)
        {
            _db = _conexion.Open();
            _db.Delete<Ferias_Agricolas>(x => x.Fa_Id == fa_id);
        }
    }
}
