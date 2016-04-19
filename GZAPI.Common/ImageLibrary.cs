using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZAPI.Common
{
    public class ImageLibrary
    {
        /// <summary>
        /// 图片转为 base64编码的文本
        /// </summary>
        /// <param name="Imagefilename"></param>
        public static void ImgToBase64String(string Imagefilename)
        {
            try
            {
                Bitmap bmp = new Bitmap(Imagefilename);

                FileStream fs = new FileStream(Imagefilename + ".txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);

                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                String strbaser64 = Convert.ToBase64String(arr);
                sw.Write(strbaser64);

                sw.Close();
                fs.Close();
                // MessageBox.Show("转换成功!");
            }
            catch (Exception ex)
            {
                throw new Exception("ImgToBase64String 转换失败\nException:" + ex.Message);
            }
        }

        /// <summary>
        /// base64编码的文本转为图片
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static Bitmap Base64StringToImage(string inputStr)
        {
            try
            {
                byte[] arr = Convert.FromBase64String(inputStr);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);



                ms.Close();

                return bmp;
            }
            catch (Exception ex)
            {
                throw new Exception("Base64StringToImage 转换失败\nException：" + ex.Message);
            }
        }



        /// <summary>
        /// base64编码的文本转为图片文件
        /// </summary>
        /// <param name="inputStr"></param>
        /// <param name="FilePath"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static bool Base64StringToFile(string inputStr, string FilePath, string FileName)
        {
            try
            {
                string extension = Path.GetExtension(FilePath + "/" + FileName);
                string SaveFileName = Guid.NewGuid().ToString() + extension;
                using (FileStream fs = File.Create(FilePath + FileName))
                {
                    byte[] arr = Convert.FromBase64String(inputStr);
                    using (MemoryStream ms = new MemoryStream(arr))
                    {
                        ms.WriteTo(fs);
                    }
                }
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception("Base64StringToImage 转换失败\nException：" + ex.Message);
            }
        }

    }
}
