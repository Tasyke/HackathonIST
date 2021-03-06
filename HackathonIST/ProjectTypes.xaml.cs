﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NetworkLibrary;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HackathonIST.back;

namespace HackathonIST
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectTypes : ContentPage
    {
        public ObservableCollection<Buildings> _buildings { get; set; }

        public ProjectTypes()
        {
            InitializeComponent();

            _buildings = new ObservableCollection<Buildings>();
            UpdateBuildings();
            this.BindingContext = this;
            BuildingsList.ItemsSource = _buildings;

            constrTitle.Text = "Наименование стройки";
            constrAddress.Text = "Адрес стройки";
        }

        private void UpdateBuildings()
		{
            NetworkClient client = new NetworkClient();
            client.ConnectToServer();
            string[] dataBuildings = client.GetBuilderConstructions();
            client.CloseConnection();

            _buildings.Clear();

            if (dataBuildings.Length > 0)
            {
                for (int i = 0; i < dataBuildings.Length; i++)
                {
                    string[] construction = dataBuildings[i].Split(new string[] { "<;>" }, StringSplitOptions.None);
                    _buildings.Add(new Buildings() { BuildingsID = int.Parse(construction[0]), Adress = construction[1], Name = construction[2] });
                }
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //_buildings.Add(new Buildings { Name = "Galaxy S8", Adress = "Samsung"});
            UpdateBuildings();
        }

		private void Button_Clicked_1(object sender, EventArgs e)
		{
            var item = BuildingsList.SelectedItem as Buildings;

            BuilderData.constructionID = item.BuildingsID;
            NetworkClient client = new NetworkClient();
            client.ConnectToServer();
            client.SetConstructionToBuilder();
            client.CloseConnection();
            ConstructionData.isChoosen = true;
            ConstructionData.constructionName = item.Name;
            ConstructionData.constructionAddress = item.Adress;

            constrTitle.Text = ConstructionData.constructionName;
            constrAddress.Text = ConstructionData.constructionAddress;
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
