using System;
using System.IO;
using LiveDex.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

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

            MainPage = new NavigationPage(new MainPage()) {
                BarBackgroundColor = Color.FromHex("990D11"),
                BarTextColor = Color.White
            };
        }

        protected override void OnStart() {
            // Handle when your app starts
            AppCenter.Start("ios=22292377-6970-442f-8f33-810fb60d5fab;" +
                  "uwp={Your UWP App secret here};" +
                  "android=c56427a6-56a5-4665-9848-feda4c7b652a;",
                  typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
