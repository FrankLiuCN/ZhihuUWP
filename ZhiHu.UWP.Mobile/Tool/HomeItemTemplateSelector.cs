using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Zhihu.UWP.Models;

namespace Zhihu.UWP.Tool
{
    public class HomeItemTemplateSelector: DataTemplateSelector
    {
        public DataTemplate QuestionTemplate { get; set; }
        public DataTemplate AnswerTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            Story story = item as Story;
            if (story.verb == "TOPIC_POPULAR_QUESTION")
                return QuestionTemplate;
            else if(story.verb == "TOPIC_ACKNOWLEDGED_ANSWER")
                return AnswerTemplate;
            else if(story.verb== "MEMBER_FOLLOW_ROUNDTABLE")
                return QuestionTemplate;
            else
                return  AnswerTemplate;
        }
    }
}
