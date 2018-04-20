using LekkerLokaalApp.Models;
using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LekkerLokaalApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VerificatiePage : ContentPage
	{
        private const string url = "https://www.bramdeconinck.com/apps/lekkerlokaal/v1/cadeaubon/";
        private HttpClient _Client = new HttpClient(new NativeMessageHandler());
        private Cadeaubon cadeaubon;
        private bool Valideerbaar;

        public VerificatiePage (Handelaar handelaar, string qrcode)
		{
			InitializeComponent ();
            Init();
            VerifieerQRCode(handelaar, qrcode);
        }

        private void Init()
        {
            MasterLayout.BackgroundColor = Constants.BackgroundColor;
            Lbl_Melding.TextColor = Constants.MainTextColor;
            VerificatieStatus.TextColor = Constants.VerificatieMislukt;
            VerificatieAfbeelding.HeightRequest = Constants.VerificatieHeight;

            Valideerbaar = false;
        }

        private async void VerifieerQRCode(Handelaar handelaar, string qrcode)
        {
            try
            {
                var content = await _Client.GetStringAsync(url + "/" + qrcode);
                var cadeaubonListTemp = JsonConvert.DeserializeObject<List<Cadeaubon>>(content);
                cadeaubon = cadeaubonListTemp[0];

                switch (cadeaubon.Geldigheid)
                {
                    case 0:
                        if (cadeaubon.AanmaakDatum.Date.AddYears(1) <= DateTime.Today.Date)
                        {
                            Lbl_Melding.Text = "Deze cadeaubon is meer dan één jaar oud en daardoor niet meer bruikbaar.";
                            cadeaubon.Geldigheid = 2;
                            await CadeaubonPut(cadeaubon);
                        }
                        else if (cadeaubon.Gebruikersnaam != handelaar.Gebruikersnaam && cadeaubon.Gebruikersnaam != "generiek")
                        {
                            Lbl_Melding.Text = "De ingescande QR-code is niet bruikbaar in deze winkel.";
                        }
                        else
                        {
                            VerificatieAfbeelding.Source = "Vinkje.png";
                            VerificatieStatus.TextColor = Constants.VerificatieGeslaagd;
                            ValidatieFrame.BackgroundColor = Constants.VerificatieGeslaagd;
                            VerificatieStatus.Text = "Verificatie geslaagd!";

                            Lbl_Melding.Text = String.Format("Naam: {0} \n\nBedrag: € {1}", cadeaubon.Naam, cadeaubon.Prijs);
                            Btn_Procedure.Text = "Valideer cadeaubon";
                        }
                        break;
                    case 1:
                        Lbl_Melding.Text = "Deze cadeaubon werd ongeldig verklaard.";
                        break;
                    case 2:
                        Lbl_Melding.Text = "Deze cadeaubon is meer dan één jaar oud en daardoor niet meer bruikbaar.";
                        break;
                    case 3:
                        Lbl_Melding.Text = "Deze cadeaubon is reeds gebruikt.";
                        break;
                    default:
                        Lbl_Melding.Text = "Er is een fout opgetreden bij het ophalen van de gegevens.";
                        break;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                Lbl_Melding.Text = "De ingescande QR-code is ongeldig.";
            }
            catch (Exception)
            {
                Lbl_Melding.Text = "Er kan op dit moment geen verbinding worden gemaakt met het internet.";
            }
        }

        private async Task CadeaubonPut(Cadeaubon cadeaubon)
        {
            var json = JsonConvert.SerializeObject(cadeaubon);
            HttpContent content = new StringContent(json);
            var response = await _Client.PutAsync(url + "/" + cadeaubon.BestelLijnId, content);
        }

        private void Procedure(object sender, EventArgs e)
        {
            if (Valideerbaar)
            {
                DisplayAlert("Aanmelding", "Validatie", "Oke");
            }
            else
            {

            }
        }
    }
}