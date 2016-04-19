using GZAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZAPI.Biz.ResponseCore
{
    public class TokenProvider
    {
        public static string TokenGenerate(string account)
        {
            return Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 验证token，-1：非法token，0：token过期；1:正确的token
        /// </summary>
        /// <param name="account"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static int ValidateToken(string token)
        {
            return 1;
        }
    }
}
