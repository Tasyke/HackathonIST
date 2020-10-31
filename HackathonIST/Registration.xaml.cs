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
    public partial class Registration : ContentPage
    {
        public Registration()
        {
            InitializeComponent();
        }
        private void Registration_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EnPhone.Text))
            {
                EnPhone.BackgroundColor = Color.FromHex("#FC4444");
            }
            else
            {
                EnPhone.BackgroundColor = Color.White;
            }
            if (string.IsNullOrEmpty(EnMail.Text))
            {
                EnMail.BackgroundColor = Color.FromHex("#FC4444");
            }
            else
            {
                EnMail.BackgroundColor = Color.White;
            }
            if (string.IsNullOrEmpty(EnPass.Text))
            {
                EnPass.BackgroundColor = Color.FromHex("#FC4444");
            }
            else
            {
                EnPass.BackgroundColor = Color.White;
            }
            if (string.IsNullOrEmpty(EnSurname.Text))
            {
                EnSurname.BackgroundColor = Color.FromHex("#FC4444");
            }
            else
            {
                EnSurname.BackgroundColor = Color.White;
            }
            if (string.IsNullOrEmpty(EnName.Text))
            {
                EnName.BackgroundColor = Color.FromHex("#FC4444");
            }
            else
            {
                EnName.BackgroundColor = Color.White;
            }
            if (string.IsNullOrEmpty(EnOtch.Text))
            {
                EnOtch.BackgroundColor = Color.FromHex("#FC4444");
            }
            else
            {
                EnOtch.BackgroundColor = Color.White;
            }
            if (string.IsNullOrEmpty(EnFirma.Text))
            {
                EnFirma.BackgroundColor = Color.FromHex("#FC4444");
            }
            else
            {
                EnFirma.BackgroundColor = Color.White;
            }
        }
    }
}