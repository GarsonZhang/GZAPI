using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GZAPI.Common
{
    public class ConvertLibrary
    {
        public static IEnumerable<T> Convert2Object<T>(DataTable dt, bool IgnoreCase) where T : new()
        {
            Type type = typeof(T);
            List<T> lst = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                foreach (DataColumn col in dt.Columns)
                {
                    //if (object.Equals(dr[s.Name], DBNull.Value)) continue;
                    //object tv = System.ComponentModel.TypeDescriptor.GetConverter(s.PropertyType).ConvertFromString(dr[s.Name].ToString());
                    //s.SetValue(t, tv, null);
                    SetObjectValue(type, t, col.ColumnName, dr[col], IgnoreCase);
                }
                lst.Add(t);
                //yield return t;
            }
            return lst;
        }

        public static T ConvertObject<T>(DataRow dr, bool IgnoreCase) where T : new()
        {
            T t = new T();
            Type type = typeof(T);

            if (dr.Table != null)
            {
                foreach (DataColumn col in dr.Table.Columns)
                {
                    object o = dr[col];
                    SetObjectValue(type, t, col.ColumnName, o, IgnoreCase);
                }
            }


            return t;

        }

        public static T Convert2Object<T>(DbDataReader row, bool IgnoreCase) where T : new()
        {
            T t = new T();
            Type type = typeof(T);

            for (int i = 0; i < row.FieldCount; i++)
            {
                string fname = row.GetName(i);

                SetObjectValue(type, t, fname, row.GetValue(i), IgnoreCase);
            }

            return t;
        }
        /// <summary>
        /// 转换为对象，忽略大小写
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T Convert2Object<T>(DbDataReader row) where T : new()
        {
            return Convert2Object<T>(row, true);
        }

        /// <summary>
        /// 将字典转换为指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
        /// <param name="IgnoreCase">是否忽略大小写，True，忽略大小写</param>
        /// <returns></returns>
        public static T Convert2ObjectIgnoreCase<T>(Dictionary<string, object> dic, bool IgnoreCase = false) where T : new()
        {

            T t = new T();
            Type type = typeof(T);
            foreach (string key in dic.Keys)
            {
                SetObjectValue(type, t, key, dic[key], IgnoreCase);
            }
            return t;
        }

        /// <summary>
        /// 设置对象指定属性的值
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="obj">对象实例</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">值</param>
        /// <param name="IgnoreCase">忽略大小写</param>
        private static void SetObjectValue(Type type, object obj, string name, object value, bool IgnoreCase)
        {
            BindingFlags bindingflags = (BindingFlags.Public | BindingFlags.Instance) | (IgnoreCase ? BindingFlags.IgnoreCase : BindingFlags.Default);
            PropertyInfo p = type.GetProperty(name, bindingflags);
            if (p != null)
            {
                object tv = ConvertValue(p.PropertyType, value);
                p.SetValue(obj, tv, null);
            }
        }

        public static T ConvertValue<T>(object o)
        {
            try
            {
                object tv = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(o);
                return (T)tv;
            }
            catch
            {
                return default(T);
            }
        }

        public static object ConvertValue(Type type, object value)
        {
            object tv;
            if (object.Equals(value, DBNull.Value) || value == null)
                tv = type.IsValueType ? Activator.CreateInstance(type) : null;
            else
            {
                try
                {
                    tv = System.ComponentModel.TypeDescriptor.GetConverter(type).ConvertFrom(value);
                }
                catch
                {
                    tv = type.IsValueType ? Activator.CreateInstance(type) : null;
                }
            }
            return tv;
        }
    }
}
