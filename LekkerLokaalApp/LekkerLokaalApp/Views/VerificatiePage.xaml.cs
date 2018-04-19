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
	public partial class VerificatiePage : ContentPage
	{
		public VerificatiePage (Handelaar handelaar, string qrcode)
		{
			InitializeComponent ();
            Init();

            //Lbl_Status.Text = qrcode;
            //Lbl_Melding.Text = handelaar.Naam;
		}

        private void Init()
        {
            //VerificatieAfbeelding.Source = "Vinkje.png";
            //VerificatieStatus.TextColor = Constants.VerificatieGeslaagd;
            //ValidatieFrame.BackgroundColor = Constants.VerificatieGeslaagd;
            //VerificatieStatus.Text = "Verificatie geslaagd!";

            MasterLayout.BackgroundColor = Constants.BackgroundColor;
            Lbl_Status.TextColor = Constants.MainTextColor;
            Lbl_Melding.TextColor = Constants.MainTextColor;
            VerificatieStatus.TextColor = Constants.VerificatieMislukt;
            VerificatieAfbeelding.HeightRequest = Constants.VerificatieHeight;
        }

        private void VerifieerQRCode(string qrcode)
        {

        }

        private void AfsluitProcedure()
        {

        }

        private void OpnieuwProcedure()
        {

        }

    }
}