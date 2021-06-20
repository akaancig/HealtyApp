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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            EntryUsername.Text = "";
            EntryPassword.Text = "";
            EntryPassword2.Text = "";
            EntryEmail.Text = "";
        }
        async void BtnKaydolClicked(object sender, EventArgs args)
        {
                var current = Connectivity.NetworkAccess;
                if (current != NetworkAccess.Internet)
                {
                    await DisplayAlert("Durum", "Kayıt olmak için internet bağlantısı gereklidir.", "Tamam");
                    return;
                }
                if (EntryUsername.Text.Contains(" "))
                {
                    await DisplayAlert("Durum", "Kullanıcı adınız BOŞLUK karakteri içeremez.", "Tamam");
                    return;
                }
                else if (EntryPassword.Text != EntryPassword2.Text)
                {
                    await DisplayAlert("Durum", "Parolalar eşleşmiyor !", "Tamam");
                    return;
                }
                else  if ((EntryPassword.Text == "") || (EntryUsername.Text == "") || (EntryEmail.Text == ""))
                {
                    await DisplayAlert("Durum", "Boşluk Bırakılamaz !", "Tamam");
                    return;
                }
                else  if ((EntryPassword.Text.Trim() == "") || (EntryUsername.Text.Trim() == "") || (EntryEmail.Text.Trim() == ""))
                {
                    await DisplayAlert("Durum", "Boşluk Bırakılamaz !", "Tamam");
                    return;
                }
                else if ((EntryPassword.Text.Length < 8) || (EntryPassword.Text.Length > 20))
                {
                    await DisplayAlert("Durum", "Parola en az 8 en fazla 20 karakter uzunluğunda olmalıdır. !", "Tamam");
                    return;
                }
                else if ((EntryUsername.Text.Length < 4) || (EntryUsername.Text.Length > 20))
                {
                    await DisplayAlert("Durum", "Kullanıcı Adı en az 4 en falza 20 karakter uzunluğunda olmalıdır. !", "Tamam");
                    return;
                }
                else if ((EntryEmail.Text.Length < 8) || (EntryEmail.Text.Length > 50))
                {
                    await DisplayAlert("Durum", "E-mail en az 8 en fazla 50 karakter uzunluğunda olmalıdır. !", "Tamam");
                    return;
                }
                string url = "http://arifcig.net/user_save.php?e_mail=" + EntryEmail.Text + "&username=" + EntryUsername.Text + "&password=" + EntryPassword.Text + "&secretToken=A.t541541";
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = "GET";
                WebResponse myResponse = myRequest.GetResponse();
                StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);//System.Text.Encoding.ASCII
                string result1 = sr.CurrentEncoding.HeaderName.ToString();
                string result3 = sr.ReadToEnd();
                sr.Close();
                myResponse.Close();
                if (result3 == "Böyle bir kullanıcı zaten mevcut." + EntryUsername.Text + "<br>")
                {
                    await DisplayAlert("Durum", "Kayıtlı kullanıcı adı !", "Tamam");
                    return;
                }
                else if (result3 == "Böyle bir e-mail zaten mevcut." + EntryEmail.Text + "<br>")
                {
                    await DisplayAlert("Durum", "Kayıtlı e-mail !", "Tamam");
                    return;
                }
                else if (result3 == "Yeni kayıt oluşturuldu.")
                {
                    await DisplayAlert("Durum", "Kullanıcı başarıyla oluşturuldu !", "Tamam");
                    Application.Current.MainPage = new LoginPage();
                    return;
                }
                else
                {
                    await DisplayAlert("Durum", "Hata Oluştu !", "Tamam");
                    return;
                }

            
        }
        void BtnGeriClicked(object sender, EventArgs args)
        {
            Application.Current.MainPage = new LoginPage();
        }
    }
}