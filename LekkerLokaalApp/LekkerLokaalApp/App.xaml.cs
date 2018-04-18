using LekkerLokaalApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace LekkerLokaalApp
{
    public partial class App : Application
	{
        static UserDatabaseController userDatabase;

        public App ()
		{
			InitializeComponent();

            MainPage = new NavigationPage(new LekkerLokaalApp.Views.LoginPage());
            //MainPage = new NavigationPage(new LekkerLokaalApp.MainPage());
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        public static UserDatabaseController UserDatabase
        {
            get
            {
                if (userDatabase == null)
                {
                    userDatabase = new UserDatabaseController();
                }
                return userDatabase;
            }
        }
    }
}
