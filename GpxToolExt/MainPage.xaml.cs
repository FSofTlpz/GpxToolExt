using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using FSofTUtils.Xamarin;
using FSofTUtils.Xamarin.DependencyTools;
using FSofTUtils.Xamarin.Page;
using Xamarin.Forms;

namespace GpxToolExt {
   public partial class MainPage : ContentPage {

      const string TITLE = "GpxToolExt, © by FSofT 17.8.2022";

      readonly object androidactivity;

      StorageHelper sh;



      /// <summary>
      /// Zieldatei
      /// </summary>
      public string DestFilename {
         get => labelDestFile.Text;
         set => labelDestFile.Text = value;
      }




      public MainPage() {
         InitializeComponent();
      }

      public MainPage(object androidactivity) : this() {
         this.androidactivity = androidactivity;
      }


      protected override void OnAppearing() {
         base.OnAppearing();

         Title = TITLE + " (v" + Xamarin.Essentials.AppInfo.VersionString + ")";

         if (!DesignMode.IsDesignModeEnabled) {
            readAppData();
            if (sh == null) {
               sh = DepToolsWrapper.GetStorageHelper(androidactivity);
               for (int v = 0; v < sh.Volumes; v++)
                  if (!sh.SetAndroidPersistentPermissions(v) && v > 0)
                     sh.Ask4AndroidPersistentPermissonAndWait(v);
            }

            inputFileListCtrl.Clear();
            GetAppProps4SrcFile();
            GetAppProps4DestFile();
         } else {
            //inputFileList.Add("abc", "123");
            //inputFileList.Add("ABC", "567");
         }
      }


      async private void ButtonStart_Tapped(object sender, EventArgs e) {
         saveAppData();

         //string[] args = new string[] {
         //   "--info=true",
         //   "--output=out.gpx",
         //   "/test1.gpx"
         //};

         WorkPage workPage;
         try {
            workPage = new WorkPage(getOptions()) {
               sh = sh,
            };
         } catch (Exception ex) {
            workPage = null;
            await Helper.MessageBox(this, "Fehler", ex.Message);
         }
         if (workPage != null)
            await Navigation.PushAsync(workPage);

      }

      private void TestButton_Clicked(object sender, EventArgs e) {

      }


      /// <summary>
      /// speichert die Page-Daten als App-Daten
      /// </summary>
      void saveAppData() {
         AppData.ShowInfo = cb_ShowInfo.IsChecked;
         AppData.InputWithSubdirs = cb_InputWithSubdirs.IsChecked;
         AppData.OutputOverwrite = cb_OutputOverwrite.IsChecked;
         AppData.FormatedOutput = cb_FormatedOutput.IsChecked;
         AppData.SimplifyGPX = cb_SimplifyGPX.IsChecked;
         AppData.OneFilePerTrack = cb_OneFilePerTrack.IsChecked;
         AppData.Segment2Track = cb_Segment2Track.IsChecked;
         AppData.Filename2TrackName = cb_Filename2TrackName.IsChecked;

         switch (picker_HorizontalSimplification.SelectedIndex) {
            default:
               AppData.HorizontalSimplification = "";
               break;
            case 1:
               AppData.HorizontalSimplification = "RW";
               break;
            case 2:
               AppData.HorizontalSimplification = "DP";
               break;
         }

         AppData.HorizontalWidth = AppData.TextAsDouble(entry_HorizontalWidth.Text);
         AppData.HorizontalMaxSpeed = AppData.TextAsDouble(entry_HorizontalMaxSpeed.Text);

         switch (picker_VerticalSimplification.SelectedIndex) {
            default:
               AppData.VerticalSimplification = "";
               break;
            case 1:
               AppData.VerticalSimplification = "SI";
               break;
            case 2:
               AppData.VerticalSimplification = "SM";
               break;
         }

         AppData.VerticalWidth = AppData.TextAsDouble(entry_VerticalWidth.Text);
         AppData.VerticalOutlierWidth = AppData.TextAsDouble(entry_VerticalOutlierWidth.Text);
         AppData.MaxAscent = AppData.TextAsDouble(entry_MaxAscent.Text);

         AppData.DeleteTimestamp = cb_DeleteTimestamp.IsChecked;
         AppData.DeleteHeight = cb_DeleteHeight.IsChecked;
         AppData.GapFill = cb_GapFill.IsChecked;
         AppData.ConstantHeight = AppData.TextAsDouble(entry_ConstantHeight.Text);
         AppData.MinHeight = AppData.TextAsDouble(entry_MinHeight.Text);
         AppData.MaxHeight = AppData.TextAsDouble(entry_MaxHeight.Text);

         AppData.KmlColor = kmlColorChooserCtrl.ActualColor;
         AppData.KmlWidth = AppData.TextAsInt(entry_KmlTrackDataWidth.Text);
      }

