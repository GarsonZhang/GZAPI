using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZAPI.Models
{
    public class UserModel
    {
        public string UserID { get; set; }
        public string Pwd { get; set; }
        public string UserName { get; set; }

        public object GetResponse()
        {
            return this;
        }
    }
}
