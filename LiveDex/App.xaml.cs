using System;
using System.IO;
using LiveDex.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LiveDex {
    public partial class App : Application {

        private static CaughtDatabase caughtDatabase;
        public static CaughtDatabase CaughtDatabaseInstance {
            get {
                if (caughtDatabase == null) {
                    caughtDatabase = new CaughtDatabase(
                      Path.Combine(
                          Environment.GetFolderPath(
                              Environment.SpecialFolder.LocalApplicationData),
                              "CaughtDatabase.db3"));
                }
                return caughtDatabase;
            }
        }

        public App() {
            InitializeComponent();
            CaughtDatabaseInstance.PopulateDatabase();

            MainPage = new NavigationPage(new MainPage()) {
                BarBackgroundColor = Color.FromHex("990D11"),
                BarTextColor = Color.White
            };
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
