using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LiveDex {
    public partial class SettingsPage : ContentPage {
        public SettingsPage() {
            InitializeComponent();
        }

        const string youAreAbout = "You are about to change the caught status of ALL pokemon to:\n";
        const string noUndo = "\nThis CANNOT be undone!";
        const string statusSet = "The caught status of ALL pokemon have changed to:\n";

        async void SetAllPokemonMissing(object sender, System.EventArgs e) {
            await SetAllCaughtStatus(false);
        }

        async void SetAllPokemonCaught(object sender, System.EventArgs e) {
            await SetAllCaughtStatus(true);
        }

        async Task SetAllCaughtStatus(bool caught) {
            string caughtString = caught.ToString().ToUpper();
            var changeSelected = await DisplayAlert("Are you sure?",
                  youAreAbout + caughtString + noUndo,
                  "Change",
                  "Cancel");
            if (changeSelected) {
                loading.IsRunning = true;
                await App.CaughtDatabaseInstance.PopulateDatabase(false, true);
                loading.IsRunning = false;
                await DisplayAlert("Changed!", statusSet + caughtString, "Okay");
            }
        }
    }
}
