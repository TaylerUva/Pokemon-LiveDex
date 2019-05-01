using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using LiveDex.Models;

namespace LiveDex {
    public partial class SettingsPage : ContentPage {
        public SettingsPage() {

            InitializeComponent();

        }

        bool checkAllCaught;
        bool checkAllMissing;
        const string youAreAbout = "You are about to change the caught status of ALL pokemon to:\n";
        const string noUndo = "\nThis CANNOT be undone!";
        const string statusSet = "The caught status of ALL pokemon have changed to:\n";

        async void SetAllPokemonMissing(object sender, System.EventArgs e) {
            await SetAllCaughtStatus(false);
            //releaseButton.IsEnabled = false;
            catchButton.IsEnabled = true;
            catchButton.Text = "Catch 'em All";
        }

        async void SetAllPokemonCaught(object sender, System.EventArgs e) {
            await SetAllCaughtStatus(true);
            //catchButton.IsEnabled = false;
            releaseButton.IsEnabled = true;
            releaseButton.Text = "Release 'em All";
        }

        async Task SetAllCaughtStatus(bool caught) {
            string caughtString = caught.ToString().ToUpper();

            var changeSelected = await DisplayAlert("Are you sure?",
                  youAreAbout + caughtString + noUndo,
                  "Change",
                  "Cancel");
            if (changeSelected) {
                loading.IsRunning = true;
                await App.CaughtDatabaseInstance.PopulateDatabase(caught, true);
                loading.IsRunning = false;
                await DisplayAlert("Changed!", statusSet + caughtString, "Okay");
                checkAllCaught = PokeData.AllCaught();
                checkAllMissing = PokeData.AllMissing();
            }
        }

        void Handle_Appearing(object sender, System.EventArgs e)
        {
            if (PokeData.AllCaught()) { 
                catchButton.IsEnabled = false; 
                releaseButton.Text = "Release 'em All"; 
                catchButton.Text = "You got 'em all!";
            }
            else if (PokeData.AllMissing()) { 
                releaseButton.IsEnabled = false; 
                catchButton.Text = "Catch 'em All"; 
                releaseButton.Text = "Better catch 'em all!";
            }
            else { 
                catchButton.IsEnabled = true; 
                releaseButton.IsEnabled = true; 
                releaseButton.Text = "Release 'em All"; 
                catchButton.Text = "Catch 'em All"; 
            }
        }
    }
}
