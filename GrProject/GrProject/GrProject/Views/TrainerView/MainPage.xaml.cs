using GrProject.Data;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GrProject.Views.TrainerView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainPageMasterMenuItem;
            if (item == null)
                return;

            //var page = (Page)Activator.CreateInstance(item.TargetType);
            //page.Title = item.Title;
            //Detail = new NavigationPage(page);

            if (item.Id == 0)
                Detail = new MainPage();
            if (item.Id == 2)
            {
                string db = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "lodosFit.db3");
                LoginPage.CurrenPass = "";
                LoginPage.CurrentUser = "";
                var dbCon = new SQLiteConnection(db);
                dbCon.DropTable<UserInfo>();
                dbCon.Close();

                Application.Current.MainPage = new LoginPage();

            }

            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}