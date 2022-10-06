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
    public class MClientes : IClientes
    {
        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;
        public MClientes()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion, SqlServerDialect.Provider);
        }
        public void ActualizarClientes(Clientes clientes)
        {
            _db = _conexion.Open();
            _db.Update(clientes);
        }

        public Clientes BuscarClientes(String Cl_Cedula)
        {
            _db = _conexion.Open();
            return _db.Select<Clientes>(x => x.Cl_Cedula == Cl_Cedula).FirstOrDefault();
        }

        public void EliminarClientes(String Cl_Cedula)
        {
            _db = _conexion.Open();
            _db.Delete<Clientes>(x => x.Cl_Cedula == Cl_Cedula);
        }

        public void InsertarClientes(Clientes clientes)
        {
            _db = _conexion.Open();
            _db.Insert(clientes);
        }

        public List<Clientes> ListarClientes()
        {
            _db = _conexion.Open();
            return _db.Select<Clientes>();
        }

        public bool BuscarClientes(string cl_user_name, string cl_password)
        {
            _db = _conexion.Open();
            return (_db.Count<Clientes>(x => x.Cl_Usuario == cl_user_name & x.Cl_Password == cl_password) > 0) ? true : false;
        }

        public Clientes BuscarClientePorLogin(string cl_user_name, string cl_password)
        {
            _db = _conexion.Open();
            return _db.Select<Clientes>(x => x.Cl_Usuario == cl_user_name & x.Cl_Password == cl_password).FirstOrDefault();
        }

        public Clientes BuscarClienteExistentePorUsuario(string usuario)
        {
            _db = _conexion.Open();
            return _db.Select<Clientes>(x => x.Cl_Usuario.ToLower().Trim() == usuario.ToLower().Trim()).FirstOrDefault();
        }
        public Clientes BuscarClienteExistentePorCedula(string cedula)
        {
            _db = _conexion.Open();
            return _db.Select<Clientes>(x =>x.Cl_Cedula == cedula).FirstOrDefault();
        }
        public Clientes BuscarClienteExistentePorCorreo(string correo)
        {
            _db = _conexion.Open();
            return _db.Select<Clientes>(x => x.Cl_Correo == correo).FirstOrDefault();
        }
    }
}
