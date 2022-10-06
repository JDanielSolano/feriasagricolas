using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DATA;

namespace WiserSoft.DAL.Interfaces
{
    public interface IUsers
    {
        List<Users> ListarUsers();
        Users BuscarUsers(String us_user_name);
        void InsertarUsers(Users users);
        void ActualizarUsers(Users users);
        void EliminarUsers(String us_user_name);
        bool BuscarUsers(string us_user_name, string us_password); 
    }
}
