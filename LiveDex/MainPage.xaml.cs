using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LiveDex {
    public partial class MainPage : ContentPage {
        public MainPage() {
            InitializeComponent();
        }

        void LoadLiveDex(object sender, System.EventArgs e) {
            Navigation.PushAsync(new LiveDex());
        }
    }
}
