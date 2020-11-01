using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using NetworkLibrary;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HackathonIST.back;

namespace HackathonIST
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuilderStart : ContentPage
    {
        //WorkTimer Timer = new WorkTimer();
        // SOSButton SendSOS = new SOSButton();
        // AcceleratorFunc CheckAcceleration = new AcceleratorFunc();
        bool alive = true;
        CancellationTokenSource cts;

        Stopwatch mStopWatch = new Stopwatch();


        public BuilderStart()
        {
            InitializeComponent();
        }

        private void SOSButton_Clicked(object sender, EventArgs e)
        {
            //SendSOS.SOSCall();
        }

        private void StartDay_Clicked(object sender, EventArgs e)
        {
            
            ButtonDS.IsVisible = false;
            ButtonED.IsVisible = true;
            SOSbutton.IsVisible = true;
            OnWork.IsVisible = true;

            CheckGeolocation();

            NetworkClient client = new NetworkClient();
            client.ConnectToServer();
            client.SendStartWorkRequest();
            client.CloseConnection();

            //CheckAcceleration.ToggleAccelerator();
            //OnWork.Text=Timer.ToogleStopwatch();
            
            if (!mStopWatch.IsRunning)
            {
                mStopWatch.Start();
            }

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                // Do something

                OnWork.Text = mStopWatch.Elapsed.ToString();

                return true; // True = Repeat again, False = Stop the timer
            });
        }
        private void EndDay_Clicked(object sender, EventArgs e)
        {
            ButtonDS.IsVisible = true;
            ButtonED.IsVisible = false;
            SOSbutton.IsVisible = false;
            OnWork.IsVisible = false;
            alive = false;

            NetworkClient client = new NetworkClient();
            client.ConnectToServer();
            client.SendEndWorkRequest();
            client.CloseConnection();

            //OnWork.Text = Timer.DisableStopwatch();

            //CheckAcceleration.DisableAccelerator();
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
                    BuilderData.geoLocation = location.Latitude + " " + location.Longitude;

                    NetworkClient client = new NetworkClient();
                    client.ConnectToServer();
                    client.SetBuilderLastGeoLocation();
                    client.CloseConnection();
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
 
