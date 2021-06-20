using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GrProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageDetail : ContentPage
    {
        public MainPageDetail()
        {
            InitializeComponent();
            KullaniciAdi.Text = "Hoş Geldiniz, Sayın " + LoginPage.CurrentUser.ToUpper();
        }
        async void OnAntremanClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new AntremanPage());
        }
        async void OnBeslenmeClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new BeslenmePage());
        }
    }
}