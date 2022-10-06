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
    public class MDistritos : IDistritos
    {
        private OrmLiteConnectionFactory _conexion;
        private IDbConnection _db;

        public MDistritos()
        {
            _conexion = new OrmLiteConnectionFactory(BD.Default.conexion,
                SqlServerDialect.Provider);
        }
        public List<Distritos> ListarDistritos()
        {
            _db = _conexion.Open();
            return _db.Select<Distritos>();
        }
    }
}
