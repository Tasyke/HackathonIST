using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HackathonIST.back;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HackathonIST
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuilderStats : ContentPage
    {
        public BuilderStats()
        {
            InitializeComponent();
            UpdateInfo();
        }

        private void UpdateInfo()
		{
            EnName.Text = BuilderData.builderName;
            EnLastName.Text = BuilderData.builderLastName;
            EnSurName.Text = BuilderData.builderSurName;
            EnPhone.Text = BuilderData.builderPhone;
            EnEmail.Text = BuilderData.builderEmail;
            EnCompany.Text = BuilderData.builderMainCompany;
		}
    }
}