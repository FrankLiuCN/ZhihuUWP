using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Zhihu.UWP.Models;
using Zhihu.UWP.Tool;
using Newtonsoft.Json;

namespace Zhihu.UWP.Http
{
    public static class ZhihuAPI
    {
        public static async void GetCaptcha(Action<bool> successCallback, Action<Exception> erorrCallback)
        {
            try
            {
                Dictionary<string, string> headers = BaseHeaders();
                HttpWebResponse response = await BaseService.CreateGetHttpResponse(ApiUrl.Captcha_URL, headers, null);
                WebHeaderCollection rHeaders = response.Headers;
                for (int i = 0; i < rHeaders.Count; i++)
                {
                    foreach (string key in rHeaders.AllKeys)
                    {
                        if (key == "Set-Cookie")
                        {
                            Setting.COOKIE = rHeaders[key].Split('"')[1];
                            break;
                        }
                    }
                }
                string json = Util.GetJson(response);
                if (json.Contains("true"))
                    successCallback(true);
                else
                    successCallback(false);
            }
            catch (Exception ex)
            {
                erorrCallback(ex);
            }
        }

        public static async void PutCaptcha(Action<string> successCallback, Action<Exception> erorrCallback)
        {
            try
            {
                Dictionary<string, string> headers = BaseHeaders();
                HttpWebResponse response = await BaseService.CreatePutHttpResponse(ApiUrl.Captcha_URL, headers, new Cookie("capsion_ticket", Setting.COOKIE));
                string json = Util.GetJson(response);
                successCallback(json);
            }
            catch (Exception ex)
            {
                erorrCallback(ex);
            }
        }


        public static async void PostCaptcha(string captcha, Action<string> successCallback, Action<Exception> erorrCallback)
        {
            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("input_text", captcha);
                Dictionary<string, string> headers = BaseHeaders();
                HttpWebResponse response = await BaseService.CreatePostHttpResponse(ApiUrl.Captcha_URL, param, headers, new Cookie("capsion_ticket", Setting.COOKIE));
                string json = Util.GetJson(response);
                successCallback(json);
            }
            catch (Exception ex)
            {
                erorrCallback(ex);
            }
        }


        public static async void Login(Dictionary<string, string> param, Action<string> successCallback, Action<Exception> erorrCallback)
        {
            try
            {
                Dictionary<string, string> headers = BaseHeaders();
                HttpWebResponse response = await BaseService.CreatePostHttpResponse(ApiUrl.Login_URL, param, headers, new Cookie("capsion_ticket", Setting.COOKIE));
                string json = Util.GetJson(response);
                successCallback(json);
            }
            catch (Exception ex)
            {
                erorrCallback(ex);
            }
        }

        public static async void GetPeopleSelf(Action<string> successCallback, Action<Exception> erorrCallback)
        {
            try
            {
                Dictionary<string, string> headers = BaseHeaders();
                headers["Authorization"] = Util.LoadLoginAuthorization();
                HttpWebResponse response = await BaseService.CreateGetHttpResponse(ApiUrl.PeopleSelf_URL, headers, null);
                string json = Util.GetJson(response);
                successCallback(json);
            }
            catch (Exception ex)
            {
                erorrCallback(ex);
            }
        }


        public static async Task<string> GetTopStory(string url)
        {
            Dictionary<string, string> headers = BaseHeaders();
            headers["Authorization"] = Util.LoadLoginAuthorization();
            HttpWebResponse response = await BaseService.CreateGetHttpResponse(url, headers, null);
            string json = Util.GetJson(response, Encoding.UTF8);
            return json;
        }


        public static async Task<string> GetAnswer(string url)
        {

            Dictionary<string, string> headers = BaseHeaders();
            headers["Authorization"] = Util.LoadLoginAuthorization();
            HttpWebResponse response = await BaseService.CreateGetHttpResponse(url, headers, null);
            string json = Util.GetJson(response, Encoding.UTF8);
            return json;
        }

        private static Dictionary<string, string> BaseHeaders()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Accept-Encoding", "gzip");
            headers.Add("Authorization", Setting.Authorization);
            headers.Add("x-api-version", Setting.API_VERSION);
            headers.Add("x-app-version", Setting.APP_VERSION);
            headers.Add("x-app-za", Setting.APP_ZA);
            headers.Add("x-app-build", Setting.APP_BUILD);
            headers.Add("User-Agent", Setting.User_Agent);
            headers.Add("Host", Setting.HOST);
            return headers;
        }

    }
}
