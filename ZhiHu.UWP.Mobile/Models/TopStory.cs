using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhihu.UWP.Models
{
    public class TopStory
    {
        public List<Story> data { get; set; }

        public Paging paging { get; set; } 
    }
}
