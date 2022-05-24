using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GpxToolExt {
   class AppData {

      public AppData() { }

      public static bool Value(string name, bool def) {
         if (Application.Current.Properties.ContainsKey(name))
            return Convert.ToBoolean(Application.Current.Properties[name]);
         return def;
      }

      public static string Value(string name, string def) {
         if (Application.Current.Properties.ContainsKey(name))
            return Application.Current.Properties[name].ToString();
         return def;
      }

      public static int Value(string name, int def) {
         if (Application.Current.Properties.ContainsKey(name))
            try {
               return Convert.ToInt32(Application.Current.Properties[name]);
            } catch { }
         return def;
      }

      public static double Value(string name, double def) {
         if (Application.Current.Properties.ContainsKey(name))
            try {
               return Convert.ToDouble(Application.Current.Properties[name]);
            } catch { }
         return def;
      }

      public static void SetValue(string name, object value) {
         Application.Current.Properties[name] = value;
      }

      static void RemoveData(string name) {
         if (Application.Current.Properties.ContainsKey(name))
            Application.Current.Properties.Remove(name);
      }


      public static int TextAsInt(string txt) {
         try {
            return Convert.ToInt32(txt);
         } catch {
            return -1;
         }
      }

      public static string IntAsText(int v) {
         return v >= 0 ? v.ToString() : "";
      }

      public static double TextAsDouble(string txt) {
         try {
            return Convert.ToDouble(txt);
         } catch {
            return -1;
         }
      }

      public static string DoubleAsText(double v) {
         return v >= 0 ? v.ToString() : "";
      }


      /// <summary>
      /// setzt alle Daten (außer Dateinamen) zurück
      /// </summary>
      public static void ResetAll() {
         RemoveData(OPT_INFO);
         RemoveData(OPT_INPWITHSUBDIRS);
         RemoveData(OPT_OUTPOVERWRITE);
         RemoveData(OPT_FORMATEDOUTPUT);
         RemoveData(OPT_SIMPLIFYGPX);
         RemoveData(OPT_NEWTRACKNAME);
         RemoveData(OPT_SEGMENT2TRACK);
         RemoveData(OPT_DELETETIMESTAMP);
         RemoveData(OPT_DELETEHEIGHT);
         RemoveData(OPT_GAPFILL);
         RemoveData(OPT_CONSTANTHEIGHT);
         RemoveData(OPT_HORIZONTALSIMPLIFICATION);
         RemoveData(OPT_HORIZONTALWIDTH);
         RemoveData(OPT_HORIZONTALMAXSPEED);
         RemoveData(OPT_MINHEIGHT);
         RemoveData(OPT_MAXHEIGHT);
         RemoveData(OPT_VERTICALSIMPLIFICATION);
         RemoveData(OPT_VERTICALWIDTH);
         RemoveData(OPT_VERTICALOUTLIERWIDTH);
         RemoveData(OPT_MAXASCENT);
         RemoveData(OPT_FILENAME2TRACKNAME);
         RemoveData(OPT_ONEFILEPERTRACK);
         RemoveData(OPT_KMLDATACOLOR);
         RemoveData(OPT_KMLDATAWIDTH);
      }


      const string OPT_INPFILE = "OptInputFiles";

      /// <summary>
      /// Eingabedateien und jeweils Zusatzdaten
      /// </summary>
      public static string[] InputfilesData {
         get {
            return Value(OPT_INPFILE, "").Split(new char[] { '|' });
         }
         set {
            SetValue(OPT_INPFILE, string.Join("|", value));
         }
      }
      /// <summary>
      /// liefert die Namen der Inputdateien
      /// </summary>
      public static string[] Inputfiles {
         get {
            string[] data = InputfilesData;
            string[] result = new string[data.Length / 2];
            for (int i = 0; i < data.Length; i += 2)
               result[i / 2] = data[i];
            return result;
         }
      }



      const string OPT_INFO = "OptInfo";

      /// <summary>
      /// Anzeige von GPX-Infos
      /// </summary>
      public static bool ShowInfo {
         get {
            return Value(OPT_INFO, true);
         }
         set {
            SetValue(OPT_INFO, value);
         }
      }


      const string OPT_DESTFILE = "OptDestFile";

      /// <summary>
      /// Ergebnisdatei
      /// </summary>
      public static string Outputfile {
         get {
            return Value(OPT_DESTFILE, "");
         }
         set {
            SetValue(OPT_DESTFILE, value);
         }
      }


      const string OPT_INPWITHSUBDIRS = "OptWithSubdirs";

      /// <summary>
      /// bei * oder ? Eingabedateien auch in Unterverzeichnissen suchen
      /// </summary>
      public static bool InputWithSubdirs {
         get {
            return Value(OPT_INPWITHSUBDIRS, false);
         }
         set {
            SetValue(OPT_INPWITHSUBDIRS, value);
         }
      }


      const string OPT_OUTPOVERWRITE = "OptOverwrite";

      /// <summary>
      /// ev. schon vorhandene Datei darf überschrieben werden
      /// </summary>
      public static bool OutputOverwrite {
         get {
            return Value(OPT_OUTPOVERWRITE, false);
         }
         set {
            SetValue(OPT_OUTPOVERWRITE, value);
         }
      }


      const string OPT_FORMATEDOUTPUT = "OptFormatedOutput";

      /// <summary>
      /// Ausgabe formatiert oder '1-zeilig'
      /// </summary>
      public static bool FormatedOutput {
         get {
            return Value(OPT_FORMATEDOUTPUT, false);
         }
         set {
            SetValue(OPT_FORMATEDOUTPUT, value);
         }
      }


      const string OPT_SIMPLIFYGPX = "OptSimplifyGPX";

      /// <summary>
      /// GPX-Datei vereinfachen
      /// </summary>
      public static bool SimplifyGPX {
         get {
            return Value(OPT_SIMPLIFYGPX, false);
         }
         set {
            SetValue(OPT_SIMPLIFYGPX, value);
         }
      }


      const string OPT_NEWTRACKNAME = "OptNewTrackName";

      /// <summary>
      /// neue Tracknamen
      /// </summary>
      public static string[] NewTrackName {
         get {
            return Value(OPT_NEWTRACKNAME, "").Split(new char[] { '|' });
         }
         set {
            SetValue(OPT_NEWTRACKNAME, string.Join("|", value));
         }
      }


      const string OPT_SEGMENT2TRACK = "OptSegment2Track";

      /// <summary>
      /// zusätzliche Segmente in eigene Tracks umwandeln
      /// </summary>
      public static bool Segment2Track {
         get {
            return Value(OPT_SEGMENT2TRACK, false);
         }
         set {
            SetValue(OPT_SEGMENT2TRACK, value);
         }
      }


      const string OPT_DELETETIMESTAMP = "OptDeleteTimestamp";

      /// <summary>
      /// alle Zeitstempel werden aus den Trackpunkten entfernt
      /// </summary>
      public static bool DeleteTimestamp {
         get {
            return Value(OPT_DELETETIMESTAMP, false);
         }
         set {
            SetValue(OPT_DELETETIMESTAMP, value);
         }
      }


      const string OPT_DELETEHEIGHT = "OptDeleteHeight";

      /// <summary>
      /// alle Höhenangaben werden aus den Trackpunkten entfernt
      /// </summary>
      public static bool DeleteHeight {
         get {
            return Value(OPT_DELETEHEIGHT, false);
         }
         set {
            SetValue(OPT_DELETEHEIGHT, value);
         }
      }


      const string OPT_GAPFILL = "OptGapFill";

      /// <summary>
      /// fehlende Höhenwerte und Zeitstempel linear interpolieren
      /// </summary>
      public static bool GapFill {
         get {
            return Value(OPT_GAPFILL, false);
         }
         set {
            SetValue(OPT_GAPFILL, value);
         }
      }


      const string OPT_CONSTANTHEIGHT = "OptConstantHeight";

      /// <summary>
      /// alle Höhen werden in den Trackpunkten auf einen konstanten Wert gesetzt
      /// </summary>
      public static double ConstantHeight {
         get {
            return Value(OPT_CONSTANTHEIGHT, -1.0);
         }
         set {
            SetValue(OPT_CONSTANTHEIGHT, value);
         }
      }


      const string OPT_HORIZONTALSIMPLIFICATION = "OptHorizontalSimplification";

      /// <summary>
      /// Vereinfachung der Tracks [mit Algorithmus Reumann-Witkam (RW) oder Douglas-Peucker (DP)]
      /// </summary>
      public static string HorizontalSimplification {
         get {
            return Value(OPT_HORIZONTALSIMPLIFICATION, "");
         }
         set {
            SetValue(OPT_HORIZONTALSIMPLIFICATION, value);
         }
      }


      const string OPT_HORIZONTALWIDTH = "OptHorizontalWidth";

      /// <summary>
      /// Breite des Toleranzbereiches für die Vereinfachung (Standard 0.05)
      /// </summary>
      public static double HorizontalWidth {
         get {
            return Value(OPT_HORIZONTALWIDTH, 0.05);
         }
         set {
            SetValue(OPT_HORIZONTALWIDTH, value);
         }
      }


      const string OPT_HORIZONTALMAXSPEED = "OptHorizontalMaxSpeed";

      /// <summary>
      /// Punkte entfernen, die mit einer höheren Geschwindigkeit in km/h erreicht werden
      /// </summary>
      public static double HorizontalMaxSpeed {
         get {
            return Value(OPT_HORIZONTALMAXSPEED, -1.0);
         }
         set {
            SetValue(OPT_HORIZONTALMAXSPEED, value);
         }
      }


      const string OPT_MINHEIGHT = "OptMinHeight";

      /// <summary>
      /// Minimalhöhe; alle kleineren Höhen werden damit ersetzt
      /// </summary>
      public static double MinHeight {
         get {
            return Value(OPT_MINHEIGHT, -1.0);
         }
         set {
            SetValue(OPT_MINHEIGHT, value);
         }
      }


      const string OPT_MAXHEIGHT = "OptMaxHeight";

      /// <summary>
      /// Maximalhöhe; alle kleineren Höhen werden damit ersetzt
      /// </summary>
      public static double MaxHeight {
         get {
            return Value(OPT_MAXHEIGHT, -1.0);
         }
         set {
            SetValue(OPT_MAXHEIGHT, value);
         }
      }


      const string OPT_VERTICALSIMPLIFICATION = "OptVerticalSimplification";

      /// <summary>
      /// Höhenprofil vereinfachen [mit Algorithmus SlidingIntegral (SI) oder SlidingMean (SM)]
      /// </summary>
      public static string VerticalSimplification {
         get {
            return Value(OPT_VERTICALSIMPLIFICATION, "");
         }
         set {
            SetValue(OPT_VERTICALSIMPLIFICATION, value);
         }
      }


      const string OPT_VERTICALWIDTH = "OptVerticalWidth";

      /// <summary>
      /// Breite des Höhen-Integrationsbereiches in Metern (Standard 100m)
      /// </summary>
      public static double VerticalWidth {
         get {
            return Value(OPT_VERTICALWIDTH, 100.0);
         }
         set {
            SetValue(OPT_VERTICALWIDTH, value);
         }
      }


      const string OPT_VERTICALOUTLIERWIDTH = "OptVerticalOutlierWidth";

      /// <summary>
      /// Länge des Bereiches für die 'Ausreißer'-Korrektur von Höhen (Standard 50m)
      /// </summary>
      public static double VerticalOutlierWidth {
         get {
            return Value(OPT_VERTICALOUTLIERWIDTH, 50.0);
         }
         set {
            SetValue(OPT_VERTICALOUTLIERWIDTH, value);
         }
      }


      const string OPT_MAXASCENT = "OptMaxAscent";

      /// <summary>
      /// max. gültiger An-/Abstieg in Prozent (Standard 25%)
      /// </summary>
      public static double MaxAscent {
         get {
            return Value(OPT_MAXASCENT, 25);
         }
         set {
            SetValue(OPT_MAXASCENT, value);
         }
      }


      const string OPT_FILENAME2TRACKNAME = "OptFilename2TrackName";

      /// <summary>
      /// Tracknamen auf den Dateinamen setzen (Standard: false)
      /// </summary>
      public static bool Filename2TrackName {
         get {
            return Value(OPT_FILENAME2TRACKNAME, false);
         }
         set {
            SetValue(OPT_FILENAME2TRACKNAME, value);
         }
      }


      const string OPT_ONEFILEPERTRACK = "OptOneFilePerTrack";

      /// <summary>
      /// jeden Track in einer eigenen Datei ausgeben (Standard: false)
      /// </summary>
      public static bool OneFilePerTrack {
         get {
            return Value(OPT_ONEFILEPERTRACK, false);
         }
         set {
            SetValue(OPT_ONEFILEPERTRACK, value);
         }
      }


      const string OPT_KMLDATACOLOR = "OptKmlTrackDataColor";

      /// <summary>
      /// Farbe für KML-Tracks
      /// </summary>
      public static Color KmlColor {
         get {
            return Color.FromHex(Value(OPT_KMLDATACOLOR, "#FFC000"));
         }
         set {
            Application.Current.Properties[OPT_KMLDATACOLOR] = value.ToHex();
         }
      }

      const string OPT_KMLDATAWIDTH = "OptKmlTrackDataWidth";

      /// <summary>
      /// Breite der KML-Tracks
      /// </summary>
      public static int KmlWidth {
         get {
            return Value(OPT_KMLDATAWIDTH, 4);
         }
         set {
            Application.Current.Properties[OPT_KMLDATAWIDTH] = value.ToString();
         }
      }


   }
}
