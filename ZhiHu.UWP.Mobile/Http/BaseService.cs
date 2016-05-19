using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Zhihu.UWP.Tool;

namespace Zhihu.UWP.Http
{
    public static class BaseService
    {
        public static async Task<HttpWebResponse> CreateGetHttpResponse(string url, Dictionary<string, string> headers, Cookie cookies)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            if (headers != null)
                foreach (string key in headers.Keys)
                    request.Headers[key] = headers[key];
            request.Method = "GET";
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(new Uri(ApiUrl.Root_URL), cookies);
            }
            return await request.GetResponseAsync() as HttpWebResponse;
        }


        public static async Task<HttpWebResponse> CreatePutHttpResponse(string url, Dictionary<string, string> headers, Cookie cookies)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            if (headers != null)
                foreach (string key in headers.Keys)
                    request.Headers[key] = headers[key];
            request.Method = "PUT"; 
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(new Uri(ApiUrl.Root_URL), cookies);
            }


            return await request.GetResponseAsync() as HttpWebResponse;
        }
        public static async Task<HttpWebResponse> CreatePostHttpResponse(string url, IDictionary<string, string> parameters, Dictionary<string, string> headers, Cookie cookies)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            
            if (headers != null)
                foreach (string key in headers.Keys)
                   request.Headers[key] = headers[key];

            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(new Uri(ApiUrl.Root_URL), cookies);
            }

            //如果需要POST数据   
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                byte[] data = Encoding.UTF8.GetBytes(buffer.ToString());
                using (Stream stream =await request.GetRequestStreamAsync())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            return await request.GetResponseAsync() as HttpWebResponse;
        }

    }
}
