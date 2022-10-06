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
    public class MUsers :IUsers
    {
        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;

        public MUsers()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion,
              SqlServerDialect.Provider);
        }

        public List<Users> ListarUsers()
        {
            _db = _conexion.Open();
            return _db.Select<Users>();
        }

        public Users BuscarUsers(string us_user_name)
        {
            _db = _conexion.Open();
            return _db.Select<Users>(x => x.Us_User_Name == us_user_name).FirstOrDefault();
        }

        public void InsertarUsers(Users users)
        {
            _db = _conexion.Open();
            _db.Insert(users);
        }

        public void ActualizarUsers(Users users)
        {
            _db = _conexion.Open();
            _db.Update(users);
        }

        public void EliminarUsers(string us_user_name)
        {
            _db = _conexion.Open();
            _db.Delete<Users>(x => x.Us_User_Name == us_user_name);
        }

        public bool BuscarUsers(string us_user_name, string us_password)
        {
            _db = _conexion.Open();
            return (_db.Count<Users>(x => x.Us_User_Name == us_user_name & x.Us_Password == us_password) > 0) ? true : false;
        }
    }
}
