using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhihu.UWP.Tool
{
    public static class ApiUrl
    {
        public static string Root_URL = "https://api.zhihu.com";      

        public static string Captcha_URL = Root_URL + "/captcha"; 
        
        public static string Login_URL = Root_URL + "/sign_in"; 

        public static string TopStory_URL = Root_URL + "/topstory"; 
        
        public static string Answers_URL = Root_URL + "/answers/";

        public static string PeopleSelf_URL = Root_URL + "/people/self/basic";
    }
}
