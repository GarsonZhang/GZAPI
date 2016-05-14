using Microsoft.VisualStudio.TestTools.UnitTesting;
using GZAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace GZAPI.Common.Tests
{
    [TestClass()]
    public class ConvertLibraryTests
    {
        [TestMethod()]
        public void Convert2ObjectTest()
        {
            //ConvertLibrary.Convert2Object()

            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("UserName", "张三");
                dic.Add("Age", 16);
                dic.Add("Money", 500.00);
                Data d = ConvertLibrary.Convert2Object<Data>(dic);
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            Assert.AreEqual("", "张三");
        }

        private class Data
        {
            public string UserName { get; set; }
            public int Age { get; set; }
            public decimal Money { get; set; }
            public DateTime? Birthday { get; set; }
        }
    }
}