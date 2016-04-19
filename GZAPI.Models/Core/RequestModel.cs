using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZAPI.Models
{
    /// <summary>
    /// 请求实体
    /// </summary>
    public class RequestModel
    {
        /// <summary>
        /// 令牌
        /// </summary>
        public string token { get; set; }
       
        /// <summary>
        /// 业务系统
        /// </summary>
        public string client { get; set; }

        /// <summary>
        /// 接口标识
        /// </summary>
        public int interfacecode { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
    }
}
