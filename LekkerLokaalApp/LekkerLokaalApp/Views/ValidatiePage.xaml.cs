using LekkerLokaalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LekkerLokaalApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ValidatiePage : ContentPage
	{
		public ValidatiePage ()
		{
			InitializeComponent ();
            Init();
        }

        private void Init()
        {
            MasterLayout.BackgroundColor = Color.White;
            Lbl_Melding.TextColor = Constants.MainTextColor;
            VerificatieStatus.TextColor = Constants.VerificatieGeslaagd;
            VerificatieAfbeelding.HeightRequest = Constants.VerificatieHeight;
        }

        private void TerugNaarBegin(object sender, EventArgs e)
        {
            TerugNaarRoot();
        }

        private async void TerugNaarRoot()
        {
            await Navigation.PopToRootAsync();
        }
    }
}