using System;
using System.Collections.Generic;
using System.Linq;

namespace GZAPI.Biz.DataModel
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class DataModelValidateBaseAttribute : Attribute
    {
        public string ErrorMsg { get; set; }

        public abstract bool doValidate(object obj);
    }
}
