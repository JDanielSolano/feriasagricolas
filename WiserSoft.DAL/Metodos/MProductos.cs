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
    public class MProductos : IProductos
    {
        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;

        public MProductos()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion,
                SqlServerDialect.Provider);
        }
        public void ActualizarProductos(Productos Producto)
        {
            _db = _conexion.Open();
            _db.Update(Producto);
        }

        public Productos BuscarProductos(int Ptd_Id)
        {
            _db = _conexion.Open();
            return _db.Select<Productos>(x => x.Pdt_id == Ptd_Id).FirstOrDefault();
        }

        public void EliminarProductos(int Ptd_Id)
        {
            _db = _conexion.Open();
            _db.Delete<Productos>(x => x.Pdt_id == Ptd_Id);
        }

        public void InsertarProductos(Productos Producto)
        {
            _db = _conexion.Open();
            _db.Insert(Producto);
        }

        public List<Productos> ListarProductos()
        {
            _db = _conexion.Open();
            return _db.Select<Productos>();
        }
    }
}