      /// <summary>
      /// setzt die Page-Daten entsprechend der gespeicherten App-Daten
      /// </summary>
      void readAppData() {
         cb_ShowInfo.IsChecked = AppData.ShowInfo;
         cb_InputWithSubdirs.IsChecked = AppData.InputWithSubdirs;
         cb_OutputOverwrite.IsChecked = AppData.OutputOverwrite;
         cb_FormatedOutput.IsChecked = AppData.FormatedOutput;
         cb_SimplifyGPX.IsChecked = AppData.SimplifyGPX;
         cb_OneFilePerTrack.IsChecked = AppData.OneFilePerTrack;
         cb_Segment2Track.IsChecked = AppData.Segment2Track;
         cb_Filename2TrackName.IsChecked = AppData.Filename2TrackName;

         if (AppData.HorizontalSimplification == "RW")
            picker_HorizontalSimplification.SelectedIndex = 1;
         else if (AppData.HorizontalSimplification == "DP")
            picker_HorizontalSimplification.SelectedIndex = 2;
         else
            picker_HorizontalSimplification.SelectedIndex = 0;

         entry_HorizontalWidth.Text = AppData.DoubleAsText(AppData.HorizontalWidth);
         entry_HorizontalMaxSpeed.Text = AppData.DoubleAsText(AppData.HorizontalMaxSpeed);

         if (AppData.VerticalSimplification == "SI")
            picker_VerticalSimplification.SelectedIndex = 1;
         else if (AppData.VerticalSimplification == "SM")
            picker_VerticalSimplification.SelectedIndex = 2;
         else
            picker_VerticalSimplification.SelectedIndex = 0;

         entry_VerticalWidth.Text = AppData.DoubleAsText(AppData.VerticalWidth);
         entry_VerticalOutlierWidth.Text = AppData.DoubleAsText(AppData.VerticalOutlierWidth);
         entry_MaxAscent.Text = AppData.DoubleAsText(AppData.MaxAscent);

         cb_DeleteTimestamp.IsChecked = AppData.DeleteTimestamp;
         cb_DeleteHeight.IsChecked = AppData.DeleteHeight;
         cb_GapFill.IsChecked = AppData.GapFill;
         entry_ConstantHeight.Text = AppData.DoubleAsText(AppData.ConstantHeight);
         entry_MinHeight.Text = AppData.DoubleAsText(AppData.MinHeight);
         entry_MaxHeight.Text = AppData.DoubleAsText(AppData.MaxHeight);

         kmlColorChooserCtrl.ActualColorR = AppData.KmlColor.R;
         kmlColorChooserCtrl.ActualColorG = AppData.KmlColor.G;
         kmlColorChooserCtrl.ActualColorB = AppData.KmlColor.B;
         kmlColorChooserCtrl.ActualColorA = AppData.KmlColor.A;

         entry_KmlTrackDataWidth.Text = AppData.IntAsText(AppData.KmlWidth);
      }

      #region Aufbau der Optionsliste

