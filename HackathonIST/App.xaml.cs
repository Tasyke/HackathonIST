﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HackathonIST
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new BuilderStart();
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