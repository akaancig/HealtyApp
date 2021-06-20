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
    public partial class Usr_Antreman : ContentPage
    {
        static string errKullanicilar = "";
        public static string sta_username = "";
        public Usr_Antreman(string userName)
        {
            InitializeComponent();
            sta_username = userName;
            Title = userName + " Antremanı";
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet) kullaniciCek(userName);
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
            if (current == NetworkAccess.Internet) kullaniciCek(sta_username);
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
        async void func22()
        {
            await Navigation.PushAsync(new Usr_AntremanEkle());
        }
        async void func23(string hrkt)
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                try
                {
                    string url = "http://arifcig.net/del_usrAntreman.php?username=" + LoginPage.CurrentUser + "&password=" + LoginPage.CurrenPass + "&secretToken=A.t541541" + "&person=" + sta_username + "&ID=" + hrkt;
                    HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                    myRequest.Method = "GET";
                    WebResponse myResponse = myRequest.GetResponse();
                    StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
                    string result1 = sr.CurrentEncoding.HeaderName.ToString();
                    string result3 = sr.ReadToEnd();
                    sr.Close();
                    myResponse.Close();
                    if (result3 == "Kayıt silindi.")
                    {
                        await DisplayAlert("Durum", "Kayıt başarıyla silindi.", "Tamam");
                        OnYenile(new object(), new EventArgs());
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
        public static Task<string> func(string userName)
        {
            string kullanici = LoginPage.CurrentUser;
            string sifre = LoginPage.CurrenPass;
            Task<string> islem = Task.Run<string>(() =>
            {
                try
                {
                    string url = "http://arifcig.net/get_uaTable.php?username=" + kullanici + "&password=" + sifre + "&secretToken=A.t541541&person=" + userName;
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
        public async void kullaniciCek(string userName)
        {
            string oSonuc = await func(userName);
            if (errKullanicilar != "")
            {
                await DisplayAlert("Hata Oluştu", errKullanicilar, "Tamam");
                await Navigation.PopAsync();
                errKullanicilar = "";
            }
            string[] separatingStrings = { "<br>" };
            string[] users = oSonuc.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

            List<Grid> grids = new List<Grid>();

            Grid grid1 = new Grid();
            grid1.RowDefinitions = new RowDefinitionCollection();
            grid1.ColumnDefinitions = new ColumnDefinitionCollection();
            int count = 1;
            grid1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3.0, GridUnitType.Star), });
            grid1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(70) });
            Grid grd = new Grid
            {
                RowDefinitions =
                            {
                                new RowDefinition { Height = new GridLength(50) },
                            },
                ColumnDefinitions =
                            {
                                new ColumnDefinition{Width = new GridLength(100, GridUnitType.Star) },
                            },
            };
            grd.Children.Add(new Frame { Content = new Button { Text = " (+)   Yeni Antreman Düzeni Ekleme   (+) ", TextColor = Color.White, BackgroundColor = Color.DarkBlue, VerticalOptions = LayoutOptions.Fill, HorizontalOptions = LayoutOptions.Fill, Command = new Command(execute: () => { func22(); }) }, BackgroundColor = Color.Transparent, VerticalOptions = LayoutOptions.Fill, HorizontalOptions = LayoutOptions.Fill, Padding = new Thickness(0), Margin = new Thickness(0), CornerRadius = 9 }, 0, 0);
            grid1.Children.Add(new Frame
            {
                Content = grd,
                CornerRadius = 9,
                Margin = new Thickness(15, 0, 15, 20),
                Padding = new Thickness(0, 0, 0, 0),
                BackgroundColor = Color.FromHex("#EFFD5F"),
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
            },
                            0, 0);

            foreach (var user in users)
            {
                string[] separatingStrings2 = { "," };
                string[] users2 = user.Split(separatingStrings2, System.StringSplitOptions.None);

                // users2[0] -> id
                // users2[1] -> gun
                // users2[2] -> tanim
                // users2[3] -> miktar
                Grid grd2 = new Grid
                {
                    RowDefinitions =
                            {
                                new RowDefinition { Height = new GridLength(100) },
                            },
                    ColumnDefinitions =
                            {
                                new ColumnDefinition{Width = new GridLength(85, GridUnitType.Star) },
                                new ColumnDefinition{Width = new GridLength(15, GridUnitType.Star) },
                            },
                };
                //grd2.Children.Add(new Frame { Content = new Button { BackgroundColor = Color.FromHex("#cde8f6"), TextColor = Color.Black, Command = new Command<string>(execute: (string str) => { func22(str); }), CommandParameter = adSoyad + "-" + tcKimlikNo + "-" + telefonNumarasi + "-" + bildirimTarihi, Text = "İzlem Yap", }, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, Padding = new Thickness(0), Margin = new Thickness(0, 3, 3, 3), CornerRadius = 9 })
                grd2.Children.Add(new Frame { Content = new Label { Text = users2[1]+".Gün  -  " + "Hareket: " + users2[2] + "  -  Tekrar/Set: " + users2[3], HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center, TextColor = Color.DarkBlue, FontSize = 14, FontAttributes = FontAttributes.Bold | FontAttributes.Italic }, BackgroundColor = Color.Transparent, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, Padding = new Thickness(0), Margin = new Thickness(0, 3, 3, 3), CornerRadius = 9 }, 0, 0);
                grd2.Children.Add(new Frame { Content = new Button { Text = "Sil", BackgroundColor = Color.LightBlue, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, Command = new Command<string>(execute: (string str) => { func23(str); }), CommandParameter = users2[0] }, BackgroundColor = Color.Transparent, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, Padding = new Thickness(0), Margin = new Thickness(0, 3, 2, 3), CornerRadius = 9 }, 1, 0);
                if (Convert.ToInt32(users2[1]) % 4 == 0)
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
                else if (Convert.ToInt32(users2[1]) % 4 == 1)
                    grid1.Children.Add(new Frame
                    {
                        Content = grd2,
                        CornerRadius = 16,
                        Margin = new Thickness(7, 5, 7, 5),
                        Padding = new Thickness(8),
                        BackgroundColor = Color.Wheat,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,
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
                        HorizontalOptions = LayoutOptions.Center,
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
        }
    }
}