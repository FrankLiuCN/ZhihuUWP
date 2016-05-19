using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Zhihu.UWP.Http;
using Zhihu.UWP.Models;
using Zhihu.UWP.ViewModels;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace Zhihu.UWP.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Home : Page
    {
        HomeViewModel _viewModel;
        HomeTest _test;
        public Home()
        {
            this.InitializeComponent();

            homeHeader.OnRefresh += HomeHeader_OnRefresh;
        }

        private async void HomeHeader_OnRefresh(object sender, EventArgs e)
        {
            _test.DoRefresh();
            await lvQuestion.LoadMoreItemsAsync();
           
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New)
            {
                LoadViewModel();
            }
        }

        private void LoadViewModel()
        {
            //HomeViewModel model = new HomeViewModel();
            //_viewModel = model;
            //this.DataContext = _viewModel;
            _test = new HomeTest();
            _test.DataLoaded += _test_DataLoaded;
            _test.DataLoading += _test_DataLoading;

        }
        private void _test_DataLoading()
        {
            prLoading.IsActive = true;
        }

        private void _test_DataLoaded()
        {
            prLoading.IsActive = false;
        }

        private void txtTitle_Click(object sender, RoutedEventArgs e)
        {
            HyperlinkButton hlb = sender as HyperlinkButton;
            string questionUrl = hlb.Tag.ToString();
        }

        private void topic_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lvQuestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Story story = lvQuestion.SelectedItems[0] as Story;
            //lvQuestion.SelectedIndex = -1;
            Frame root = Window.Current.Content as Frame;
            root.Navigate(typeof(AnswerPage),story );
        }

        
    }
}
