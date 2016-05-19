using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhihu.UWP.Models
{
    public class SignIn
    {
        public string user_id { get; set; }
        public string uid { get; set; }
        public string access_token { get; set; }
        public string lock_in { get; set; }
        public string expires_in { get; set; }
        public string token_type { get; set; }
        //public List<string> cookie { get; set; }
        public string unlock_ticket { get; set; }
        public string refresh_token { get; set; }
    }

}
