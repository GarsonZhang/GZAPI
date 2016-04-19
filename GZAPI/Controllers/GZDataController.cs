using GZAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GZAPI.Controllers
{
    public class GZDataController : ApiController
    {
        [HttpPost]
        ///同步处理
        public ResponseModel Post(RequestModel data)
        {
            //System.Web.HttpContext.Current.Application[]
            try
            {
                return Biz.ResponseHelper.InterfacecodeCollection.DoRun(data);
            }
            catch (Exception ex)
            {
                ResponseModel Response = new ResponseModel();
                Response.errcode = ErrorCodeItems.SystemErrCode;
                Response.errmsg = String.Format("系统异常：{0}", ex.Message);
                return Response;
            }
        }

        /* 异步处理
      [HttpPost]
      [ActionName("PostAsync")]
      //异步处理
      public async Task<responsedata> PostAsync(requiredata value)
      {
          //var data = await GetPlaceAsync(value);
          //return data;

          var tk = Task.Run(() =>
          {
              return GetPlace(value);
          });

          await Task.WhenAll(tk);

          return tk.Result;
      }


      async Task<responsedata> GetPlaceAsync(requiredata json)
      {
          var tk = Task.Run(() =>
          {
              return GetPlace(json);
          });

          await Task.WhenAll(tk);

          return tk.Result;

      }

      */
    }
}
