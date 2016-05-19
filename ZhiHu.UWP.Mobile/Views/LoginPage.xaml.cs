using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Zhihu.UWP.Http;
using Zhihu.UWP.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using Zhihu.UWP.Tool;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace Zhihu.UWP.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            lblError.Visibility = Visibility.Collapsed;
            Logining.Visibility = Visibility.Visible;
            ZhihuAPI.PostCaptcha(txtCaptcha.Text, (json) =>
            {
                if (json.Contains("true"))
                {
                    SignIn();
                }
                else
                {
                    btnLogin.IsEnabled = true;
                    lblError.Visibility = Visibility.Visible;
                    lblError.Text = json;
                    PutCaptcha();
                }
            }, (ex) =>
            {
                Logining.Visibility = Visibility.Collapsed;
                lblError.Visibility = Visibility.Visible;
                lblError.Text = "验证码不正确.";
                PutCaptcha();
            });
        }

        private void SignIn()
        {
            string timestamp = Util.DateTimeToStamp(DateTime.Now).ToString();
            Dictionary<string, string> param = Setting.LoginBody();
            param["password"] = txtPassword.Password;
            param["username"] = txtLoginName.Text;
            param["timestamp"] = timestamp;
            param["signature"] = Util.Signature(timestamp);
            ZhihuAPI.Login(param, async (json) =>
             {
                 SignIn signin = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<SignIn>(json));
                 Setting.access_token = signin.access_token;
                 Setting.lock_in = signin.lock_in;
                 Setting.refresh_token = signin.refresh_token;
                 Setting.token_type = signin.token_type;
                 Setting.uid = signin.uid;
                 Setting.unlock_ticket = signin.unlock_ticket;
                 Setting.user_id = signin.user_id;
                 Util.SaveLoginAuthorization(signin.token_type + " " + signin.access_token);
                 this.Frame.Navigate(typeof(AppShell));
             }, (ex) =>
             {
                 Logining.Visibility = Visibility.Collapsed;
                 lblError.Visibility = Visibility.Visible;
                 lblError.Text = ex.Message;
                 PutCaptcha();
             });
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Init();
        }
        private void Init()
        {
            ZhihuAPI.GetCaptcha((showCaptcha) =>
            {
                if (showCaptcha)
                {
                    spCaptcha.Visibility = Visibility.Visible;
                    PutCaptcha();
                }
            }, (Exception ex) =>
            {
            });
        }

        private void PutCaptcha()
        {
            ZhihuAPI.PutCaptcha(async (json) =>
            {
                Captcha captcha = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Captcha>(json));
                byte[] imageBytes = Convert.FromBase64String(captcha.img_base64);
                var bitmap = new RenderTargetBitmap();
                using (IRandomAccessStream fileStream = Util.ConvertTo(imageBytes))
                {
                    BitmapImage myBitmapImage = new BitmapImage();
                    await myBitmapImage.SetSourceAsync(fileStream);
                    imgCaptcha.Source = myBitmapImage;
                }
            }, (Exception ex) =>
            {

            });
        }

        private void imgCaptcha_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PutCaptcha();
        }
    }
}
