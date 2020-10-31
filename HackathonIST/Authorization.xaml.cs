using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HackathonIST
{
    public partial class Authorization : ContentPage
    {
        public Authorization()
        {
            InitializeComponent();
        }

        protected internal void DisplayStack()
        {
            NavigationPage navPage = (NavigationPage)App.Current.MainPage;
        }

        private async void LoginButton_OnClicked(object sender, EventArgs e)
        {
            var login = LoginText.Text;

            if (login == "a")
            {
                SideTab worker1 = new SideTab();
                await Navigation.PushAsync(worker1);
            }
            else if (login == "b")
            {
                BuilderStart worker1 = new BuilderStart();
                await Navigation.PushAsync(worker1);
            }

        }
    }
}