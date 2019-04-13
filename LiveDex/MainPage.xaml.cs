using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveDex.Models;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace LiveDex {
    public partial class MainPage : ContentPage {


        public MainPage() {
            InitializeComponent();
        }


        async void LoadPokedexPage(object sender, System.EventArgs e) {
            if (await HasInternet()) {
                await Navigation.PushAsync(new PokedexPage());
            }
        }

        async void LoadCaughtPage(object sender, System.EventArgs e) {
            if (await HasInternet()) {
                await Navigation.PushAsync(new CaughtPage());
            }
        }
        async void LoadMissingPage(object sender, System.EventArgs e) {
            if (await HasInternet()) {
                await Navigation.PushAsync(new MissingPage());
            }
        }

        private async Task<bool> HasInternet() {
            if (!CrossConnectivity.Current.IsConnected) {
                await DisplayAlert("No Internet Connection", "Our data is pulled from the web, please connect to the internet", "Close");
                return false;
            }
            return true;
        }

        async void Handle_Appearing(object sender, System.EventArgs e) {

            //todo
            //var checkB = CaughtDatabase.populated;
            //var countB = CaughtDatabase.pullCount;

            await App.CaughtDatabaseInstance.PopulateDatabase();

            // todo
            //var list = await App.CaughtDatabaseInstance.GetAllPokemonCaught();
            //var checkA = CaughtDatabase.populated;
            //var countA = CaughtDatabase.pullCount;
        }
    }
}
