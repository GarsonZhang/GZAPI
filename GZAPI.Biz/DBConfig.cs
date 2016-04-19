using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZAPI.Biz
{
    public class DBConfig
    {
        public static void InitDB(string connectionstr)
        {
            GZAPI.DataServices.DBConfig.InitDB(connectionstr);
        }
    }
}
