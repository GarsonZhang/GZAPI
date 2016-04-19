using GZFramework.DB.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZAPI.DataServices
{
    public class DataServiceBase
    {
        private IDatabase _db;
        protected IDatabase DB
        {
            get
            {
                if (_db == null)
                    _db = DatabaseFactory.CreateDataBaseEx("");
                return _db;
            }
        }
    }
}
