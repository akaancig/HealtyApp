using GrProject.Data;
using GrProject.Views;
using SQLite;
using System;
using System.IO;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GrProject
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            bool logIninfo = false;

            logIninfo = controlLogin();
            
            if (UserInfo.checkUser() == 0)
            {
                MainPage = new LoginPage();
            }
            else
            {
                if(logIninfo && LoginPage.CurrenisTrainer == "1")
                    MainPage = new Views.TrainerView.MainPage();
                else if (logIninfo && LoginPage.CurrenisTrainer == "0")
                    MainPage = new Views.MainPage();
                else
                    MainPage = new LoginPage();
            }
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        bool controlLogin() {
            try
            {
                string user = "";
                string pass = "";

                string db = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "lodosFit.db3");
                var dbCon = new SQLiteConnection(db);
                var table = dbCon.Table<UserInfo>();
                foreach (var s in table)
                {
                    user = s.username;
                    pass = s.password;
                    break;
                }
                string url = "http://arifcig.net/user_login.php?username=" + user + "&password=" + pass + "&secretToken=A.t541541";
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = "GET";
                WebResponse myResponse = myRequest.GetResponse();
                StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);//System.Text.Encoding.ASCII
                string result1 = sr.CurrentEncoding.HeaderName.ToString();
                string result3 = sr.ReadToEnd();
                sr.Close();
                myResponse.Close();
                if (result3 == "Giriş başarılı. User:" + user + "<br>")
                {
                    LoginPage.CurrentUser = user;
                    LoginPage.CurrenPass = pass;
                    LoginPage.CurrenisTrainer = "0";
                    return true;
                }
                else if (result3 == "Trainer")
                {
                    LoginPage.CurrentUser = user;
                    LoginPage.CurrenPass = pass;
                    LoginPage.CurrenisTrainer = "1";
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;    
            }
            
        }
    }
}
