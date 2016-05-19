using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhihu.UWP.Models
{
    public class Target
    {
        public string answer_count { get; set; }
        public Author author { get; set; }
        public string created_time { get; set; }
        public string comment_count { get; set; }
        public string follower_count { get; set; }
        public string excerpt { get; set; }
        public string id { get; set; }
        public string is_copyable { get; set; }
        public Question question { get; set; }
        public string thanks_count { get; set; }
        public string updated_time { get; set; }
        public string voteup_count { get; set; }
        public string verb { get; set; }
        public List<Actor> actors { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string url { get; set; }

    }
}
