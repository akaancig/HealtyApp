using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GrProject.Views.TrainerView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Usr_BeslenmeEkle : ContentPage
    {
        public Usr_BeslenmeEkle()
        {
            InitializeComponent();
            Title = Usr_Beslenme.sta_username + " Beslenme Ekle";
            EntryCode.Text = "";
            doldur();
            
        }
        async void doldur()
        {
            try
            {
                var monkeyList = new List<string>();
                string oSonuc = await BeslenmePage.func();
                string[] separatingStrings = { "<br>" };
                string[] users = oSonuc.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
                foreach (var user in users)
                {
                    monkeyList.Add(user);
                }

                besinPicker.ItemsSource = monkeyList;
            }
            catch (Exception)
            {
                return;
            }
        }
        async void OnKaydetClicked(object sender, EventArgs args)
        {
            string gun = "";
            string besinAdi = "";
            string miktar = "";
            if (gunPicker.SelectedIndex != -1) gun = gunPicker.Items[gunPicker.SelectedIndex];
            else { await DisplayAlert("Durum", "Gün seçilmelidir.", "Tamam"); return; }
            if (besinPicker.SelectedIndex != -1) besinAdi = besinPicker.Items[besinPicker.SelectedIndex];
            else { await DisplayAlert("Durum", "Besin seçilmelidir.", "Tamam"); return; }
            if (EntryCode.Text == "") { await DisplayAlert("Durum", "Miktar belirtilmelidir.", "Tamam"); return; }
            else if(EntryCode.Text.Trim() == "") { await DisplayAlert("Durum", "Miktar belirtilmelidir.", "Tamam"); return; }
            else { miktar = EntryCode.Text; }
            if (gun == "Pazartesi") gun = "1";
            else if (gun == "Salı") gun = "2";
            else if (gun == "Çarşamba") gun = "3";
            else if (gun == "Perşembe") gun = "4";
            else if (gun == "Cuma") gun = "5";
            else if (gun == "Cumartesi") gun = "6";
            else if (gun == "Pazar") gun = "7";
            else gun = "8";
            try
            {
                string url = "http://arifcig.net/save_usrBeslenme.php?username=" + LoginPage.CurrentUser + "&password=" + LoginPage.CurrenPass + "&secretToken=A.t541541&person="+Usr_Beslenme.sta_username+ "&gun=" +gun+"&besinAdi=" + besinAdi +"&miktar="+miktar;
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = "GET";
                WebResponse myResponse = myRequest.GetResponse();
                StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);//System.Text.Encoding.ASCII
                string result1 = sr.CurrentEncoding.HeaderName.ToString();
                string result3 = sr.ReadToEnd();
                sr.Close();
                myResponse.Close();
                if (result3 == "Kayıt başarılı.")
                    await DisplayAlert("Durum", "Başarıyla kaydedildi.", "Tamam");
                else {
                    await DisplayAlert("Durum", "Kaydedilirken hata !", "Tamam");
                }
            }
            catch
            {
                await DisplayAlert("Durum", "Beklenmedik bir hata oluştu lütfen iletişime geçin.", "Tamam");
                return;
            }
        }
        async void OnGeriClicked(object sender, EventArgs args)
        {
            await Navigation.PopAsync();
        }
    }
}