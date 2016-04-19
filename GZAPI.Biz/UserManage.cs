using GZAPI.Biz.ResponseCore;
using System;
using System.Collections.Generic;
using System.Linq;
using GZAPI.Models;
using GZAPI.Biz.DataModel;
using GZAPI.DataServices;

namespace GZAPI.Biz
{

    internal class UserManage : IManage
    {

        internal const int Login = 1;
        internal const int EditPwd = 2;
        internal const int Register = 3;

        
        IManage manage;
        public UserManage(RequestModel data, int type)
        {

            switch (type)
            {
                case Login:
                    manage = new UserLoginManage(data);
                    break;
                case EditPwd:
                    manage = new UserEditPwdManage(data);
                    break;
                case Register:
                    manage = new UserRegisterManage(data);
                    break;
                default:
                    manage = null;
                    break;
            }
        }

        public ResponseModel DoCallResource()
        {
            if (manage == null)
            {
                ResponseModel data = new ResponseModel();
                data.errcode = ErrorCodeItems.NoFoundInterface;
                return data;
            }
            return manage.DoCallResource();
        }
    }

    internal class UserLoginManage : ManageBase
    {
        UserService dataService;
        public UserLoginManage(RequestModel data)
                : base(data)
        {
            dataService = new UserService();
        }

        DataModel data = null;

        public override object doJsonFormat(out bool success)
        {
            try
            {
                data = (Request.Data as Newtonsoft.Json.Linq.JObject).ToObject<DataModel>();
                success = true;
            }
            catch
            {
                success = false;
            }
            return data;
        }


        protected override void GetResource()
        {

            UserModel user = dataService.GetUser(data.UserID, data.Pwd);

            if (user == null)
            {
                Response.errcode = ErrorCodeItems.OtherErrCode;
                Response.errmsg = "用户名或密码不正确";
            }
            else
            {
                Response.errcode = 0;
                Response.data1 = user.GetResponse();
            }
        }


        public class DataModel
        {
            [StringNotNull(ErrorMsg = "UserID不能为空！")]
            public string UserID { get; set; }
            [StringNotNull(ErrorMsg = "Pwd不能为空！")]
            public string Pwd { get; set; }

        }
    }

    internal class UserEditPwdManage : ManageBase
    {
        UserService dataService;
        public UserEditPwdManage(RequestModel data)
                : base(data)
        {
            dataService = new UserService();
        }

        DataModel data = null;

        public override object doJsonFormat(out bool success)
        {
            try
            {
                data = (Request.Data as Newtonsoft.Json.Linq.JObject).ToObject<DataModel>();
                success = true;
            }
            catch
            {
                success = false;
            }
            return data;
        }


        protected override void GetResource()
        {


            bool success = dataService.EditPwd(data.UserID, data.OldPwd, data.NewPwd);

            if (success == false)
            {
                Response.errcode = ErrorCodeItems.OtherErrCode;
                Response.errmsg = "用户名或密码不正确";
            }
            else
            {
                Response.errcode = 0;
            }
        }


        public class DataModel
        {
            [StringNotNull(ErrorMsg = "UserID不能为空！")]
            public string UserID { get; set; }

            [StringNotNull(ErrorMsg = "Pwd不能为空！")]
            public string OldPwd { get; set; }

            [StringNotNull(ErrorMsg = "Pwd不能为空！")]
            public string NewPwd { get; set; }

        }
    }

    internal class UserRegisterManage : ManageBase
    {
        UserService dataService;
        public UserRegisterManage(RequestModel data)
                : base(data)
        {
            dataService = new UserService();
        }

        DataModel data = null;

        public override object doJsonFormat(out bool success)
        {
            try
            {
                data = (Request.Data as Newtonsoft.Json.Linq.JObject).ToObject<DataModel>();
                success = true;
            }
            catch
            {
                success = false;
            }
            return data;
        }


        protected override void GetResource()
        {


            UserModel user = dataService.Add(new UserModel() { UserID = data.UserID, Pwd = data.Pwd, UserName = data.UserName });

            if (user == null)
            {
                Response.errcode = ErrorCodeItems.OtherErrCode;
            }
            else
            {
                Response.errcode = 0;
                Response.data1 = user.GetResponse();
            }
        }


        public class DataModel
        {
            [StringNotNull(ErrorMsg = "UserID不能为空！")]
            public string UserID { get; set; }

            [StringNotNull(ErrorMsg = "Pwd不能为空！")]
            public string Pwd { get; set; }

            [StringNotNull(ErrorMsg = "UserName不能为空！")]
            public string UserName { get; set; }

                        

        }
    }
}
