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

namespace GrProject.Views.TrainerView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewBeslenme : ContentPage
    {
        public NewBeslenme()
        {
            InitializeComponent();
        }
        async void OnKaydetClicked(object sender, EventArgs args)
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                try
                {
                    string url = "http://arifcig.net/besin_save.php?username=" + LoginPage.CurrentUser + "&password=" + LoginPage.CurrenPass + "&secretToken=A.t541541" + "&besin=" + EntryCode.Text;
                    HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                    myRequest.Method = "GET";
                    WebResponse myResponse = myRequest.GetResponse();
                    StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);//System.Text.Encoding.ASCII
                    string result1 = sr.CurrentEncoding.HeaderName.ToString();
                    string result3 = sr.ReadToEnd();
                    sr.Close();
                    myResponse.Close();
                    if (result3 == "Yeni kayıt oluşturuldu.")
                    {
                        await Navigation.PopAsync();
                        return;
                    }
                    else if (result3 == "Hata.")
                    {
                        await DisplayAlert("Durum", "Server kaynaklı bir hata oluştu lütfen iletişime geçin.", "Tamam");
                        return;
                    }
                    else if (result3 == "Hatalı kullanıcı adı.")
                    {
                        await DisplayAlert("Durum", "Kullanıcı Adı veya Parola Yanlış !", "Tamam");
                    }
                    else
                    {
                        await DisplayAlert("Durum", "Beklenmedik bir hata oluştu lütfen tekrar deneyin.", "Tamam");
                    }
                }
                catch (Exception)
                {
                    await DisplayAlert("Durum", "Beklenmedik bir hata oluştu lütfen tekrar deneyin.", "Tamam");
                    return;
                }
            }
            else
            {
                await DisplayAlert("Durum", "Bu işlem için internet bağlantısı gereklidir.", "Tamam");
                return;
            }

        }
        async void OnGeriClicked(object sender, EventArgs args)
        {
            await Navigation.PopAsync();
        }
    }
}