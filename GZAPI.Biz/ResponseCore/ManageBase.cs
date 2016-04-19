using GZAPI.Biz.DataModel;
using GZAPI.Common;
using GZAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZAPI.Biz.ResponseCore
{
    public abstract class ManageBase : IManage
    {

        /// <summary>
        /// 请求对象
        /// </summary>
        protected RequestModel Request { get; private set; }

        protected ResponseModel Response { get; private set; }


        public ManageBase(RequestModel data)
        {
            Request = data;
            Response = new ResponseModel();
        }


        public abstract object doJsonFormat(out bool success);
        public virtual bool doValidateData(object ReceiveData)
        {
            string errormsg = "";
            bool success = ValidateDataModel.Validate(ReceiveData, out errormsg);
            
            if (success == false)
            {
                Response.errcode = ErrorCodeItems.DataVerifyFail;
                Response.errmsg = errormsg;
            }
            return success;
        }

        public bool ValidaToken = false;

        public ResponseModel DoCallResource()
        {
            //判断请求是否为空
            if (Request == null)
            {
                Response.errcode = ErrorCodeItems.DataFormatError;
                return Response;
            }

            //验证Token
            if ((ValidaToken == true) && (DoValidateToken() == false))//验证token
                return Response;

            bool success = false;
            object ReceiveData = doJsonFormat(out success);
            if (success == false)//接收数据为空
            {
                Response.errcode = ErrorCodeItems.DataFormatError;
                return Response;
            }
            if (ReceiveData != null)
            {
                if (doValidateData(ReceiveData) == false)//数据验证
                {
                    return Response;
                }
            }

            GetResource();
            return Response;
        }

        protected abstract void GetResource();



        //protected bool ContainsKeyAndNotDefult(string key)
        //{
        //    bool success = Request.data1.ContainsKey(key);
        //    if (success == true)
        //    {
        //        object o = Request.data1[key];
        //        Type type = o.GetType();

        //        object defult = o.GetType().IsValueType ? Activator.CreateInstance(type) : null;
        //        if (object.Equals(o, defult))
        //        {
        //            success = false;
        //        }
        //    }
        //    return success;
        //}

        //protected bool ContainsKey(string key)
        //{
        //    return Request.data1.ContainsKey(key);
        //}


        //protected T GetKeyValue<T>(string key)
        //{
        //    return Tools.GetKey<T>(Request.data1, key);
        //}


        public bool DoValidateToken()
        {
            bool success = !String.IsNullOrEmpty(Request.token);
            int r = GZAPI.Biz.ResponseCore.TokenProvider.ValidateToken(Request.token);
            if (r == 0)
            {
                Response.errcode = ErrorCodeItems.TokenExpired;
            }
            if (r == -1)
            {
                Response.errcode = ErrorCodeItems.TokenUnauthorized;
            }

            return r == 1;
        }
    }
}
