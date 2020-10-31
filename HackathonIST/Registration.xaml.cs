using System;
using NetworkLibrary;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HackathonIST
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registration : ContentPage
    {
        public Registration()
        {
            InitializeComponent();
        }
        private async void Registration_Clicked(object sender, EventArgs e)
        {
            bool notEmptyFields = true;

            if (string.IsNullOrEmpty(EnPhone.Text))
            {
                notEmptyFields = false;
                EnPhone.BackgroundColor = Color.FromHex("#FC4444");
            }
            else
            {
                EnPhone.BackgroundColor = Color.White;
            }
            if (string.IsNullOrEmpty(EnMail.Text))
            {
                notEmptyFields = false;
                EnMail.BackgroundColor = Color.FromHex("#FC4444");
            }
            else
            {
                EnMail.BackgroundColor = Color.White;
            }
            if (string.IsNullOrEmpty(EnPass.Text))
            {
                notEmptyFields = false;
                EnPass.BackgroundColor = Color.FromHex("#FC4444");
            }
            else
            {
                EnPass.BackgroundColor = Color.White;
            }
            if (string.IsNullOrEmpty(EnSurname.Text))
            {
                notEmptyFields = false;
                EnSurname.BackgroundColor = Color.FromHex("#FC4444");
            }
            else
            {
                EnSurname.BackgroundColor = Color.White;
            }
            if (string.IsNullOrEmpty(EnName.Text))
            {
                notEmptyFields = false;
                EnName.BackgroundColor = Color.FromHex("#FC4444");
            }
            else
            {
                EnName.BackgroundColor = Color.White;
            }
            if (string.IsNullOrEmpty(EnOtch.Text))
            {
                notEmptyFields = false;
                EnOtch.BackgroundColor = Color.FromHex("#FC4444");
            }
            else
            {
                EnOtch.BackgroundColor = Color.White;
            }
            if (string.IsNullOrEmpty(EnFirma.Text))
            {
                notEmptyFields = false;
                EnFirma.BackgroundColor = Color.FromHex("#FC4444");
            }
            else
            {
                EnFirma.BackgroundColor = Color.White;
            }
    
            if (notEmptyFields)
			{
                NetworkClient client = new NetworkClient();
                client.ConnectToServer();
                string[] regData = new string[] { EnName.Text, EnSurname.Text, EnOtch.Text, EnPhone.Text, EnFirma.Text, EnMail.Text, EnPass.Text };
                bool result = client.SendRegisterRequest(regData);
                client.CloseConnection();

                if (result)
				{
                    await Navigation.PopAsync();
                }
			}
        }
    }
}