using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Web;
using CefSharp;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Arch_LAB5     
{
    public partial class MainWindow : Window
    {
        public string Access_token { get; set; }
        public string UserID { get; set; }
        string appId = "51814556";
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var uriStr = @"https://oauth.vk.com/authorize?client_id=" + appId +
                    @"&scope=friends&redirect_uri=https://oauth.vk.com/blank.html&display=page&v=5.6&response_type=token";
            web.AddressChanged += BrowserOnNavigated;
            web.Load(uriStr);
        }
        private void BrowserOnNavigated(object sender, DependencyPropertyChangedEventArgs e)
        {
            var uri = new Uri((string)e.NewValue);
            if (uri.AbsoluteUri.Contains(@"oauth.vk.com/blank.html#"))
            {
                string url = uri.Fragment;
                url = url.Trim('#');
                Access_token = HttpUtility.ParseQueryString(url).Get("access_token");
                UserID = HttpUtility.ParseQueryString(url).Get("user_id").ToString();
                Dispatcher.Invoke(() =>
                {
                    API apiWindow = new API();
                    apiWindow.accesstoken = Access_token;
                    apiWindow.userID = UserID;
                    apiWindow.Show();

                });
            }
        }

        public static string GET(string Url, string Method, string Token)
        {
            WebRequest req = WebRequest.Create(string.Format(Url, Method, Token));
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string Out = sr.ReadToEnd();
            return Out;
        }
    }
}