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
    public class MCantones:ICantones
    {
        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;

        public MCantones()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion,
                SqlServerDialect.Provider);
        }

        
        public List<Cantones> ListarCantones()
        {
            _db = _conexion.Open();
            return _db.Select<Cantones>();
        }
    }
}
