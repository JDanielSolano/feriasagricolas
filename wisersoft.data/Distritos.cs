using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiserSoft.DATA
{
    public class Distritos
    {
        [AutoIncrement]
        public int dis_id { get; set; }

        public int dis_canton { get; set; }

        public string dis_nombre { get; set; }
    }
}
