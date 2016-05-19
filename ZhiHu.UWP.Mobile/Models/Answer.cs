using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhihu.UWP.Models
{
    public class Answer
    {
        public Suggest suggest_edit { get; set; }
        public string is_copyable { get; set; }
        public Can_Commnet can_comment { get; set; }
        public string is_mine { get; set; }
        public Author author { get; set; }
        public string url { get; set; }
        public Question question { get; set; }
        public string excerpt { get; set; }
        public string updated_time { get; set; }
        public string content { get; set; }
        public string comment_count { get; set; }
        public string comment_permission { get; set; }
        public string created_time { get; set; }
        public string voteup_count { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public string thanks_count { get; set; }
    }

    public class Suggest
    {
        public string status { get; set; }
        public string url { get; set; }
        public string reason { get; set; }
        public string tip { get; set; }
        public string title { get; set; }
    }
    public class Can_Commnet
    {
        public string status { get; set; }
        public string reason { get; set; }
    }
}
