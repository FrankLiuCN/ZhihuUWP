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
            AnswerInfo = answer;
            IsLoading = false;
        }
    }
}
