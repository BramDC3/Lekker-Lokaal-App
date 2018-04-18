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
using ZXing.Net.Mobile.Forms;

namespace LekkerLokaalApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        private const string url = "https://www.bramdeconinck.com/apps/lekkerlokaal/v1/handelaars/";
        private HttpClient _Client = new HttpClient(new NativeMessageHandler());

        public LoginPage ()
		{
			InitializeComponent ();
            Init();
		}

        private void Init()
        {
            BackgroundColor = Constants.BackgroundColor;
            Lbl_Username.TextColor = Constants.MainTextColor;
            Lbl_Password.TextColor = Constants.MainTextColor;
            ActivitySpinner.IsVisible = false;
            LoginIcon.HeightRequest = Constants.LoginIconHeight;

            Entry_Username.Completed += (s, e) => Entry_Password.Focus();
            Entry_Password.Completed += (s, e) => SignInProcedure(s, e);
        }

        public async void Scanner()
        {
            var ScannerPage = new ZXingScannerPage();

            ScannerPage.OnScanResult += (result) => {
                ScannerPage.IsScanning = false;

                //Wat er moet gebeuren nadat de scanner een QR-code gedecteerd heeft
                Device.BeginInvokeOnMainThread(() => {
                    Navigation.PopAsync();
                    DisplayAlert("Scanresultaat", result.Text, "Oke");
                });
            };

            await Navigation.PushAsync(ScannerPage);
        }

        private async void SignInProcedure(object sender, EventArgs e)
        {
            User user = new User(Entry_Username.Text, Entry_Password.Text);
            try
            {
                if (user.CheckInformation())
                {
                    try
                    {
                        var content = await _Client.GetStringAsync(url + "/" + user.Username + "/" + user.Password);
                        var handelaarListTemp = JsonConvert.DeserializeObject<List<Handelaar>>(content);
                        var handelaar = handelaarListTemp[0];

                        User dbUser = App.UserDatabase.GetUser();
                        if (dbUser == null)
                            App.UserDatabase.SaveUser(user);

                        await DisplayAlert("Aanmelding", "Welkom, " + handelaar.Naam + "!" + " Uw BTW nummer is " + handelaar.BTW_Nummer, "Oke");

                        Scanner();
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        await DisplayAlert("Aanmelding", "Uw gebruikersnaam en/of wachtwoord is onjuist. Gelieve het opnieuw te proberen.", "Oke");
                    }
                    catch (Exception)
                    {
                        await DisplayAlert("Aanmelding", "Er kan op dit moment geen verbinding worden gemaakt met het internet. Gelieve het later opnieuw te proberen.", "Oke");
                    }
                }
                else
                {
                    await DisplayAlert("Aanmelding", "Uw gebruikersnaam en/of wachtwoord ontbreekt. Gelieve het opnieuw te proberen.", "Oke");
                }
            }
            catch (NullReferenceException)
            {
                await DisplayAlert("Aanmelding", "Uw gebruikersnaam en/of wachtwoord ontbreekt. Gelieve het opnieuw te proberen.", "Oke");
            }
        }
    }
}