using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using NetworkLibrary;

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

        private void LoginButton_OnClicked(object sender, EventArgs e)
        {
            //var login = LoginText.Text;
            //if (App.Current.Properties.ContainsKey("Login"))
            //{
            //    if (login == "a")
            //    {
            //        SideTab worker1 = new SideTab();
            //        Application.Current.MainPage = new NavigationPage(worker1);
            //    }
            //}
            //else
            //{
            //    App.Current.Properties.Add("Login", login);
            //    if (login == "a")
            //    {
            //        SideTab worker1 = new SideTab();
            //        Application.Current.MainPage = new NavigationPage(worker1);
            //    }
            //}
            
            string login = LoginText.Text;
            string password = entry3.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return;

            NetworkClient client = new NetworkClient();
            client.ConnectToServer();
            bool isAuth = client.SendAuthentificationRequest(new string[] { login, password });
            client.CloseConnection();

            if (isAuth)
            {
                SideTab worker1 = new SideTab();
                Application.Current.MainPage = new NavigationPage(worker1);
            }
        }

        private async void RegisterButton_OnCLiked(object sender, EventArgs e)
        {
            Registration registration = new Registration();
            await Navigation.PushAsync(registration);
            
        }
    }
}