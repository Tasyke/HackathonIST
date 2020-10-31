using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HackathonIST
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //object login;
            //if (App.Current.Properties.TryGetValue("Login", out login))
            //{
            //   // if (login == "a")
            //   // {
            MainPage = new NavigationPage(new Authorization());
            //   // }
                
            //}
            //else
            //{
            //    MainPage = new NavigationPage(new Authorization());
            //}
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
