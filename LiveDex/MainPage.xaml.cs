using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveDex.Models;
using Xamarin.Forms;

namespace LiveDex {
    public partial class MainPage : ContentPage {

        Pokedex pokedexData;


        public MainPage() {
            InitializeComponent();
        }

        async void LoadLiveDex(object sender, System.EventArgs e) {
            pokedexData = await GetPokedex.GetEntryAsync("12");
            System.Diagnostics.Debug.WriteLine("done");
        }
    }
}
