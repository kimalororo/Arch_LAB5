using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Ink;

namespace Arch_LAB5
{
    /// <summary>
    /// Логика взаимодействия для API.xaml
    /// </summary>
    public partial class API : Window
    {
        public API()
        {
            InitializeComponent();
        }
        public string accesstoken { get; set; }
        public string userID { get; set; }

        private string GET(string Url, string Method, string Token)
        {
            WebRequest req = WebRequest.Create(String.Format(Url, Method, Token));
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string Out = sr.ReadToEnd();
            return Out;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string reqStrTemplate = "https://api.vk.com/method/{0}?access_token={1}&v=5.154";
            string method = "account.getProfileInfo";
            string f = GET(reqStrTemplate, method, accesstoken);
            var user = JsonConvert.DeserializeObject(f) as JObject; 
            StringBuilder stroka = new StringBuilder();
            stroka.Append("Фамилия: " + user["response"]["last_name"] + 
                        " \nИмя: " + user["response"]["first_name"] + 
                        " \nДата рождения: " + user["response"]["bdate"] + 
                        " \nГород: " + user["response"]["city"]["title"] +
                        " \nНомер телефона: " + user["response"]["phone"] + 
                        " \nСтрана: " + user["response"]["country"]["title"]+
                        "\n accss " + accesstoken +
                        "\n usrid " + userID);

            inputTextBox.Text = stroka.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string reqStrTemplate = @"https://api.vk.com/method/{0}?{1}&access_token=" + accesstoken + "&v=5.154";
            string method = "friends.getOnline";
            string f = GET(reqStrTemplate, method, "user_id="+ userID + "&online_mobile=1");
            var friends = JsonConvert.DeserializeObject(f) as JObject;
            StringBuilder str = new StringBuilder();
            str.Append(friends);
            inputTextBox.Text = str.ToString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
