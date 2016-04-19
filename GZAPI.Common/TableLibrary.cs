using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZAPI.Common
{
    public class TableLibrary
    {
        public static string GetColumnValues(DataTable dt, string ColumnName)
        {
            StringBuilder str = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                str.Append("," + dr[ColumnName]);
            }
            if (str.Length > 0)
                str.Remove(0, 1);
            return str.ToString();
        }
    }
}
