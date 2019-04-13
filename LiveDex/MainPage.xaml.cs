using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveDex.Models;
using Xamarin.Forms;

namespace LiveDex {
    public partial class MainPage : ContentPage {


        public MainPage() {
            InitializeComponent();
        }

        async void LoadPokedex(object sender, System.EventArgs e) {
            await Navigation.PushAsync(new PokedexPage());
        }
    }
}
