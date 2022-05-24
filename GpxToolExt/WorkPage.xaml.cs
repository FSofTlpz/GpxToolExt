using FSofTUtils.Xamarin;
using FSofTUtils.Xamarin.DependencyTools;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GpxToolExt {
   /// <summary>
   /// dient nur dem Starten des eigentlichen Win-Programms und der Ausgabe der Ergebnisse
   /// </summary>
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class WorkPage : ContentPage {

      readonly string[] args;

      /// <summary>
      /// Noch bei der Arbeit?
      /// </summary>
      bool isBusy = false;

      public StorageHelper sh;


      public WorkPage() {
         InitializeComponent();
      }

      public WorkPage(string[] args) : this() {
         this.args = args;
      }

      async protected override void OnAppearing() {
         base.OnAppearing();
         if (!DesignMode.IsDesignModeEnabled) {
            output.Text = "Eingabe:" + Environment.NewLine;
            foreach (string item in args)
               output.Text += "   " + item + Environment.NewLine;
            output.Text += "Ausgabe:" + Environment.NewLine + Environment.NewLine;
            SetBusyStatus(true);
            CatchStdoutStderr();

            try {
               GpxTool.Program.sh = sh;
               GpxTool.Program.Main(args);
               await Helper.MessageBox(this, "Bearbeitung", "Beendet.");
            } catch (Exception ex) {
               Console.Error.WriteLine("Exception: " + ex.Message);
            }

            CatchStdoutStderr(false);
            SetBusyStatus(false);
         }
      }

      void SetBusyStatus(bool isbusy) {
         isBusy = isbusy;
         NavigationPage.SetHasBackButton(this, !isBusy);    // den Soft-Backbutton in der Titelleiste entfernen oder sichtbar machen
      }

      /// <summary>
      /// Event that is raised when the hardware back button is pressed. This event is not raised on iOS.
      /// </summary>
      /// <returns></returns>
      protected override bool OnBackButtonPressed() {
         if (isBusy)
            return true; // Disable Backbutton

         return base.OnBackButtonPressed(); // Standard
      }

      /// <summary>
      /// umlenken von Stdout/Stderr
      /// </summary>
      /// <param name="catchoutput"></param>
      void CatchStdoutStderr(bool catchoutput = true) {
         Console.Out.Flush();
         Console.Error.Flush();

         if (catchoutput) {
            MyMemoryStream ms = new MyMemoryStream(10240);
            ms.NewTextEvent += ms_NewTextEvent;
            StreamWriter output = new StreamWriter(ms) {
               AutoFlush = true
            };

            Console.SetOut(output);
            Console.SetError(output);
         } else {
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetError(new StreamWriter(Console.OpenStandardError()));
         }
      }

      /// <summary>
      /// neuer Text wurde auf Stdout/Stderr geschrieben
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void ms_NewTextEvent(object sender, MyMemoryStream.NewTextEventArgs e) {
         output.Text += e.NewText;
         Helper.SrollToEnd(output);
      }
   }
}