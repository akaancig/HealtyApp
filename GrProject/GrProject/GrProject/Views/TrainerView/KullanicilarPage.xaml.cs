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
    public partial class KullanicilarPage : ContentPage
    {
        public static string errKullanicilar = "";
        public KullanicilarPage()
        {
            InitializeComponent();
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet) kullaniciCek();
            else
            {
                Frame frame = new Frame
                {
                    Margin = new Thickness(30, 20, 30, 20),
                    BackgroundColor = Color.LightSalmon,
                    BorderColor = Color.Orange,
                    CornerRadius = 10,
                    HasShadow = true,
                    Content = new Label { Text = "Sayfa yüklenemedi, aktif bir internet bağlantınız bulunmamaktadır !", TextColor = Color.White, FontSize = 18, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center },
                    HeightRequest = 150,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                };
                Content = frame;
            }
        }
        async void OnYenile(object sender, EventArgs args)
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet) kullaniciCek();
            else
            {
                Frame frame = new Frame
                {
                    Margin = new Thickness(30, 20, 30, 20),
                    BackgroundColor = Color.LightSalmon,
                    BorderColor = Color.Orange,
                    CornerRadius = 10,
                    HasShadow = true,
                    Content = new Label { Text = "Sayfa yüklenemedi, aktif bir internet bağlantınız bulunmamaktadır !", TextColor = Color.White, FontSize = 18, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center },
                    HeightRequest = 150,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                };
                Content = frame;
            }
        }
        async void funcBeslenme(string hrkt)
        {
            await Navigation.PushAsync(new Usr_Beslenme(hrkt));
        }
        async void funcAntreman(string hrkt)
        {
            await Navigation.PushAsync(new Usr_Antreman(hrkt));
        }
        public async void kullaniciCek()
        {
            string oSonuc = await func();
            if (errKullanicilar != "")
            {
                await DisplayAlert("Hata Oluştu", errKullanicilar, "Tamam");
                await Navigation.PopAsync();
                errKullanicilar = "";
            }
            string[] separatingStrings = {"<br>"};
            string[] users = oSonuc.Split(separatingStrings,System.StringSplitOptions.RemoveEmptyEntries);

            List<Grid> grids = new List<Grid>();

            Grid grid1 = new Grid();
            grid1.RowDefinitions = new RowDefinitionCollection();
            grid1.ColumnDefinitions = new ColumnDefinitionCollection();
            int count = 0;
            grid1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3.0, GridUnitType.Star), });

            foreach (var user in users)
            {
                Grid grd2 = new Grid
                {
                    RowDefinitions =
                            {
                                new RowDefinition { Height = new GridLength(100) },
                            },
                    ColumnDefinitions =
                            {
                                new ColumnDefinition{Width = new GridLength(40, GridUnitType.Star) },
                                new ColumnDefinition{Width = new GridLength(30, GridUnitType.Star) },
                                new ColumnDefinition{Width = new GridLength(30, GridUnitType.Star) },
                            },
                };
                //grd2.Children.Add(new Frame { Content = new Button { BackgroundColor = Color.FromHex("#cde8f6"), TextColor = Color.Black, Command = new Command<string>(execute: (string str) => { func22(str); }), CommandParameter = adSoyad + "-" + tcKimlikNo + "-" + telefonNumarasi + "-" + bildirimTarihi, Text = "İzlem Yap", }, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, Padding = new Thickness(0), Margin = new Thickness(0, 3, 3, 3), CornerRadius = 9 })
                grd2.Children.Add(new Frame { Content =  new Label { Text = user, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center, TextColor = Color.DarkBlue, FontSize = 14,FontAttributes = FontAttributes.Bold | FontAttributes.Italic },BackgroundColor = Color.Transparent, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, Padding = new Thickness(0), Margin = new Thickness(0, 3, 3, 3), CornerRadius = 9 }, 0, 0);
                grd2.Children.Add(new Frame { Content = new Button { Text = "Beslenme",BackgroundColor=Color.LightBlue,VerticalOptions=LayoutOptions.Center,HorizontalOptions = LayoutOptions.Center, Command = new Command<string>(execute: (string str) => { funcBeslenme(str); }), CommandParameter = user }, BackgroundColor = Color.Transparent, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, Padding = new Thickness(0), Margin = new Thickness(0, 3, 2, 3), CornerRadius = 9 }, 1, 0);
                grd2.Children.Add(new Frame { Content = new Button { Text = "Antreman", BackgroundColor = Color.LightBlue, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, Command = new Command<string>(execute: (string str) => { funcAntreman(str); }), CommandParameter = user }, BackgroundColor = Color.Transparent, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, Padding = new Thickness(0), Margin = new Thickness(0, 3, 2, 3), CornerRadius = 9 }, 2, 0);
                grid1.Children.Add(new Frame
                {
                    Content = grd2,
                    CornerRadius = 16,
                    Margin = new Thickness(7, 5, 7, 5),
                    Padding = new Thickness(8),
                    BackgroundColor = Color.FromHex("#EFFD5F"),
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                },
                                0, count);
                grid1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(140) });
                System.Console.WriteLine($"<{user}>");
                count++;
            }
            ScrollView scr = new ScrollView { Padding = new Thickness(3, 10, 3, 5) };
            scr.Content = grid1;
            Content = scr;
            Title = "Kullanıcılarım (" + Convert.ToString(count) + ")";
        }
        public static Task<string> func()
        {
            string kullanici = LoginPage.CurrentUser;
            string sifre = LoginPage.CurrenPass;
            Task<string> islem = Task.Run<string>(() =>
            {
                try
                {
                    string url = "http://arifcig.net/get_users.php?username=" + kullanici + "&password=" + sifre +"&secretToken=A.t541541";
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
                catch
                {
                    errKullanicilar = "Kullanıcıları listelerken hata oluştu lütfen tekrar deneyiniz.";
                    return "Hata";
                }
            });

            return islem;
        }
    }
}