using LekkerLokaalApp.Data;
using LekkerLokaalApp.Models;
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
        static HandelaarDatabaseController handelaarDatabase;

        public App ()
		{
			InitializeComponent();

            MainPage = new NavigationPage(new LekkerLokaalApp.Views.LoginPage());
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

        public static HandelaarDatabaseController HandelaarDatabase
        {
            get
            {
                if (handelaarDatabase == null)
                {
                    handelaarDatabase = new HandelaarDatabaseController();
                }
                return handelaarDatabase;
            }
        }
    }
}
