using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhihu.UWP.Models
{
    public class Story
    {
        public ObservableCollection<Actor> actors { get; set; }
        public string id { get; set; }
        public Target target { get; set; }
        public string type { get; set; }
        public string updated_time { get; set; }
        public string verb { get; set; }
    }
}
