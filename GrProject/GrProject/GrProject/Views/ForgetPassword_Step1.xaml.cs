using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GrProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgetPassword_Step1 : ContentPage
    {
        public static string resetCode = "";
        public bool isMailSent = false;
        public static string mail = "";
        public static string UpdatableUser = "";
        public ForgetPassword_Step1()
        {
            InitializeComponent();
        }
        async void BtnileriClicked(object sender, EventArgs args)
        {
            mail = getMail();
            if (mail == "Hatalı kullanıcı adı.") {
                await DisplayAlert("Durum", "Hatalı kullanıcı adı !", "Tamam");
                return;
            }
            else if (mail.Trim() == "" || mail.Trim().Length < 8 ) {
                await DisplayAlert("Durum", "Mail kaydı bulunurken hata oluştu !", "Tamam");
                return;
            }
            else{
                sendEmail(mail);
                if (isMailSent == true) {
                    Application.Current.MainPage = new ForgetPassword_Step2();
                }
            }
        }
        void BtnGeriClicked(object sender, EventArgs args)
        {
            Application.Current.MainPage = new LoginPage();
        }
        private async void sendEmail(string mail)
        {
            try
            {
                Random rnd = new Random();
                resetCode = rnd.Next(100000, 999999).ToString();
                MailMessage new_mail = new MailMessage();
                SmtpClient istemci = new SmtpClient();
                istemci.Credentials = new System.Net.NetworkCredential("cahmetkaan@hotmail.com", "A.k16411903");
                istemci.Port = 587;
                istemci.Host = "smtp.live.com";
                istemci.EnableSsl = true;
                new_mail.To.Add(mail);
                new_mail.From = new MailAddress("cahmetkaan@hotmail.com");
                new_mail.Subject = "Lodos Fitness App";
                new_mail.Body = "Lodos Fitness Ekibinden Merhaba Parola Sıfırlama Kodunuz : " + resetCode;
                istemci.Send(new_mail);
                isMailSent = true;
                await DisplayAlert("E-mail Gönderildi", "Parola sıfırlama kodunuz e-mail adresinize gönderilmiştir.", "Tamam");
                UpdatableUser = EntryUsername.Text;
            }
            catch
            {
                await DisplayAlert("E-mail gönderilemedi.", "Mail adresiniz eksik yada yanlış lütfen bizimle iletişime geçiniz.", "Tamam");
                return;
            }

        }
        public string getMail() {
            try
            {
                string url = "http://arifcig.net/get_mail.php?username=" + EntryUsername.Text + "&secretToken=A.t541541";
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = "GET";
                WebResponse myResponse = myRequest.GetResponse();
                StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);//System.Text.Encoding.ASCII
                string result1 = sr.CurrentEncoding.HeaderName.ToString();
                string result3 = sr.ReadToEnd();
                sr.Close();
                myResponse.Close();
                return result3;
            }
            catch (Exception)
            {
                return "";
                throw;
            }
        }
    }
}