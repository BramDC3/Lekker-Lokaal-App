using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LekkerLokaalApp.Models
{
    public static class Constants
    {
        public static bool IsDev = true;

        public static Color BackgroundColor = Color.FromRgb(58, 153, 215);
        public static Color MainTextColor = Color.White;

        public static Color VerificatieMislukt = Color.FromHex("#ff0000");
        public static Color VerificatieGeslaagd = Color.FromHex("#008000");

        public static int LoginIconHeight = 120;
        public static int VerificatieHeight = 150;
    }
}
