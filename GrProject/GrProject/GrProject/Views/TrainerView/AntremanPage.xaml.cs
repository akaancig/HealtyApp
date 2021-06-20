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
    public partial class AntremanPage : ContentPage
    {
        public static string errHareket = "";
        public AntremanPage()
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
        void onSaveClicked()
        {

        }
        async void func22()
        {
            await Navigation.PushAsync(new NewHareket());
        }
        async void func23(string hrkt)
        {
            bool answer = await DisplayAlert("Emin misin ?", "Silmek istediğinize emin misiniz ? " + hrkt, "Evet", "Hayır");
            if (answer)
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    try
                    {
                        string url = "http://arifcig.net/hareket_delete.php?username=" + LoginPage.CurrentUser + "&password=" + LoginPage.CurrenPass + "&secretToken=A.t541541" + "&hareket=" + hrkt;
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
        public async void kullaniciCek()
        {
            string oSonuc = await func();
            if (errHareket != "")
            {
                await DisplayAlert("Hata Oluştu", errHareket, "Tamam");
                await Navigation.PopAsync();
                errHareket = "";
            }
            string[] separatingStrings = { "<br>" };
            string[] users = oSonuc.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

            List<Grid> grids = new List<Grid>();

            Grid grid1 = new Grid();
            grid1.RowDefinitions = new RowDefinitionCollection();
            grid1.ColumnDefinitions = new ColumnDefinitionCollection();
            int count = 1;
            grid1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3.0, GridUnitType.Star), });
            grid1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(70)});
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
            grd.Children.Add(new Frame { Content = new Button { Text = " (+)   Yeni Hareket Ekleme   (+) ",TextColor = Color.White, BackgroundColor = Color.DarkBlue, VerticalOptions = LayoutOptions.Fill, HorizontalOptions = LayoutOptions.Fill, Command = new Command(execute: () => { func22(); }) }, BackgroundColor = Color.Transparent, VerticalOptions = LayoutOptions.Fill, HorizontalOptions = LayoutOptions.Fill, Padding = new Thickness(0), Margin = new Thickness(0), CornerRadius = 9 }, 0, 0);
            grid1.Children.Add(new Frame
            {
                Content = grd,
                CornerRadius = 9,
                Margin = new Thickness(15,0,15,20),
                Padding = new Thickness(0,0,0,0),
                BackgroundColor = Color.FromHex("#EFFD5F"),
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
            },
                            0, 0);

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
                                new ColumnDefinition{Width = new GridLength(60, GridUnitType.Star) },
                                new ColumnDefinition{Width = new GridLength(40, GridUnitType.Star) },
                            },
                };
                //grd2.Children.Add(new Frame { Content = new Button { BackgroundColor = Color.FromHex("#cde8f6"), TextColor = Color.Black, Command = new Command<string>(execute: (string str) => { func22(str); }), CommandParameter = adSoyad + "-" + tcKimlikNo + "-" + telefonNumarasi + "-" + bildirimTarihi, Text = "İzlem Yap", }, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, Padding = new Thickness(0), Margin = new Thickness(0, 3, 3, 3), CornerRadius = 9 })
                grd2.Children.Add(new Frame { Content = new Label { Text = user, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center, TextColor = Color.DarkBlue, FontSize = 14, FontAttributes = FontAttributes.Bold | FontAttributes.Italic }, BackgroundColor = Color.Transparent, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, Padding = new Thickness(0), Margin = new Thickness(0, 3, 3, 3), CornerRadius = 9 }, 0, 0);
                grd2.Children.Add(new Frame { Content = new Button { Text = "Sil", BackgroundColor = Color.LightBlue, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, Command = new Command<string>(execute: (string str) => { func23(str); }), CommandParameter = user }, BackgroundColor = Color.Transparent, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, Padding = new Thickness(0), Margin = new Thickness(0, 3, 2, 3), CornerRadius = 9 }, 1, 0);
                grid1.Children.Add(new Frame
                {
                    Content = grd2,
                    CornerRadius = 16,
                    Margin = new Thickness(7, 5, 7, 5),
                    Padding = new Thickness(0),
                    BackgroundColor = Color.LightSalmon,
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
        public static Task<string> func()
        {
            string kullanici = LoginPage.CurrentUser;
            string sifre = LoginPage.CurrenPass;
            Task<string> islem = Task.Run<string>(() =>
            {
                try
                {
                    string url = "http://arifcig.net/get_harekets.php?username=" + kullanici + "&password=" + sifre + "&secretToken=A.t541541";
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
                    errHareket = "Kullanıcıları listelerken hata oluştu lütfen tekrar deneyiniz.";
                    return "Hata";
                }
            });

            return islem;
        }
    }
}