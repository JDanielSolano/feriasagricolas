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
    public class MProductos_Por_Agricultor : IProductos_Por_Agricultor
    {
        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;

        public MProductos_Por_Agricultor()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion,
                SqlServerDialect.Provider);
        }
        public void ActualizarProductos_Por_Agricultor(Productos_Por_Agricultor producto)
        {
            _db = _conexion.Open();
            _db.Update(producto);
        }

        public Productos_Por_Agricultor BuscarProductos_Por_Agricultor(int Ppa_Id)
        {
            _db = _conexion.Open();
            return _db.Select<Productos_Por_Agricultor>(x => x.Ppa_Id == Ppa_Id).FirstOrDefault();
        }

        public void EliminarProductos_Por_Agricultor(int Ppa_Id)
        {
            _db = _conexion.Open();
            _db.Delete<Productos_Por_Agricultor>(x => x.Ppa_Id == Ppa_Id);
        }

        public void InsertarProductos_Por_Agricultor(Productos_Por_Agricultor producto)
        {
            _db = _conexion.Open();
            _db.Insert(producto);
        }

        public List<Productos_Por_Agricultor> ListarProductos_Por_Agricultor()
        {
            _db = _conexion.Open();
            return _db.Select<Productos_Por_Agricultor>();
        }
    }
}