      /// <summary>
      /// ermittelt die Optionsliste (größtenteils) aus den Page-Daten
      /// </summary>
      /// <returns></returns>
      string[] getOptions() {
         List<string> options = new List<string>();

         addOption4Checkbox(cb_ShowInfo, options, "info");

         if (!string.IsNullOrEmpty(AppData.Outputfile))     // ausnahmsweise aus gespeicherten Werten geholt
            options.Add("--output=" + AppData.Outputfile);

         addOption4Checkbox(cb_InputWithSubdirs, options, "withsubdirs");
         addOption4Checkbox(cb_OutputOverwrite, options, "overwrite");
         addOption4Checkbox(cb_FormatedOutput, options, "formated");
         addOption4Checkbox(cb_SimplifyGPX, options, "simplifygpx");
         addOption4Checkbox(cb_OneFilePerTrack, options, "onefilepertrack");
         addOption4Checkbox(cb_Segment2Track, options, "segment2track");
         addOption4Checkbox(cb_Filename2TrackName, options, "filenametotrackname");

         switch (picker_HorizontalSimplification.SelectedIndex) {
            case 1:
               options.Add("--simplify=RW");
               break;
            case 2:
               options.Add("--simplify=DP");
               break;
            default:
               break;
         }

         addOption4EntryAsDouble(entry_HorizontalWidth, options, "width");
         addOption4EntryAsDouble(entry_HorizontalMaxSpeed, options, "maxspeed");

         switch (picker_VerticalSimplification.SelectedIndex) {
            case 1:
               options.Add("--heightsimplify=SI");
               break;
            case 2:
               options.Add("--heightsimplify=SM");
               break;
            default:
               break;
         }

         addOption4EntryAsDouble(entry_VerticalWidth, options, "heightwidth");
         addOption4EntryAsDouble(entry_VerticalOutlierWidth, options, "heightoutlierwidth");
         addOption4EntryAsDouble(entry_MaxAscent, options, "maxascent");

         addOption4Checkbox(cb_DeleteTimestamp, options, "deletetime");
         addOption4Checkbox(cb_DeleteHeight, options, "deleteheight");
         addOption4Checkbox(cb_GapFill, options, "gapfill");

         addOption4EntryAsDouble(entry_ConstantHeight, options, "newheigth");
         addOption4EntryAsDouble(entry_MinHeight, options, "minheight");
         addOption4EntryAsDouble(entry_MaxHeight, options, "maxheight");

         int v = getEntryAsPosInt(entry_KmlTrackDataWidth);
         if (v > 0)
            options.Add("--kmltrackdata=" + kmlColorChooserCtrl.ActualColor.ToHex() + "," + v.ToString());

         // Eingabedateien
         foreach (var item in AppData.Inputfiles)     // ausnahmsweise aus gespeicherten Werten geholt
            options.Add(item);

         return options.ToArray();
      }

      void addOption4EntryAsDouble(Entry entry, List<string> options, string optname) {
         double v = getEntryAsPosDouble(entry);
         if (v >= 0)
            options.Add("--" + optname + "=" + v.ToString(CultureInfo.InvariantCulture));
      }

      void addOption4Checkbox(CheckBox cb, List<string> options, string optname) {
         options.Add("--" + optname + "=" + cb.IsChecked.ToString());
      }

      /// <summary>
      /// Der Entry-Text wird als Integer größer oder gleich als 0 geliefert oder -1
      /// </summary>
      /// <param name="entry"></param>
      /// <returns></returns>
      int getEntryAsPosInt(Entry entry) {
         if (!string.IsNullOrEmpty(entry.Text)) {
            try {
               int v = Convert.ToInt32(entry.Text);
               if (v >= 0)
                  return v;
            } catch { }
         }
         return -1;
      }

      /// <summary>
      /// Der Entry-Text wird als Double größer oder gleich als 0 geliefert oder -1
      /// </summary>
      /// <param name="entry"></param>
      /// <returns></returns>
      double getEntryAsPosDouble(Entry entry) {
         if (!string.IsNullOrEmpty(entry.Text)) {
            try {
               double v = Convert.ToDouble(entry.Text);
               if (v > 0)
                  return v;
            } catch { }
         }
         return -1.0;
      }

      #endregion

      private void kmlColorChooserCtrlColorChanged(object sender, EventArgs e) {

      }

      /// <summary>
      /// eine Eingabedatei soll hinzugefügt werden
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void buttonInputfile_Tapped(object sender, EventArgs e) {
         choosefileTapped(FileType.SrcFile);
      }

      /// <summary>
      /// eine Ausgabedatei soll festgelegt werden
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void destFileButtonTapped(object sender, EventArgs e) {
         choosefileTapped(FileType.Destfile);
      }

      /// <summary>
      /// ev. die Zieldatei löschen
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private async void destFileTapped(object sender, EventArgs e) {
         if (DestFilename != "") {
            bool remove = await Helper.MessageBox(this, "Achtung", "Soll die Datei '" + DestFilename + "' entfernt werden?", "ja", "nein");
            if (remove) {
               DestFilename = "";
               SetAppProps4DestFile();
            }
         }
      }

      #region Auswahl einer Datei über eine eigene Page

      enum FileType {
         SrcFile,
         Destfile
      }

      FileType ChooseFile = FileType.SrcFile;

      string LastSrcFile = "";

