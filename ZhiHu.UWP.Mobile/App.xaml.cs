﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Zhihu.UWP.Tool;
using Zhihu.UWP.Views;

namespace Zhihu.UWP
{
    /// <summary>
    /// 提供特定于应用程序的行为，以补充默认的应用程序类。
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// 初始化单一实例应用程序对象。这是执行的创作代码的第一行，
        /// 已执行，逻辑上等同于 main() 或 WinMain()。
        /// </summary>
        public App()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// 在应用程序由最终用户正常启动时进行调用。
        /// 将在启动应用程序以打开特定文件等情况下使用。
        /// </summary>
        /// <param name="e">有关启动请求和过程的详细信息。</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // 不要在窗口已包含内容时重复应用程序初始化，
            // 只需确保窗口处于活动状态
            if (rootFrame == null)
            {
                // 创建要充当导航上下文的框架，并导航到第一页
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;
                rootFrame.Navigated += RootFrame_Navigated;
                // 将框架放在当前窗口中
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // 当导航堆栈尚未还原时，导航到第一页，
                // 并通过将所需信息作为导航参数传入来配置
                // 参数
                if (string.IsNullOrWhiteSpace(Util.LoadLoginAuthorization()))
                {
                    rootFrame.Navigate(typeof(LoginPage));
                }
                else
                {
                    rootFrame.Navigate(typeof(AppShell));
                    SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;

                }

            }
            // 确保当前窗口处于活动状态
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {

                var statusBar = StatusBar.GetForCurrentView();
                if (statusBar != null)
                {
                    //statusBar.BackgroundOpacity = 1;
                    //Windows.UI.Color c = (Application.Current.Resources.ThemeDictionaries["SystemControlHighlightAltListAccentLowBrush"] as SolidColorBrush).Color;
                    //statusBar.BackgroundColor = Windows.UI.Colors.Transparent;
                    statusBar.ForegroundColor = Windows.UI.Colors.White;

                    await statusBar.ShowAsync();
                }
            }
            Window.Current.Activate();
        }

        private void RootFrame_Navigated(object sender, NavigationEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
rootFrame.CanGoBack ?
AppViewBackButtonVisibility.Visible :
AppViewBackButtonVisibility.Collapsed;
        }

        bool isExit = false;
        private async void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
                return;
            if (rootFrame.CurrentSourcePageType.Name != "MainPage")
            {
                if (rootFrame.CanGoBack && e.Handled == false)
                {
                    e.Handled = true;
                    rootFrame.GoBack();
                }
            }
            else if (e.Handled == false)
            {

                StatusBar statusBar = StatusBar.GetForCurrentView();
                await statusBar.ShowAsync();
                statusBar.ForegroundColor = Colors.White; // 前景色  
                statusBar.BackgroundOpacity = 0.9; // 透明度  
                statusBar.ProgressIndicator.Text = "再按一次返回键退出程序。"; // 文本  
                await statusBar.ProgressIndicator.ShowAsync();

                if (isExit)
                {
                    App.Current.Exit();
                }
                else
                {
                    isExit = true;
                    await Task.Run(async () =>
                    {
                        //Windows.Data.Xml.Dom. XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);  
                        //Windows.Data.Xml.Dom.XmlNodeList elements = toastXml.GetElementsByTagName("text");  
                        //elements[0].AppendChild(toastXml.CreateTextNode("再按一次返回键退出程序。"));  
                        //ToastNotification toast = new ToastNotification(toastXml);  
                        //ToastNotificationManager.CreateToastNotifier().Show(toast);       

                        await Task.Delay(1500);
                        await statusBar.ProgressIndicator.HideAsync();
                        await statusBar.HideAsync();
                        isExit = false;
                    });
                    e.Handled = true;
                }
            }

        }

        /// <summary>
        /// 导航到特定页失败时调用
        /// </summary>
        ///<param name="sender">导航失败的框架</param>
        ///<param name="e">有关导航失败的详细信息</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// 在将要挂起应用程序执行时调用。  在不知道应用程序
        /// 无需知道应用程序会被终止还是会恢复，
        /// 并让内存内容保持不变。
        /// </summary>
        /// <param name="sender">挂起的请求的源。</param>
        /// <param name="e">有关挂起请求的详细信息。</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: 保存应用程序状态并停止任何后台活动
            deferral.Complete();
        }
    }
}
