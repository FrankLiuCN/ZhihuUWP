using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhihu.UWP.Http;
using Zhihu.UWP.Models;

namespace Zhihu.UWP.ViewModels
{
    public class AnswerViewModel : ObservableObjectBase
    {
        private Answer _answerInfo;

        public Answer AnswerInfo
        {
            get
            {
                return _answerInfo;
            }

            set
            {
                _answerInfo = value;
                OnPropertyChanged();
            }
        }

        public AnswerViewModel(string url)
        {
            GetAnswer(url);
        }

        private async void GetAnswer(string url)
        {
            IsLoading = true;
            string json = await ZhihuAPI.GetAnswer(url);
            Answer answer = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Answer>(json));

            if (answer != null)
            {
                string css = "<style>"
                        + "html{-ms-content-zooming:none;font-family:微软雅黑;}"
                        + "body{word-break:break-all;font-size:16px;} img{width:100%;}"
                        + "body{line-height:150%;}"
                        + "</style>";   //基础css

                //合并
                answer.content = "<html><head>"  + css  + "</head>" + "<body>" + answer.content.Replace("<blockquote>", "<p>").Replace("</blockquote>", "</p>") + "</body></html>";  //附加css
            }

            AnswerInfo = answer;
            IsLoading = false;
        }
    }
}
