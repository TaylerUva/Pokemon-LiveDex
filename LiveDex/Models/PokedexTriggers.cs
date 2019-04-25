using System;
using System.Linq;
using Xamarin.Forms;

namespace LiveDex.Models{

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
