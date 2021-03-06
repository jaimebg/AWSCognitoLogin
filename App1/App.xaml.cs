﻿using Amazon.CognitoIdentityProvider;
using Amazon.CognitoSync;
using Amazon.Runtime;
using Amazon.CognitoIdentity;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using Amazon.CognitoSync.Model;

namespace App1
{
    public partial class App : Application
    {
        public static AWSUser user;
        public static AmazonCognitoIdentityProviderClient provider;

        public App()
        {
            InitializeComponent();

            provider = new AmazonCognitoIdentityProviderClient(new AnonymousAWSCredentials(), AWS.RegionEndpoint);

            MainPage = new NavigationPage(new MainPage());
        }

        protected async override void OnStart()
        {
            string secureUser = await SecureStorage.GetAsync("User");
            if (secureUser != null)
            {
                user = JsonConvert.DeserializeObject<AWSUser>(secureUser);
            }
            else
            {
                user = null;
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
