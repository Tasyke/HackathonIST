using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HackathonIST
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuilderStart : ContentPage
    {
        public BuilderStart()
        {
            InitializeComponent();
        }

        private async void Logout_OnClick(object sender, EventArgs e)
        {
            App.Current.Properties.Remove("Login");

            App.Current.MainPage = new NavigationPage(new Authorization());
        }
    }
}