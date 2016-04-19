using GZAPI.Biz.ResponseCore;
using GZAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZAPI.Biz.ResponseHelper
{
    public class RS
    {

        /// <summary>
        /// 操作类型
        /// </summary>
        public int Opt { get; private set; }

        public Type t;
        public RS(Type type)
        {
            t = type;
        }
        public RS(Type type,int opt)
        {
            t = type;
            Opt = opt;
        }

        public IManage GetObj(RequestModel data)
        {
            if (Opt > 0)
                return (IManage)System.Activator.CreateInstance(t, data, Opt);
            else
                return (IManage)System.Activator.CreateInstance(t, data);
        }
    }
}