      /// <summary>
      /// eine Datei für Input oder Output soll ausgewählt werden
      /// </summary>
      /// <param name="type"></param>
      void choosefileTapped(FileType type) {
         string path = type == FileType.SrcFile ? LastSrcFile :
                               DestFilename.Trim().Length == 0 ? LastSrcFile : DestFilename;
         if (path != "")
            path = Path.GetDirectoryName(path);
         if (path == "")
            path = ChooseFilePage.LastChoosedPath;

         ChooseFilePage page = new ChooseFilePage() {
            BackgroundColor = this.BackgroundColor,
            AndroidActivity = androidactivity,
            Path = path,
            Filename = "",
            OnlyExistingFile = type != FileType.Destfile,
            Title = type == FileType.SrcFile ? "GPX-Datei auswählen" :
                                                                          "Zieldatei auswählen",
         };
         page.ChooseFileReadyEvent += page_ChooseFileReadyEvent;
         ChooseFile = type; // für die Auswertung im Event-Handler
         Navigation.PushAsync(page);
      }

      /// <summary>
      /// eine Datei wurde ausgewählt
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      async private void page_ChooseFileReadyEvent(object sender, FSofTUtils.Xamarin.Control.ChooseFile.ChoosePathAndFileEventArgs e) {
         if (e.OK) {
            string filename = Path.Combine(e.Path, e.Filename);
            if (filename != "") {
               switch (ChooseFile) {
                  case FileType.SrcFile:
                     if (Path.GetExtension(filename).ToLower() != ".gpx") {
                        await Helper.MessageBox(this, "Fehler", "Es sind nur GPX-Dateien erlaubt.");
                        return;
                     }
                     long len = sh.GetFileAttributes(filename, false, out bool canread, out bool canwrite, out DateTime lastmod);
                     LastSrcFile = filename;
                     bool contains = false;
                     for (int i = 0; i < inputFileListCtrl.Count; i++)
                        if (inputFileListCtrl.Get(i).Data1 == filename) {
                           contains = true;
                           break;
                        }

                     if (contains)
                        await Helper.MessageBox(this, "Achtung", "Die Datei '" + filename + "' ist schon enthalten.");
                     else {
                        inputFileListCtrl.Add(new ItemList.Item(filename,
                                                                string.Format("{0} Byte, {1} kByte, {2} MByte / {3}",
                                                                      len,
                                                                      Math.Round(len / 1024.0, 1),
                                                                      Math.Round(len / (1024.0 * 1024.0), 1),
                                                                      lastmod.ToString("G"))));
                        Helper.SrollToEnd(inputFileListCtrl);
                        SetAppProps4SrcFile();
                     }
                     break;

                  case FileType.Destfile:
                     if (Path.GetExtension(filename).ToLower() != ".gpx" &&
                         Path.GetExtension(filename).ToLower() != ".kml" &&
                         Path.GetExtension(filename).ToLower() != ".kmz")
                        filename += ".gpx";
                     DestFilename = filename;
                     SetAppProps4DestFile();
                     break;
               }
            }

            ChooseFilePage.LastChoosedPath = e.Path;
         }
      }

      #endregion

      /// <summary>
      /// ev. eine Datei aus der Liste löschen
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private async void inputFileListCtrlElementTapped(object sender, ItemList.ElementTappedEventArgs e) {
         bool remove = await Helper.MessageBox(this, "Achtung", "Soll die Datei '" + e.Item.Data1 + "' entfernt werden?", "ja", "nein");
         if (remove) {
            ItemList il = sender as ItemList;
            il.RemoveAt(e.Position);
            SetAppProps4SrcFile();
         }
      }

      void SetAppProps4DestFile() {
         Helper.SrollToEnd(labelDestFile);
         AppData.Outputfile = DestFilename;
      }

      void GetAppProps4DestFile() {
         DestFilename = AppData.Outputfile;
         Helper.SrollToEnd(labelDestFile);
      }

      /// <summary>
      /// Listeninhalt speichern
      /// </summary>
      void SetAppProps4SrcFile() {
         List<string> tmp = new List<string>();
         for (int i = 0; i < inputFileListCtrl.Count; i++) {
            tmp.Add(inputFileListCtrl.Get(i).Data1);
            tmp.Add(inputFileListCtrl.Get(i).Data2);
         }
         AppData.InputfilesData = tmp.ToArray();
      }

      /// <summary>
      /// Listeninhalt aus den gespeicherten Werten erzeugen
      /// </summary>
      void GetAppProps4SrcFile() {
         string[] tmp = AppData.InputfilesData;
         if (tmp.Length > 0) {
            inputFileListCtrl.Clear();
            for (int i = 1; i < tmp.Length; i += 2) {
               inputFileListCtrl.Add(new ItemList.Item(tmp[i - 1], tmp[i]));
               LastSrcFile = tmp[i - 1];
            }
         }
      }

      private void buttonOutputfile_Tapped(object sender, EventArgs e) {

      }
   }
}
