using GrProject.Data;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GrProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        string db = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "lodosFit.db3");
        public static string CurrentUser = "";
        public static string CurrenPass = "";
        public static string CurrenisTrainer = "";
        public LoginPage()
        {
            InitializeComponent();
        }
        async void BtnGirisClicked(object sender, EventArgs args)
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                try
                {
                    BtnGirisYap.IsEnabled = false;
                    BtnParolamiUnuttum.IsEnabled = false;
                    BtnKaydol.IsEnabled = false;
                    string url = "http://arifcig.net/user_login.php?username=" + EntryUsername.Text + "&password=" + EntryPassword.Text + "&secretToken=A.t541541";
                    HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                    myRequest.Method = "GET";
                    WebResponse myResponse = myRequest.GetResponse();
                    StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);//System.Text.Encoding.ASCII
                    string result1 = sr.CurrentEncoding.HeaderName.ToString();
                    string result3 = sr.ReadToEnd();
                    sr.Close();
                    myResponse.Close();
                    if (result3 == "Giriş başarılı. User:" + EntryUsername.Text + "<br>")
                    {
                        createDBuser();
                        CurrentUser = EntryUsername.Text;
                        CurrenPass = EntryPassword.Text;
                        CurrenisTrainer = "0";

                        Application.Current.MainPage = new MainPage();
                    }
                    else if (result3 == "Trainer")
                    {
                        createDBuser();
                        CurrentUser = EntryUsername.Text;
                        CurrenPass = EntryPassword.Text;
                        CurrenisTrainer = "1";

                        Application.Current.MainPage = new TrainerView.MainPage();
                    }
                    else
                    {
                        await DisplayAlert("Durum", "Kullanıcı Adı veya Parola Yanlış !", "Tamam");
                    }
                }
                catch (Exception)
                {
                    await DisplayAlert("Durum", "Beklenmedik bir hata oluştu!", "Tamam");
                    throw;
                }
                
            }
            else
            {
                await DisplayAlert("Durum", "Giriş yapmak için internet bağlantısı gereklidir.!", "Tamam");
            }
            BtnGirisYap.IsEnabled = true;
            BtnParolamiUnuttum.IsEnabled = true;
            BtnKaydol.IsEnabled = true;
        }
        void BtnParolamiUnuttumClicked(object sender, EventArgs args)
        {
            Application.Current.MainPage = new ForgetPassword_Step1();
        }
        void BtnKaydolClicked(object sender, EventArgs args)
        {
            Application.Current.MainPage = new RegisterPage();
        }
        void createDBuser()
        {
            var dbCon = new SQLiteConnection(db);
            dbCon.DropTable<UserInfo>();
            dbCon.CreateTable<UserInfo>();

            UserInfo node = new UserInfo();
            node.Id = 1;
            node.username = EntryUsername.Text;
            node.password = EntryPassword.Text;
            dbCon.Insert(node);
            dbCon.Close();
        }
    }
}