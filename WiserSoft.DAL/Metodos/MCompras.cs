using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DAL.Interfaces;
using WiserSoft.DATA;

namespace WiserSoft.DAL.Metodos
{
    public class MCompras : ICompras
    {
        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;

        public MCompras()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion,
                SqlServerDialect.Provider);
        }
        public void ActualizarCompras(Compras Compras)
        {
            _db = _conexion.Open();
            _db.Update(Compras);
        }

        public Compras BuscarCompras(int Com_Id)
        {
            _db = _conexion.Open();
            return _db.Select<Compras>(x => x.Com_Id == Com_Id).FirstOrDefault();
        }

        public void EliminarCompras(int Com_Id)
        {
            _db = _conexion.Open();
            _db.Delete<Compras>(x => x.Com_Id == Com_Id);
        }

        public void InsertarCompras(Compras Compras)
        {
            _db = _conexion.Open();
            _db.Insert(Compras);
        }

        public List<Compras> ListarCompras()
        {
            _db = _conexion.Open();
            return _db.Select<Compras>();
        }

        public List<Compras> ListarComprasPorCanasta(int id_canasta)
        {
            _db = _conexion.Open();
            return _db.Select<Compras>().Where(x => x.Com_Id_Canasta == id_canasta).ToList();
        }
    }
}
