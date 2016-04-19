using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZAPI.Models
{
    public enum ErrorCodeItems
    {

        /// <summary>
        /// 接口标识不正确
        /// </summary>
        NoFoundInterface = 4404,

        /// <summary>
        /// Token过期
        /// </summary>
        TokenExpired = 5000,
        /// <summary>
        /// 非法Token
        /// </summary>
        TokenUnauthorized = 5001,

        /// <summary>
        /// 数据验证未通过
        /// </summary>
        DataVerifyFail = 5102,

        /// <summary>
        /// JSON格式不正确
        /// </summary>
        DataFormatError = 5500,

        /// <summary>
        /// 系统异常错误
        /// </summary>
        SystemErrCode = 5800,

        /// <summary>
        /// 其他错误,数据库直接返回的错误
        /// </summary>
        OtherErrCode = 5900
    }
}
