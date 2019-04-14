using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LiveDex {
    public partial class SettingsPage : ContentPage {
        public SettingsPage() {
            InitializeComponent();
        }

        async void SetPokemonToMissing(object sender, System.EventArgs e) {
            await App.CaughtDatabaseInstance.ClearDatabase();
        }
    }
}
