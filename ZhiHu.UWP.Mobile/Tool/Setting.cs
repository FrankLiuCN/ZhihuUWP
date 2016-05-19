using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhihu.UWP.Tool
{
    public static class Setting
    {
        public static int TIMEOUT = 5000;

        public static string HOST = "api.zhihu.com";

        public static string CLIENT_ID = "8d5227e0aaaa4797a763ac64e0c3b8";

        public static string APP_SECRET = "ecbefbf6b17e47ecb9035107866380";

        public static string API_VERSION = "3.0.16";

        public static string APP_VERSION = "3.2.0";

        public static string APP_BUILD = "release";

        public static string VARY = "Accept-Encoding";

        public static string COOKIE = "";

        public static string Authorization = "oauth 8d5227e0aaaa4797a763ac64e0c3b8";

        public static string User_Agent = "Google-HTTP-Java-Client/1.20.0 (gzip)";

        public static string ContentType = "application/x-www-form-urlencoded";

        public static string Source = "";

        public static string user_id;

        public static string access_token;

        public static string lock_in;

        public static string token_type = "";

        public static string uid;

        public static string unlock_ticket;

        public static string refresh_token;

        public static string APP_ZA = "OS=Android&Release=5.1.1&Model=Mi-4c&VersionName=3.2.0&VersionCode=307&Width=1080&Height=1920&Installer=%E5%B0%8F%E7%B1%B3%E5%95%86%E5%BA%97";

        public static Dictionary<string, string> LoginBody()
        {
            Dictionary<string, string> body = new Dictionary<string, string>();
            body.Add("client_id", CLIENT_ID);
            body.Add("grant_type", "password");
            body.Add("password", "");
            body.Add("scope", "");
            body.Add("signature", "");
            body.Add("source", "com.zhihu.android");
            body.Add("timestamp", "");
            body.Add("username", "");
            body.Add("uuid", "");
            return body;
        }

    }
}
