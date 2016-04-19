using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GZAPI.Common
{
    public class httpLibrary
    {
        public static string Post(string url, string data)
        {
            string returnData = null;
            try
            {
                //byte[] buffer = Encoding.UTF8.GetBytes(data);
                //HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(url);
                //webReq.Method = "POST";
                //webReq.ContentType = "application/x-www-form-urlencoded";
                //webReq.ContentLength = buffer.Length;
                //Stream postData = webReq.GetRequestStream();
                //webReq.Timeout = 99999999;
                ////webReq.ReadWriteTimeout = 99999999;
                //postData.Write(buffer, 0, buffer.Length);
                //postData.Close();
                //HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
                //Stream answer = webResp.GetResponseStream();
                //StreamReader answerData = new StreamReader(answer);
                //returnData = answerData.ReadToEnd();

                string paraUrlCoded = data;
                byte[] payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json;charset=UTF-8";
                request.ContentLength = payload.Length;

                using (Stream writer = request.GetRequestStream())
                {
                    writer.Write(payload, 0, payload.Length);

                    using (System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse())
                    {

                        using (System.IO.Stream s = response.GetResponseStream())
                        {
                            string StrDate = "";
                            string strValue = "";
                            using (StreamReader Reader = new StreamReader(s, Encoding.UTF8))
                            {
                                while ((StrDate = Reader.ReadLine()) != null)
                                {
                                    strValue += StrDate + Environment.NewLine;
                                }
                                returnData = strValue;
                            }
                        }
                    }
                }
            }
            catch
            {
                return "";
            }
            return returnData.Trim() + "\n";
        }


        public async static void SysnPost(string url, string json)
        {
            // Clear text of Output textbox  

            using (HttpClient httpClient = new HttpClient())
            {


                try
                {

                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
                    {

                        HttpResponseMessage wcfResponse = await httpClient.PostAsync(url, content);



                        //string str = wcfResponse.Content.ReadAsStringAsync();
                        //await DisplayTextResult(wcfResponse, OutputField);
                    }
                }
                catch (HttpRequestException hre)
                {
                    //NotifyUser("Error:" + hre.Message);
                }
                catch (TaskCanceledException)
                {
                    //NotifyUser("Request canceled.");
                }
                catch (Exception ex)
                {
                    //NotifyUser(ex.Message);
                }
                finally
                {

                }
            }

        }

        public static string Get(string URL)
        {
            String ReCode = string.Empty;
            try
            {
                HttpWebRequest wNetr = (HttpWebRequest)HttpWebRequest.Create(URL);
                HttpWebResponse wNetp = (HttpWebResponse)wNetr.GetResponse();
                wNetr.ContentType = "text/html";
                wNetr.Method = "Get";
                Stream Streams = wNetp.GetResponseStream();
                StreamReader Reads = new StreamReader(Streams, Encoding.UTF8);
                ReCode = Reads.ReadToEnd();

                //封闭临时不实用的资料 
                Reads.Dispose();
                Streams.Dispose();
                wNetp.Close();
            }
            catch (Exception ex) { return ex.Message; }

            return ReCode;

        }
    }
}
