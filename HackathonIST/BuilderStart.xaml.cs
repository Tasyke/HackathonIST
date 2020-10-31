using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HackathonIST
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuilderStart : ContentPage
    {
        CancellationTokenSource cts;
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
            CheckGeolocation();
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
            App.Current.MainPage = new NavigationPage( new Authorization());

            await Navigation.PopToRootAsync();
        }

        protected override void OnDisappearing()
        {
            if (cts != null && !cts.IsCancellationRequested)
                cts.Cancel();
            base.OnDisappearing();
        }

        private async void CheckGeolocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(2));
                cts = new CancellationTokenSource();

                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {

                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }

        }
    }
}
 
