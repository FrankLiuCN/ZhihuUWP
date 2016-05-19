using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Zhihu.UWP.Http;
using Zhihu.UWP.Tool;
using Zhihu.UWP.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.UI.Xaml.Media.Imaging;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace Zhihu.UWP.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AppShell : Page
    {
        public AppShell()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;

        }
        public Frame AppFrame => AppShellFrame;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New)
            {
                LoadPeopleSelf();
                AppShellFrame.Navigate(typeof(Home));
            }
        }

        private void LoadPeopleSelf()
        {
            ZhihuAPI.GetPeopleSelf(async (json)=> {
                PepoleSelf pepole = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<PepoleSelf>(json));
                BitmapImage bi = new BitmapImage();
                bi.UriSource = new Uri(pepole.avatar_url);
                imgAvatar.ImageSource = bi;
                txtUserName.Text = pepole.name;
                txtDescribe.Text = pepole.headline;
            }, (ex)=> {

            });
        }
        private async void ShowStatusBar()
        {
            var statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            if (statusBar != null)
            {
                //statusBar.BackgroundOpacity = 0;
                //Windows.UI.Color c = (Application.Current.Resources.ThemeDictionaries["SystemControlHighlightAltListAccentLowBrush"] as SolidColorBrush).Color;
                statusBar.BackgroundColor = Windows.UI.Colors.Transparent;
                statusBar.ForegroundColor = Colors.White;
                await statusBar.ShowAsync();
            }
        }

        private void SplitViewOpener_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (e.Cumulative.Translation.X > 50)
            {
                MySplitView.IsPaneOpen = true;
            }
        }

        private void SplitViewPane_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (e.Cumulative.Translation.X < -50)
            {
                MySplitView.IsPaneOpen = false;
            }
        }
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Util.SaveLoginAuthorization("");
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(LoginPage));
        }
    }
}
