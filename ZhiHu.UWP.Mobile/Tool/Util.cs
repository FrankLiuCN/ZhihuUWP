using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Windows.Storage.Streams;
using Windows.Storage;

namespace Zhihu.UWP.Tool
{
    public static class Util
    {
        public static string GetJson(HttpWebResponse response, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            Stream s = response.GetResponseStream();
            s = new GZipStream(s, CompressionMode.Decompress);
            StreamReader sr = new StreamReader(s, encoding);
            string json = sr.ReadToEnd();
            response.Dispose();
            s.Dispose();
            return json;
        }


        public static string Signature(string timestamp)
        {
            string src = "password" + Setting.CLIENT_ID + "com.zhihu.android" + timestamp;
            string PEC = Setting.APP_SECRET;
            HMACSHA1 oEncode = new HMACSHA1(Encoding.UTF8.GetBytes(PEC));
            byte[] aBytes = oEncode.ComputeHash(Encoding.UTF8.GetBytes(src));
            StringBuilder sb = new StringBuilder();
            foreach (byte aB in aBytes)
            {
                sb.Append(aB.ToString("x02"));
            }
            return sb.ToString();
        }

        public static IRandomAccessStream ConvertTo(byte[] arr)
        {
           
            MemoryStream stream = new MemoryStream(arr);
            return stream.AsRandomAccessStream();
        }
        // DateTime时间格式转换为Unix时间戳格式 
        public static int DateTimeToStamp(System.DateTime time)
        {
            System.DateTime startTime = new System.DateTime(1970, 1, 1);
            return (int)(time - startTime).TotalSeconds;
        }

        /// <summary>
        /// 保存登录信息
        /// </summary>
        public static void SaveLoginAuthorization(string author)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            ApplicationDataCompositeValue composite =(ApplicationDataCompositeValue)localSettings.Values["Authorization"];
            if (composite == null)
            {
                composite = new ApplicationDataCompositeValue();
            }
            composite["Authorization"] = author;
            localSettings.Values["Authorization"] = composite;
        }

        public static string LoadLoginAuthorization()
        {
            ApplicationDataContainer localSettings =ApplicationData.Current.LocalSettings;
           ApplicationDataCompositeValue composite =(ApplicationDataCompositeValue)localSettings.Values["Authorization"];
            if (composite != null)
            {
                return composite["Authorization"].ToString();
            }
            return "";
        }
    }

}
