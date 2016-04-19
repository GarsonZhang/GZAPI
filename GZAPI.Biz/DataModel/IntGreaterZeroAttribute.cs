using System;
using System.Collections.Generic;
using System.Linq;

namespace GZAPI.Biz.DataModel
{
    /// <summary>
    /// 大于0的正整数
    /// </summary>
    public class IntGreaterZeroAttribute : DataModelValidateBaseAttribute
    {

        public override bool doValidate(object obj)
        {
            try
            {
                return Convert.ToInt32(obj) > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
