using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GpxToolExt {
   public partial class App : Application {
      public App() {
         InitializeComponent();
      }

      public App(object androidactivity) : this() {
         //MainPage = new MainPage();
         //MainPage = new MainPage(androidactivity);

         MainPage = new NavigationPage(new MainPage(androidactivity)) {
            BarBackgroundColor = Color.FromRgb(0, 0.5, 0),
            BarTextColor = Color.White,
         };
      }



      protected override void OnStart() {
      }

      protected override void OnSleep() {
      }

      protected override void OnResume() {
      }
   }
}
