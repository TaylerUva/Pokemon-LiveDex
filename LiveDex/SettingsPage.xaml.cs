using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LiveDex {
    public partial class SettingsPage : ContentPage {
        public SettingsPage() {
            InitializeComponent();
        }

        async void SetAllPokemonMissing(object sender, System.EventArgs e) {
            loading.IsRunning = true;
            await App.CaughtDatabaseInstance.PopulateDatabase(false, true);
            loading.IsRunning = false;
        }

        async void SetAllPokemonCaught(object sender, System.EventArgs e) {
            loading.IsRunning = true;
            await App.CaughtDatabaseInstance.PopulateDatabase(true, true);
            loading.IsRunning = false;
        }
    }
}
