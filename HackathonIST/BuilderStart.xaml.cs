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

        private void StartDay_Clicked(object sender, EventArgs e)
        {
            ButtonDS.IsVisible = false;
            ButtonED.IsVisible = true;
            SOSbutton.IsVisible = true;
            OnWork.IsVisible = true;
        }
        private void EndDay_Clicked(object sender, EventArgs e)
        {
            ButtonDS.IsVisible = true;
            ButtonED.IsVisible = false;
            SOSbutton.IsVisible = false;
            OnWork.IsVisible = false;
        }
        private async void Logout_OnClick(object sender, EventArgs e)
        {
            //App.Current.Properties.Remove("Login");
            App.Current.MainPage = new Authorization();

            await Navigation.PopToRootAsync();
        }
    }
}