using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using NetworkLibrary;
using HackathonIST.back;

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
           // NavigationPage navPage = (NavigationPage)App.Current.MainPage;
        }

        private async void LoginButton_OnClicked(object sender, EventArgs e)
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
                client = new NetworkClient();
                client.ConnectToServer();
                BuilderData.builderID = client.GetPersonalID(login);
                CheckBuilderData(client.GetBuilderInfoByID());
                client.CloseConnection();
                Application.Current.MainPage = new SideTab();
                await Navigation.PopToRootAsync(true);
            }
        }

        private void CheckBuilderData(string[] data)
		{
            if (data.Length > 0)
			{
                BuilderData.builderName = data[1];
                BuilderData.builderSurName = data[2];
                BuilderData.builderLastName = data[3];
                BuilderData.builderPhone = data[4];
                if (data[5] != "None")
                    BuilderData.constructionID = int.Parse(data[5]);
                BuilderData.builderMainCompany = data[6];
                BuilderData.builderEmail = data[7];
			}
		}

        private async void RegisterButton_OnCLiked(object sender, EventArgs e)
        {
            Registration registration = new Registration();
            await Navigation.PushAsync(registration);
            
        }
    }
}