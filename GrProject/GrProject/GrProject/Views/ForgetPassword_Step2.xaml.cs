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
    public partial class ForgetPassword_Step2 : ContentPage
    {
        public ForgetPassword_Step2()
        {
            InitializeComponent();
            LabelMail.Text = ForgetPassword_Step1.mail;
        }
        void BtnGeriClicked(object sender, EventArgs args)
        {
            Application.Current.MainPage = new ForgetPassword_Step1();
        }
        async void BtnileriClicked(object sender, EventArgs args)
        {
            if (ForgetPassword_Step1.resetCode == EntryCode.Text)
            {
                Application.Current.MainPage = new UpdateLoginInfos();
            }
            else {
                await DisplayAlert("Durum", "Girdiğiniz kod hatalı lütfen tekrar deneyin !", "Tamam");
                return;
            }
        }
    }
}