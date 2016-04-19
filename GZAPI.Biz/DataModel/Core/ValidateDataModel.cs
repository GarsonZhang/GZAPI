using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GZAPI.Biz.DataModel
{
    public class ValidateDataModel
    {
        public static bool Validate(object o, out string Msg)
        {
            bool success = true;
            StringBuilder str = new StringBuilder();
            PropertyInfo[] propertys = o.GetType().GetProperties();
            foreach (PropertyInfo p in propertys)
            {
                DataModelValidateBaseAttribute v = p.GetCustomAttribute<DataModelValidateBaseAttribute>(false);
                if (v == null) continue;
                if (v.doValidate(p.GetValue(o)) == false)
                {
                    str.Append(v.ErrorMsg+";");
                    success = success & false;
                }
                else
                {
                    success = success & true;
                }
            }
            if (str.Length > 0)
                Msg = str.ToString(0, str.Length - 1);
            else Msg = "";
            return success;
        }
    }
}
