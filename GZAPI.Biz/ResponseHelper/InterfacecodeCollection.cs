using GZAPI.Biz.ResponseCore;
using GZAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZAPI.Biz.ResponseHelper
{
    public class InterfacecodeCollection
    {
        private static Dictionary<int, RS> IntinceDic = new Dictionary<int, RS>();

        static InterfacecodeCollection()
        {
            IntinceDic.Add(1001, new RS(typeof(UserManage), UserManage.Register));//用户注册
            IntinceDic.Add(1002, new RS(typeof(UserManage), UserManage.Login));//登录
            IntinceDic.Add(1003, new RS(typeof(UserManage), UserManage.EditPwd));//修改密码

        }

        public static ResponseModel DoRun(RequestModel data)
        {
            ResponseModel result;

            if (data == null)
            {
                result = new ResponseModel();
                result.errcode = ErrorCodeItems.DataFormatError;
                return result;
            }

            if (IntinceDic.ContainsKey(data.interfacecode))
            {
                //IResource provider = (IResource)System.Activator.CreateInstance(IntinceDic[data.interfacecode], data);
                IManage provider = IntinceDic[data.interfacecode].GetObj(data);
                result = provider.DoCallResource();
            }
            else
            {
                //接口标识不正确
                result = new ResponseModel();
                result.errcode = ErrorCodeItems.NoFoundInterface;
            }
            return result;
        }
    }
}
