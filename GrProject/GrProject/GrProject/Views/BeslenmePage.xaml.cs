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
    public partial class BeslenmePage : ContentPage
    {
        static string errKullanicilar = "";
        public BeslenmePage()
        {
            InitializeComponent();
            Title = LoginPage.CurrentUser.ToUpper()+" Beslenme Programı";
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
        public static Task<string> func()
        {
            string kullanici = LoginPage.CurrentUser;
            string sifre = LoginPage.CurrenPass;
            Task<string> islem = Task.Run<string>(() =>
            {
                try
                {
                    string url = "http://arifcig.net/get_beslenmePage.php?username=" + kullanici + "&password=" + sifre + "&secretToken=A.t541541";
                    HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                    myRequest.Method = "GET";
                    WebResponse myResponse = myRequest.GetResponse();
                    StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
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
        public async void kullaniciCek()
        {
            string oSonuc = await func();
            if (errKullanicilar != "")
            {
                await DisplayAlert("Hata Oluştu", errKullanicilar, "Tamam");
                await Navigation.PopAsync();
                errKullanicilar = "";
                return;
            }
            if (oSonuc == "")
            {
                Frame frame = new Frame
                {
                    Margin = new Thickness(30, 20, 30, 20),
                    BackgroundColor = Color.LightSalmon,
                    BorderColor = Color.Orange,
                    CornerRadius = 10,
                    HasShadow = true,
                    Content = new Label { Text = "Tanımlı herhangi bir beslenme programınız bulunmamaktadır !", TextColor = Color.White, FontSize = 18, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center },
                    HeightRequest = 150,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                };
                Content = frame;
                return;
            }
            string[] separatingStrings = { "<br>" };
            string[] users = oSonuc.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

            List<Grid> grids = new List<Grid>();

            Grid grid1 = new Grid();
            grid1.RowDefinitions = new RowDefinitionCollection();
            grid1.ColumnDefinitions = new ColumnDefinitionCollection();
            int count = 0;
            grid1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3.0, GridUnitType.Star), });


            foreach (var user in users)
            {
                string[] separatingStrings2 = { "," };
                string[] users2 = user.Split(separatingStrings2, System.StringSplitOptions.None);
                string newGun = "";
                if (users2[1] == "1")
                    newGun = "Pazartesi";
                else if (users2[1] == "2")
                    newGun = "Salı";
                else if (users2[1] == "3")
                    newGun = "Çarşamba";
                else if (users2[1] == "4")
                    newGun = "Perşmebe";
                else if (users2[1] == "5")
                    newGun = "Cuma";
                else if (users2[1] == "6")
                    newGun = "Cumartesi";
                else if (users2[1] == "7")
                    newGun = "Pazar";
                Grid grd2 = new Grid
                {
                    RowDefinitions =
                            {
                                new RowDefinition { Height = new GridLength(100) },
                            },
                    ColumnDefinitions =
                            {
                                new ColumnDefinition{Width = new GridLength(100, GridUnitType.Star) },
                            },
                };
                grd2.Children.Add(new Frame { Content = new Label { Text = newGun + "  -  " + "Besin: " + users2[2] + "  -  Miktar: " + users2[3], HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center, TextColor = Color.DarkBlue, FontSize = 14, FontAttributes = FontAttributes.Bold | FontAttributes.Italic }, BackgroundColor = Color.Transparent, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, Padding = new Thickness(0), Margin = new Thickness(0, 3, 3, 3), CornerRadius = 9 }, 0, 0);
                if (Convert.ToInt32(users2[1]) % 4 == 0)
                    grid1.Children.Add(new Frame
                    {
                        Content = grd2,
                        CornerRadius = 16,
                        Margin = new Thickness(7, 5, 7, 5),
                        Padding = new Thickness(8),
                        BackgroundColor = Color.FromHex("#EFFD5F"),
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Fill,
                    },
                                    0, count);
                else if (Convert.ToInt32(users2[1]) % 4 == 1)
                    grid1.Children.Add(new Frame
                    {
                        Content = grd2,
                        CornerRadius = 16,
                        Margin = new Thickness(7, 5, 7, 5),
                        Padding = new Thickness(8),
                        BackgroundColor = Color.Wheat,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Fill,
                    },
                                    0, count);
                else if (Convert.ToInt32(users2[1]) % 4 == 2)
                    grid1.Children.Add(new Frame
                    {
                        Content = grd2,
                        CornerRadius = 16,
                        Margin = new Thickness(7, 5, 7, 5),
                        Padding = new Thickness(8),
                        BackgroundColor = Color.LightSalmon,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Fill,
                    },
                                    0, count);
                else
                    grid1.Children.Add(new Frame
                    {
                        Content = grd2,
                        CornerRadius = 16,
                        Margin = new Thickness(7, 5, 7, 5),
                        Padding = new Thickness(8),
                        BackgroundColor = Color.LightGreen,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Fill,
                    },
                                    0, count);
                grid1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(140) });
                System.Console.WriteLine($"<{user}>");
                count++;
            }
            ScrollView scr = new ScrollView { Padding = new Thickness(3, 10, 3, 5) };
            scr.Content = grid1;
            Content = scr;
        }
    }
}