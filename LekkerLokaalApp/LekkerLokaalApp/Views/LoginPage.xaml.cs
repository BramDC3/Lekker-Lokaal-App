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
using System.Security.Cryptography;

namespace LekkerLokaalApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private const string url = "https://www.bramdeconinck.com/apps/lekkerlokaal/v1/handelaars.php?";
        private HttpClient _Client = new HttpClient(new NativeMessageHandler());

        public LoginPage()
        {
            InitializeComponent();
            Init();
            VulVeldenMetGegevensUitDB();
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

        private void VulVeldenMetGegevensUitDB()
        {
            User dbUser = App.UserDatabase.GetUser();
            if (dbUser != null)
            {
                Entry_Username.Text = dbUser.Username;
                Entry_Password.Text = dbUser.Password;
            }
        }

        public async void Scanner()
        {
            var ScannerPage = new ZXingScannerPage();

            await Navigation.PushAsync(ScannerPage);

            ScannerPage.OnScanResult += (result) =>
            {
                ScannerPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopAsync();
                    Navigation.PushAsync(new VerificatiePage(App.HandelaarDatabase.GetHandelaar(), result.Text));
                });
            };
        }

        private async void SignInProcedure(object sender, EventArgs e)
        {
            User user = App.UserDatabase.GetUser();
            if (user == null)
                user = new User(Entry_Username.Text, Entry_Password.Text);

            try
            {
                if (user.CheckInformation())
                {
                    try
                    {
                        string paswoord;
                        if (App.UserDatabase.GetUser() == null)
                            paswoord = sha256(user.Password);
                        else
                            paswoord = user.Password;
                        var content = await _Client.GetStringAsync(url + "id=" + user.Username + "&ww=" + paswoord);
                        var handelaarListTemp = JsonConvert.DeserializeObject<List<Handelaar>>(content);
                        var handelaar = handelaarListTemp[0];

                        Handelaar dbHandelaar = App.HandelaarDatabase.GetHandelaar();
                        if (dbHandelaar == null)
                        {
                            App.HandelaarDatabase.SaveHandelaar(handelaar);
                        }   
                        else
                        {
                            App.HandelaarDatabase.DeleteHandelaar(dbHandelaar.Id);
                            App.HandelaarDatabase.SaveHandelaar(handelaar);
                        }

                        User dbUser = App.UserDatabase.GetUser();
                        if (dbUser == null)
                        {
                            user.Password = paswoord;
                            App.UserDatabase.SaveUser(user);
                        }

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

        private static string sha256(string randomString)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}