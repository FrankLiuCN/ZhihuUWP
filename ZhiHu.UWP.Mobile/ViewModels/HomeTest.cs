using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;
using Zhihu.UWP.Http;
using Zhihu.UWP.Models;
using Zhihu.UWP.Tool;

namespace Zhihu.UWP.ViewModels
{
    public delegate void DataLoadingEventHandler();
    public delegate void DataLoadedEventHandler();
    public class HomeTest : ObservableCollection<Story>, ISupportIncrementalLoading
    {
        private bool _busy = false;
        private bool _has_more_items = false;
        private int _current_page = 1;
        private int _page_size = 0;
        public event DataLoadingEventHandler DataLoading;
        public event DataLoadedEventHandler DataLoaded;
        private string url;

        public int TotalCount
        {
            get; set;
        }
        public bool HasMoreItems
        {
            get
            {
                if (_busy)
                    return false;
                else
                    return _has_more_items;
            }
            private set
            {
                _has_more_items = value;
            }
        }
        public HomeTest()
        {
            HasMoreItems = true;
            url = ApiUrl.TopStory_URL;
        }
        public void DoRefresh()
        {
            _current_page = 1;
            TotalCount = 0;
            Clear();
            HasMoreItems = true;
            url = ApiUrl.TopStory_URL;
        }
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return InnerLoadMoreItemsAsync(count).AsAsyncOperation();
        }
        private async Task<LoadMoreItemsResult> InnerLoadMoreItemsAsync(uint expectedCount)
        {
            _busy = true;
            var actualCount = 0;
            List<Story> list = null;
            try
            {
                if (DataLoading != null)
                {
                    DataLoading();
                }
                string json = await ZhihuAPI.GetTopStory(url);

                TopStory topStory = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<TopStory>(json));
                list = topStory.data;
                url = topStory.paging.next;
            }
            catch (Exception)
            {
                HasMoreItems = false;
            }

            if (list != null && list.Any())
            {
                actualCount = list.Count;
                TotalCount += actualCount;
                _current_page++;
                HasMoreItems = true;
                list.ForEach((c) => { this.Add(c); });
            }
            else
            {
                HasMoreItems = false;
            }
            if (DataLoaded != null)
            {
                DataLoaded();
            }
            _busy = false;
            return new LoadMoreItemsResult
            {
                Count = (uint)actualCount
            };
        }
    }
}
