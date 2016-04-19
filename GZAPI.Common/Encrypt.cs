using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GZAPI.Common
{
    public class Encrypt
    {
        //默认密钥向量
        private static byte[] DefaultIV = { 0x58, 0x58, 0x58, 0x58, 0x58, 0x58, 0x58, 0x58 };
        private static byte[] DefaultKey = { 0x61, 0x61, 0x73, 0x69, 0x69, 0x69, 0x69, 0x69 };


        #region DES对称加密解密
        /// <summary>
        /// DES对称加密
        /// </summary>
        /// <param name="data">加密数据</param>
        /// <param name="key">8位字符的密钥字符串</param>
        /// <param name="iv">8位字符的初始化向量字符串</param>
        /// <returns></returns>
        public static string DESEncrypt(string data, string key = null, string iv = null)
        {
            byte[] byKey = String.IsNullOrEmpty(key) ? DefaultKey : System.Text.ASCIIEncoding.ASCII.GetBytes(key);
            byte[] byIV = String.IsNullOrEmpty(iv) ? DefaultIV : System.Text.ASCIIEncoding.ASCII.GetBytes(iv);


            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

            StreamWriter sw = new StreamWriter(cst);
            sw.Write(data);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }

        /// <summary>
        /// DES对称解密
        /// </summary>
        /// <param name="data">解密数据</param>
        /// <param name="key">8位字符的密钥字符串(需要和加密时相同)</param>
        /// <param name="iv">8位字符的初始化向量字符串(需要和加密时相同)</param>
        /// <returns></returns>
        public static string DESDecrypt(string data, string key = null, string iv = null)
        {
            if (String.IsNullOrWhiteSpace(data)) return null;
            byte[] byKey = String.IsNullOrEmpty(key) ? DefaultKey : System.Text.ASCIIEncoding.ASCII.GetBytes(key);
            byte[] byIV = String.IsNullOrEmpty(iv) ? DefaultIV : System.Text.ASCIIEncoding.ASCII.GetBytes(iv);

            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(data);
            }
            catch
            {
                return null;
            }

            try
            {
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream(byEnc);
                CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cst);
                return sr.ReadToEnd();
            }
            catch
            {
                return null;
            }
        }
        #endregion



    }
}
