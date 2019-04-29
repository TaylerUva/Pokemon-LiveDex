using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace LiveDex.Models{
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

    public class MultiTriggerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (!PokeData.AllCaught()) // length > 0 ?
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


    public class PokedexBehavior : TriggerAction<Button>{
        protected override void Invoke(Button sender){
            //throw new NotImplementedException();
            if (!PokeData.AllCaught()){
                sender.Text = "YOU GOT ALL THE POKEMON";
                sender.IsEnabled = false;
            }
        }
    }

    public class ReleaseAllTrigger : TriggerAction<Button>{
        protected override void Invoke(Button sender){
            //throw new NotImplementedException();
            if (!PokeData.AllMissing()){
                sender.Text = "ALL THE POKEMON ARE GONE";
                sender.IsEnabled = false;
            }

        }
    }
}
