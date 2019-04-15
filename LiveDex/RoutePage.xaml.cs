using System;
using System.Collections.Generic;
using LiveDex.Models;
using Xamarin.Forms;

namespace LiveDex {
    public partial class RoutePage : ContentPage {

        public RoutePage(Route route) {
            InitializeComponent();
            Title = route.Name;
            DetailsList.ItemsSource = route.Details.EncounterDetails;
        }
    }
}
