using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GZAPI.Common
{
    public class Tools
    {
        public static T GetKey<T>(Dictionary<string, object> data, string key)
        {
            try
            {
                if (data.ContainsKey(key))
                {
                    return ConvertLibrary.ConvertValue<T>(data[key]);
                }
                else
                    return default(T);
            }
            catch (Exception ex)
            {
                return default(T);


            }
        }

      


    }
}
