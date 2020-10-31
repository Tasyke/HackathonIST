using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HackathonIST
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectTypes : ContentPage
    {
        public ObservableCollection<Buildings> _buildings { get; set; }

        public ProjectTypes()
        {
            InitializeComponent();

            _buildings = new ObservableCollection<Buildings>
            {
                new Buildings{BuildingsID = 0, Name = "body" ,Adress = "Some"},
                new Buildings{BuildingsID = 1, Name = "iiii" ,Adress = "ddd"},
                new Buildings{BuildingsID = 0, Name = "body" ,Adress = "Some"},
                new Buildings{BuildingsID = 1, Name = "iiii" ,Adress = "ddd"},
                new Buildings{BuildingsID = 0, Name = "body" ,Adress = "Some"},
                new Buildings{BuildingsID = 1, Name = "iiii" ,Adress = "ddd"},
                new Buildings{BuildingsID = 0, Name = "body" ,Adress = "Some"},
                new Buildings{BuildingsID = 1, Name = "iiii" ,Adress = "ddd"},
                new Buildings{BuildingsID = 0, Name = "body" ,Adress = "Some"},
                new Buildings{BuildingsID = 1, Name = "iiii" ,Adress = "ddd"},
                new Buildings{BuildingsID = 0, Name = "body" ,Adress = "Some"},
                new Buildings{BuildingsID = 1, Name = "iiii" ,Adress = "ddd"},
                new Buildings{BuildingsID = 0, Name = "body" ,Adress = "Some"},
                new Buildings{BuildingsID = 1, Name = "iiii" ,Adress = "ddd"},
                new Buildings{BuildingsID = 0, Name = "body" ,Adress = "Some"},
                new Buildings{BuildingsID = 1, Name = "iiii" ,Adress = "ddd"},
                new Buildings{BuildingsID = 0, Name = "body" ,Adress = "Some"},
                new Buildings{BuildingsID = 1, Name = "iiii" ,Adress = "ddd"},
                new Buildings{BuildingsID = 0, Name = "body" ,Adress = "Some"},
                new Buildings{BuildingsID = 1, Name = "iiii" ,Adress = "ddd"}
            };
            this.BindingContext = this;
            BuildingsList.ItemsSource = _buildings;
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            //_buildings.Add(new Buildings { Name = "Galaxy S8", Adress = "Samsung"});
        }
    }

    //public async void OnItemTapped(object sender, ItemTappedEventArgs e)
    //{
    //    Buildings selectedPhone = e.Item as Buildings;
    //    if (selectedPhone != null)
    //        await DisplayAlert("Выбранная модель", $"{selectedPhone.Name} - {selectedPhone.Adress}", "OK");
    //}
    public class Buildings
    {
        public int BuildingsID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
    }
}
