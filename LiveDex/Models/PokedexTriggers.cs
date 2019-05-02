using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using System.Reflection;
using System.Windows.Input;

namespace LiveDex.Models {
    /*
    public class SettingsAppear : Trigger
    {
        protected override void Invoke(ContentPage sender)
        {
            throw new NotImplementedException();
        }
    }
    */
    /*
    public class MultiTriggerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if ((int)value > 0) // length > 0 ?
                return true;            // some data has been entered
            else
                return false;            // input is empty
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
    */

    //public class MultiTriggerConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType,
    //        object parameter, CultureInfo culture)
    //    {
    //        if (!PokeData.AllCaught()) // length > 0 ?
    //            return true;            // some data has been entered
    //        else
    //            return false;            // input is empty
    //    }

    //    public object ConvertBack(object value, Type targetType,
    //        object parameter, CultureInfo culture)
    //    {
    //        throw new NotSupportedException();
    //    }
    //}
    public class PickerBehavior : Behavior<Picker> {
        static readonly BindableProperty blueGenProperties = BindableProperty.Create(nameof(blueGens), typeof(string[]), typeof(PickerBehavior));

        public string[] blueGens = { "blue", "diamond", "sapphire", "x" };
        public string[] redGens = { "firered", "ruby", "red", "y" };
        public string[] greenGens = { "leafgreen", "emerald" };
        public string[] silverGens = { "platinum", "soulsilver" };
        public string[] blackGens = { "black", "black 2" };
        public string[] whiteGens = { "white", "white 2" };
        //public string[] blueGens
        //{
        //    get => (string[])GetValue(blueGenProperties);
        //    set => SetValue(blueGenProperties, value);
        //}


        protected override void OnAttachedTo(Picker bindable) {
            //base.OnAttachedTo(bindable);
            bindable.SelectedIndexChanged += Bindable_SelectedIndexChanged;
        }
        protected override void OnDetachingFrom(Picker bindable) {
            //base.OnDetachingFrom(bindable);
            bindable.SelectedIndexChanged -= Bindable_SelectedIndexChanged;
        }

        void Bindable_SelectedIndexChanged(object sender, EventArgs e) {
            if (blueGens.Contains(((Picker)sender).SelectedItem.ToString().ToLower())) {
                ((Picker)sender).BackgroundColor = Color.FromHex("2B206F");
                ((Picker)sender).TextColor = Color.White;
            } else if (redGens.Contains(((Picker)sender).SelectedItem.ToString().ToLower())) {
                ((Picker)sender).BackgroundColor = Color.Red;
                ((Picker)sender).TextColor = Color.White;
            } else if (greenGens.Contains(((Picker)sender).SelectedItem.ToString().ToLower())) {
                ((Picker)sender).BackgroundColor = Color.Green;
                ((Picker)sender).TextColor = Color.White;
            } else if (silverGens.Contains(((Picker)sender).SelectedItem.ToString().ToLower())) {
                ((Picker)sender).BackgroundColor = Color.Silver;
                ((Picker)sender).TextColor = Color.White;
            } else if (((Picker)sender).SelectedItem.ToString().ToLower() == "heartgold") {
                ((Picker)sender).BackgroundColor = Color.Goldenrod;
                ((Picker)sender).TextColor = Color.White;
            } else if (((Picker)sender).SelectedItem.ToString().ToLower() == "yellow") {
                ((Picker)sender).BackgroundColor = Color.Gold;
                ((Picker)sender).TextColor = Color.White;
            } else if (((Picker)sender).SelectedItem.ToString().ToLower() == "pearl") {
                ((Picker)sender).BackgroundColor = Color.DeepPink;
                ((Picker)sender).TextColor = Color.White;
            } else if (blackGens.Contains(((Picker)sender).SelectedItem.ToString().ToLower())) {
                ((Picker)sender).BackgroundColor = Color.Black;
                ((Picker)sender).TextColor = Color.White;
            }
              //else if (whiteGens.Contains(((Picker)sender).SelectedItem.ToString().ToLower()))
              //{
              //    ((Picker)sender).BackgroundColor = Color.Default;
              //    ((Picker)sender).TextColor = Color.White;
              //}
              else {
                ((Picker)sender).BackgroundColor = Color.Default;
                ((Picker)sender).TextColor = Color.Default;

            }

        }
    }



    public class CatchAllTrigger : TriggerAction<Button> {
        protected override void Invoke(Button sender) {
            //throw new NotImplementedException();
            if (!PokeData.AllCaught()) {
                sender.Text = "You got 'em all!";
                sender.IsEnabled = false;
            }
        }
    }

    public class ReleaseAllTrigger : TriggerAction<Button> {
        protected override void Invoke(Button sender) {
            //throw new NotImplementedException();
            if (!PokeData.AllMissing()) {
                sender.Text = "Better catch 'em all!";
                sender.IsEnabled = false;
            }

        }
    }
}
