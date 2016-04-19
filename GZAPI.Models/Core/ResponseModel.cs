using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZAPI.Models
{
    /// <summary>
    /// 响应实体
    /// </summary>
    public class ResponseModel
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public ErrorCodeItems errcode { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string errmsg { get; set; }

        public object data1 { get; set; }
    }
}
