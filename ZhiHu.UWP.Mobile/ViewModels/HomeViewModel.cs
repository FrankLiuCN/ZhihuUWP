using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zhihu.UWP.Http;
using Zhihu.UWP.Models;

namespace Zhihu.UWP.ViewModels
{
    public class HomeViewModel : ObservableObjectBase
    {

        private Paging pagingInfo;

        private ObservableCollection<Story> storys;
        public ObservableCollection<Story> Storys
        {
            get
            {
                return storys;
            }
            set
            {
                storys = value;
                OnPropertyChanged();
            }
        }

        public HomeViewModel()
        {
            LoadTopStorys();
        }
        private void LoadTopStorys()
        {
            IsLoading = true;
            //ZhihuAPI.GetTopStory(async (json) =>
            //{
            //    TopStory topStory = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<TopStory>(json));
            //    //Storys = topStory.data;
            //    pagingInfo = topStory.paging;
            //    IsLoading = false;
            //}, (ex) =>
            //{

            //});
        }
    }
}
