using GZFramework.DB.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZAPI.DataServices
{
    public class DBConfig
    {
        public static void InitDB(string connectionstr)
        {
            GZFramework.DB.Core.Config.DBConfig = new DBC(connectionstr);
        }
    }



    internal class DBC : IDBConfig
    {
        IDatabase db;

        public DBC(string connectionstr)
        {
            

            const string ProviderName = "System.Data.SqlClient";
            bool b = DatabaseFactory.Validate(connectionstr, ProviderName);

            db = DatabaseFactory.CreateDatabase(connectionstr, ProviderName);

        }


        public IDatabase GetDBConnectionInfo(string DBCode)
        {
            return db;
        }


        public void RefreshDBList()
        {

        }
    }
}
