using GZAPI.Common;
using GZAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZAPI.DataServices
{
    public class UserService : DataServiceBase
    {

        public UserModel GetUser(string Account, string Pwd)
        {
            const string sql = "SELECT * FROM dbo.tb_User WHERE UserID=@UserID AND Pwd=@Pwd";

            SqlParameterProvider Params = new SqlParameterProvider();
            Params.AddParameter("@UserID", SqlDbType.VarChar, 20, Account);
            Params.AddParameter("@Pwd", SqlDbType.VarChar, 20, Pwd);

            UserModel data = null;
            DB.ExecuteDataReader(sql, Params, row =>
            {
                data = ConvertLibrary.Convert2Object<UserModel>(row);
            });

            return data;
        }

        public UserModel Add(UserModel data)
        {
            const string sql = "INSERT INTO dbo.tb_User(UserID,Pwd,UserName) VALUES(@UserID,@Pwd,@UserName)";

            SqlParameterProvider Params = new SqlParameterProvider();
            Params.AddParameter("@UserID", SqlDbType.VarChar, 20, data.UserID);
            Params.AddParameter("@Pwd", SqlDbType.VarChar, 20, data.Pwd);
            Params.AddParameter("@UserName", SqlDbType.VarChar, 20, data.UserName);
            DB.ExecuteNonQuery(sql, Params);
            return data;
        }

        public bool EditPwd(string Account, string OldPwd, string NewPwd)
        {
            const string sql = "UPDATE dbo.tb_User SET Pwd=@NewPwd WHERE UserID=@UserID AND Pwd=@OldPwd";
            SqlParameterProvider Params = new SqlParameterProvider();
            Params.AddParameter("@UserID", SqlDbType.VarChar, 20, Account);
            Params.AddParameter("@OldPwd", SqlDbType.VarChar, 20, OldPwd);
            Params.AddParameter("@NewPwd", SqlDbType.VarChar, 20, NewPwd);
            return DB.ExecuteNonQuery(sql, Params) > 0;
        }
    }
}
