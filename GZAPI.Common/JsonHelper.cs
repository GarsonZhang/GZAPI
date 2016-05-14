using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZAPI.Common
{
    public static class JsonExtensions
    {

        #region array
        public static ArrayList ToArrayList(this DataTable dt)
        {
            ArrayList dic = new ArrayList();
            foreach (DataRow row in dt.Rows)
            {
                Dictionary<string, object> drow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    drow.Add(col.ColumnName, row[col.ColumnName]);
                }
                dic.Add(drow);
            }
            return dic;
        }

        /// <summary>
        /// 转json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static ArrayList ToArrayList(this DataSet ds)
        {
            ArrayList array = new ArrayList();
            foreach (DataTable dt in ds.Tables)
            {
                ArrayList dtarray = new ArrayList();
                foreach (DataRow row in dt.Rows)
                {
                    Dictionary<string, object> drow = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        drow.Add(col.ColumnName, row[col.ColumnName]);
                    }
                    dtarray.Add(drow);
                }


                Dictionary<string, object> dicdt = new Dictionary<string, object>();
                dicdt.Add(dt.TableName, dtarray);
                array.Add(dicdt);
            }

            return array;
        }

        #endregion


        #region 主从级关系
        public static string ToJson(this DataTable dt, string KeyField, string ParentKeyField, params string[] DisplayFieldNames)
        {
            var lst = ToArraryList(dt, KeyField, ParentKeyField, DisplayFieldNames);

            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            return jss.Serialize(lst);

        }

        public static ArrayList ToArraryList(this DataTable dt, string KeyField, string ParentKeyField, params string[] DisplayFieldNames)
        {
            DataTable data = dt.Copy();

            Dictionary<object, JsonModel> dicModel = new Dictionary<object, JsonModel>();
            List<JsonModel> result = new List<JsonModel>();
            foreach (DataRow dr in data.DefaultView.ToTable().Rows)
            {

                object key = dr[KeyField];
                if (!dicModel.ContainsKey(key))
                {
                    JsonModel newJsonModel = new JsonModel();
                    dicModel.Add(key, newJsonModel);
                    result.Add(newJsonModel);
                }

                JsonModel Model = dicModel[key];

                foreach (string fieldname in DisplayFieldNames)
                {
                    Model.MainBody.Add(fieldname, dr[fieldname]);
                }


                object Pkey = dr[ParentKeyField];
                if (!dicModel.ContainsKey(Pkey))
                {
                    JsonModel newJsonModel = new JsonModel();
                    dicModel.Add(Pkey, newJsonModel);
                    result.Add(newJsonModel);
                }
                dicModel[Pkey].Data.Add(Model);
                result.Remove(Model);
            }

            ArrayList lst = new ArrayList();

            foreach (JsonModel model in result)
            {
                if (model.MainBody.Count == 0)
                {
                    foreach (JsonModel m in model.Data)
                        lst.Add(JsonModel2Helper2(m));
                }
                else
                    lst.Add(JsonModel2Helper2(model));
            }


            return lst;

        }


        private static Dictionary<string, object> JsonModel2Helper2(JsonModel model)
        {
            Dictionary<string, object> dic = model.MainBody;


            if (model.Data.Count > 0)
            {
                ArrayList lstDetail = new ArrayList();
                foreach (JsonModel m in model.Data)
                {
                    lstDetail.Add(JsonModel2Helper2(m));
                }

                dic.Add("Data", lstDetail);
            }


            return dic;
        }

        private class JsonModel
        {
            public object Key { get; set; }
            public Dictionary<string, object> MainBody = new Dictionary<string, object>();
            public List<JsonModel> Data = new List<JsonModel>();
        }

        /// <summary>
        /// 主从表按主字段父子级关系
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="KeyField"></param>
        /// <param name="ParentKeyField"></param>
        private static void DataTable2TreeData(this DataTable data, string KeyField, string ParentKeyField, string TreeNodeName)
        {
            if (!data.Columns.Contains("garsonzhangbkpkey"))
                data.Columns.Add("garsonzhangbkpkey", data.Columns[ParentKeyField].DataType);
            if (!data.Columns.Contains(TreeNodeName))
                data.Columns.Add(TreeNodeName, typeof(System.Int32));

            List<DataRow> lstdr = new List<DataRow>();
            lstdr.AddRange(data.Select());

            foreach (DataRow dr in lstdr)
            {
                dr["garsonzhangbkpkey"] = dr[ParentKeyField];
                dr[TreeNodeName] = 1;
            }


            while (lstdr.Count > 0)
            {
                for (int i = lstdr.Count - 1; i >= 0; i--)
                {
                    DataRow dr = lstdr[i];
                    DataRow[] tmpdrs = data.Select(String.Format("{0}='{1}'", KeyField, dr["garsonzhangbkpkey"]));
                    if (tmpdrs.Length > 0)
                    {
                        dr["garsonzhangbkpkey"] = tmpdrs[0][ParentKeyField];
                        dr[TreeNodeName] = (int)dr[TreeNodeName] + 1;
                    }
                    else
                        lstdr.Remove(dr);
                }
            }

            data.Columns.Remove("garsonzhangbkpkey");
        }

        #endregion

        public static string ToJson(this DataTable dt)
        {

            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            ArrayList dic = new ArrayList();
            foreach (DataRow row in dt.Rows)
            {
                Dictionary<string, object> drow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    drow.Add(col.ColumnName, row[col.ColumnName]);
                }
                dic.Add(drow);
            }
            return jss.Serialize(dic);
        }


        /// <summary>
        /// 转json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToJson(this DataSet ds)
        {
            ArrayList array = new ArrayList();
            foreach (DataTable dt in ds.Tables)
            {
                ArrayList dtarray = new ArrayList();
                foreach (DataRow row in dt.Rows)
                {
                    Dictionary<string, object> drow = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        drow.Add(col.ColumnName, row[col.ColumnName]);
                    }
                    dtarray.Add(drow);
                }


                Dictionary<string, object> dicdt = new Dictionary<string, object>();
                dicdt.Add(dt.TableName, dtarray);
                array.Add(dicdt);
            }

            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            return jss.Serialize(array);

        }




        /// <summary>    
        /// Json转DataTable    
        /// </summary>    
        /// <param name="JsonStr"></param>    
        /// <returns></returns>
        public static DataTable Json2Dt(string JsonStr)
        {
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            ArrayList dic = jss.Deserialize<ArrayList>(JsonStr);
            DataTable data = new DataTable();
            if (dic.Count > 0)
            {
                foreach (Dictionary<string, object> drow in dic)
                {
                    if (data.Columns.Count == 0)
                    {
                        foreach (string key in drow.Keys)
                        {
                            data.Columns.Add(key, drow[key].GetType());
                        }
                    }
                    DataRow row = data.NewRow();
                    foreach (string key in drow.Keys)
                    { row[key] = drow[key]; }
                    data.Rows.Add(row);
                }
            }
            return data;
        }

        /// <summary>
        /// json字符串转换对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JsonStr"></param>
        /// <returns></returns>
        public static T Json2Object<T>(string JsonStr)
        {
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();

            return jss.Deserialize<T>(JsonStr);

        }

        /// <summary>
        /// json转dynamic对象
        /// </summary>
        /// <param name="JsonStr"></param>
        /// <returns></returns>
        public static dynamic Json2Dynamic(string JsonStr)
        {
            return System.Web.Helpers.Json.Decode(JsonStr);
        }

    }


    /* 旧的废弃掉的类库
    public static class CustomExpand
    {
        public static string ToJson(this DataTable dt, string KeyField, string ParentKeyField, string[] DisplayFieldNames)
        {
            DataTable data = dt.Copy();

            Dictionary<object, JsonModel> dicModel = new Dictionary<object, JsonModel>();
            List<JsonModel> result = new List<JsonModel>();
            foreach (DataRow dr in data.DefaultView.ToTable().Rows)
            {

                object key = dr[KeyField];
                if (!dicModel.ContainsKey(key))
                {
                    JsonModel newJsonModel = new JsonModel();
                    dicModel.Add(key, newJsonModel);
                    result.Add(newJsonModel);
                }

                JsonModel Model = dicModel[key];

                foreach (string fieldname in DisplayFieldNames)
                {
                    Model.MainBody.Add(fieldname, dr[fieldname]);
                }


                object Pkey = dr[ParentKeyField];
                if (!dicModel.ContainsKey(Pkey))
                {
                    JsonModel newJsonModel = new JsonModel();
                    dicModel.Add(Pkey, newJsonModel);
                    result.Add(newJsonModel);
                }
                dicModel[Pkey].Data.Add(Model);
                result.Remove(Model);
            }

            List<System.Web.Helpers.DynamicJsonObject> lst = new List<System.Web.Helpers.DynamicJsonObject>();

            foreach (JsonModel model in result)
            {
                if (model.MainBody.Count == 0)
                {
                    foreach (JsonModel m in model.Data)
                        lst.Add(JsonModel2Helper(m));
                }
                else
                    lst.Add(JsonModel2Helper(model));
            }

            var JsonArray = new System.Web.Helpers.DynamicJsonArray(lst.ToArray());
            string jsonstr = System.Web.Helpers.Json.Encode(JsonArray);
            return jsonstr;
        }

        private static System.Web.Helpers.DynamicJsonObject JsonModel2Helper(JsonModel model)
        {
            Dictionary<string, object> dic = model.MainBody;


            if (model.Data.Count > 0)
            {
                List<System.Web.Helpers.DynamicJsonObject> lstDetail = new List<System.Web.Helpers.DynamicJsonObject>();
                foreach (JsonModel m in model.Data)
                {
                    lstDetail.Add(JsonModel2Helper(m));
                }
                //var data = new System.Web.Helpers.DynamicJsonArray(lstDetail.ToArray());
                dic.Add("Data", lstDetail);
            }


            System.Web.Helpers.DynamicJsonObject json = new System.Web.Helpers.DynamicJsonObject(dic);
            return json;
        }

        public class JsonModel
        {
            public object Key { get; set; }
            public Dictionary<string, object> MainBody = new Dictionary<string, object>();
            public List<JsonModel> Data = new List<JsonModel>();
        }

        /// <summary>
        /// 主从表按主字段父子级关系
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="KeyField"></param>
        /// <param name="ParentKeyField"></param>
        public static void DataTable2TreeData(this DataTable data, string KeyField, string ParentKeyField, string TreeNodeName)
        {
            if (!data.Columns.Contains("garsonzhangbkpkey"))
                data.Columns.Add("garsonzhangbkpkey", data.Columns[ParentKeyField].DataType);
            if (!data.Columns.Contains(TreeNodeName))
                data.Columns.Add(TreeNodeName, typeof(System.Int32));

            List<DataRow> lstdr = new List<DataRow>();
            lstdr.AddRange(data.Select());

            foreach (DataRow dr in lstdr)
            {
                dr["garsonzhangbkpkey"] = dr[ParentKeyField];
                dr[TreeNodeName] = 1;
            }


            while (lstdr.Count > 0)
            {
                for (int i = lstdr.Count - 1; i >= 0; i--)
                {
                    DataRow dr = lstdr[i];
                    DataRow[] tmpdrs = data.Select(String.Format("{0}='{1}'", KeyField, dr["garsonzhangbkpkey"]));
                    if (tmpdrs.Length > 0)
                    {
                        dr["garsonzhangbkpkey"] = tmpdrs[0][ParentKeyField];
                        dr[TreeNodeName] = (int)dr[TreeNodeName] + 1;
                    }
                    else
                        lstdr.Remove(dr);
                }
            }

            data.Columns.Remove("garsonzhangbkpkey");
        }

        /// <summary>
        /// 转json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToJson(this DataTable dt)
        {
            System.Web.Helpers.DynamicJsonArray JsonArray;
            List<System.Web.Helpers.DynamicJsonObject> lst = new List<System.Web.Helpers.DynamicJsonObject>();

            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();

                foreach (DataColumn col in dt.Columns)
                {
                    dic.Add(col.ColumnName, dr[col]);
                }
                lst.Add(new System.Web.Helpers.DynamicJsonObject(dic));
            }
            JsonArray = new System.Web.Helpers.DynamicJsonArray(lst.ToArray());
            string jsonstr = System.Web.Helpers.Json.Encode(JsonArray);
            return jsonstr;


           
        }

      



        /// <summary>
        /// 转json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToJson(this DataSet ds)
        {
            System.Web.Helpers.DynamicJsonArray JsonArray;

            List<System.Web.Helpers.DynamicJsonObject> lstdt = new List<System.Web.Helpers.DynamicJsonObject>();


            foreach (DataTable dt in ds.Tables)
            {
                Dictionary<string, object> dicDT = new Dictionary<string, object>();
                List<System.Web.Helpers.DynamicJsonObject> lstrow = new List<System.Web.Helpers.DynamicJsonObject>();
                foreach (DataRow dr in dt.Rows)
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();

                    foreach (DataColumn col in dt.Columns)
                    {
                        dic[col.ColumnName] = dr[col];
                    }
                    lstrow.Add(new System.Web.Helpers.DynamicJsonObject(dic));
                }
                dicDT.Add(dt.TableName, lstrow);

                lstdt.Add(new System.Web.Helpers.DynamicJsonObject(dicDT));

            }

            JsonArray = new System.Web.Helpers.DynamicJsonArray(lstdt.ToArray());

            string jsonstr = System.Web.Helpers.Json.Encode(JsonArray);
            return jsonstr;
        }



        /// <summary>
        /// json字符串转换对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ToJson<T>(this string json)
        {
            return System.Web.Helpers.Json.Decode<T>(json);
        }

        /// <summary>
        /// json转dynamic对象
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static dynamic ToJson(this string json)
        {
            return System.Web.Helpers.Json.Decode(json);
        }
    }
    */
}
