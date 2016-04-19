using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZAPI.Biz.DataModel
{
    /// <summary>
    /// 不能为空字符串
    /// </summary>
    public class StringNotNullAttribute : DataModelValidateBaseAttribute
    {

        public override bool doValidate(object obj)
        {
            if (obj == null) return false;
            return !(Object.Equals(obj, String.Empty));
        }
    }
}
