using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GpxToolExt {
   /// <summary>
   /// eine einfache Item-Liste, bei der jedes Item aus 2 Texten besteht
   /// </summary>
   [DesignTimeVisible(true)]
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class ItemList : ContentView {

      public class Item {

         public string Data1 { get; }

         public string Data2 { get; }

         public Item(string data1, string data2) {
            Data1 = data1;
            Data2 = data2;
         }

         public override string ToString() {
            return string.Format("{0} / {1}", Data1, Data2);
         }
      }

      class ItemAndFrame {

         public Item Item { get; }

         public Frame Frame { get; }

         public ItemAndFrame(Item item, Frame frame) {
            Item = item;
            Frame = frame;
         }

      }

      public class ElementTappedEventArgs : EventArgs {
         /// <summary>
         /// ausgewähltes Item
         /// </summary>
         public Item Item { get; private set; }

         /// <summary>
         /// akt. Pos. in der Itemliste
         /// </summary>
         public int Position { get; private set; }

         public ElementTappedEventArgs(Item item, int position) {
            Item = item;
            Position = position;
         }

         public override string ToString() {
            return string.Format("{0}: {1}", Position, Item);
         }
      }

      /// <summary>
      /// wird beim Tippen auf ein Element ausgelöst
      /// </summary>
      public event EventHandler<ElementTappedEventArgs> OnElementTapped;

      /// <summary>
      /// Elementanzahl
      /// </summary>
      public int Count {
         get {
            return ItemAndFrames.Count;
         }
      }

      readonly List<ItemAndFrame> ItemAndFrames;

      /// <summary>
      /// gesamte Zelle (Frame)
      /// </summary>
      Style FrameStyle;
      /// <summary>
      /// Hauptext (Label)
      /// </summary>
      Style Dat1Style;
      /// <summary>
      /// Zusatztext (Label)
      /// </summary>
      Style Dat2Style;


      #region Binding-Vars

      #region  Binding-Var ElementBackgroundColor

      public static readonly BindableProperty ElementBackgroundColorProperty = BindableProperty.Create(
         nameof(ElementBackgroundColor),
         typeof(Color),
         typeof(ItemList),
         Color.LightBlue);

      /// <summary>
      /// Farbe der Items
      /// </summary>
      public Color ElementBackgroundColor {
         get => (Color)GetValue(ElementBackgroundColorProperty);
         set => SetValue(ElementBackgroundColorProperty, value);
      }

      #endregion

      #region  Binding-Var ElementBorderColor

      public static readonly BindableProperty ElementBorderColorProperty = BindableProperty.Create(
         nameof(ElementBorderColor),
         typeof(Color),
         typeof(ItemList),
         Color.Blue);

      /// <summary>
      /// Randfarbe der Items
      /// </summary>
      public Color ElementBorderColor {
         get => (Color)GetValue(ElementBorderColorProperty);
         set => SetValue(ElementBorderColorProperty, value);
      }

      #endregion

      #region Binding-Var ElementCornerRadius

      public static readonly BindableProperty ElementCornerRadiusProperty = BindableProperty.Create(
         nameof(ElementCornerRadius),
         typeof(float),
         typeof(ItemList),
         5F);

      /// <summary>
      /// Rundung des Items
      /// </summary>
      public float ElementCornerRadius {
         get => (float)GetValue(ElementCornerRadiusProperty);
         set => SetValue(ElementCornerRadiusProperty, value);
      }

      #endregion

      #region  Binding-Var ElementTextColor

      public static readonly BindableProperty ElementTextColorProperty = BindableProperty.Create(
         nameof(ElementTextColor),
         typeof(Color),
         typeof(ItemList),
         Color.Black);

      /// <summary>
      /// Textfarbe der Items
      /// </summary>
      public Color ElementTextColor {
         get => (Color)GetValue(ElementTextColorProperty);
         set => SetValue(ElementTextColorProperty, value);
      }

      #endregion

      #region Binding-Var ElementTextSize

      public static readonly BindableProperty ElementTextSizeProperty = BindableProperty.Create(
         nameof(ElementTextSize),
         typeof(double),
         typeof(ItemList),
         Device.GetNamedSize(NamedSize.Medium, typeof(Label)));

      /// <summary>
      /// Textgröße des Items
      /// </summary>
      public double ElementTextSize {
         get => (double)GetValue(ElementTextSizeProperty);
         set => SetValue(ElementTextSizeProperty, value);
      }

      #endregion

      #region  Binding-Var ElementExtTextColor

      public static readonly BindableProperty ElementExtTextColorProperty = BindableProperty.Create(
         nameof(ElementExtTextColor),
         typeof(Color),
         typeof(ItemList),
         Color.Red);

      /// <summary>
      /// Textfarbe der Items
      /// </summary>
      public Color ElementExtTextColor {
         get => (Color)GetValue(ElementExtTextColorProperty);
         set => SetValue(ElementExtTextColorProperty, value);
      }

      #endregion

      #region Binding-Var ElementExtTextSize

      public static readonly BindableProperty ElementExtTextSizeProperty = BindableProperty.Create(
         nameof(ElementExtTextSize),
         typeof(double),
         typeof(ItemList),
         Device.GetNamedSize(NamedSize.Small, typeof(Label)));

      /// <summary>
      /// Textgröße des Items
      /// </summary>
      public double ElementExtTextSize {
         get => (double)GetValue(ElementExtTextSizeProperty);
         set => SetValue(ElementExtTextSizeProperty, value);
      }

      #endregion

      #region Binding-Var ElementFrameStyleName

      public static readonly BindableProperty ElementFrameStyleNameProperty = BindableProperty.Create(
         nameof(ElementFrameStyleName),
         typeof(string),
         typeof(ItemList),
         null);

      /// <summary>
      /// Textgröße des Items
      /// </summary>
      public string ElementFrameStyleName {
         get => GetValue(ElementFrameStyleNameProperty) as string;
         set => SetValue(ElementFrameStyleNameProperty, value);
      }

      #endregion

      #region Binding-Var Dat1StyleName

      public static readonly BindableProperty Dat1StyleNameProperty = BindableProperty.Create(
         nameof(Dat1StyleName),
         typeof(string),
         typeof(ItemList),
         null);

      /// <summary>
      /// Textgröße des Items
      /// </summary>
      public string Dat1StyleName {
         get => GetValue(Dat1StyleNameProperty) as string;
         set => SetValue(Dat1StyleNameProperty, value);
      }

      #endregion

      #region Binding-Var Dat2StyleName

      public static readonly BindableProperty Dat2StyleNameProperty = BindableProperty.Create(
         nameof(Dat2StyleName),
         typeof(string),
         typeof(ItemList),
         null);

      /// <summary>
      /// Textgröße des Items
      /// </summary>
      public string Dat2StyleName {
         get => GetValue(Dat2StyleNameProperty) as string;
         set => SetValue(Dat2StyleNameProperty, value);
      }

      #endregion

      #region Binding-Var ElementPadding

      public static readonly BindableProperty ElementPaddingProperty = BindableProperty.Create(
         "PeekAreaInsets",
         typeof(Thickness),
         typeof(ItemList),
         new Thickness(10, 5, 10, 5));

      /// <summary>
      /// PeekAreaInsets der 3 CarouselViews
      /// </summary>
      public Thickness ElementPadding {
         get => (Thickness)GetValue(ElementPaddingProperty);
         set {
            if (value != null &&
                value != (Thickness)GetValue(ElementPaddingProperty)) {
               SetValue(ElementPaddingProperty, value);
            }
         }
      }

      #endregion

      #endregion

      readonly TapGestureRecognizer tapGestureRecognizer4Frame;


      public ItemList() {
         InitializeComponent();

         ItemAndFrames = new List<ItemAndFrame>();

         tapGestureRecognizer4Frame = new TapGestureRecognizer();
         tapGestureRecognizer4Frame.Tapped += TapGestureRecognizer4Frame_Tapped;

         elementStack.Children.Clear();

         //if (DesignMode.IsDesignModeEnabled) {
         //   initStyles();
         //   Add("/storage/emulated/0/Test/test.gpx", "1024 Byte/1 kByte, 1.1.1851, 13:14:15");
         //   Add("ABC", "567");
         //}
      }



      /// <summary>
      /// Ist ein Style per Name angegeben und existiert in den Programm-Ressourcen, wird er verwendet.
      /// Andernfalls werden die anderen Properties dafür verwendet.
      /// </summary>
      /// <param name="page"></param>
      void initStyles() {
         //if (!string.IsNullOrEmpty(ElementFrameStyleName) &&
         //    Application.Current.Resources.ContainsKey(ElementFrameStyleName))
         //   FrameStyle = Application.Current.Resources[ElementFrameStyleName] as Style;
         //else
         FrameStyle = new Style(typeof(Frame)) {
            Setters = {
                   new Setter { Property = BackgroundColorProperty, Value = ElementBackgroundColor },
                   new Setter { Property = Frame.BorderColorProperty, Value = ElementBorderColor },
                   new Setter { Property = Frame.CornerRadiusProperty, Value = ElementCornerRadius },
                   new Setter { Property = Frame.HasShadowProperty, Value = false },
                   new Setter { Property = PaddingProperty, Value = ElementPadding },
                   new Setter { Property = HorizontalOptionsProperty, Value = LayoutOptions.FillAndExpand},
               }
         };
         foreach (ItemAndFrame item in ItemAndFrames) {
            item.Frame.Style = FrameStyle;
         }

         //if (!string.IsNullOrEmpty(Dat1StyleName) &&
         //    Application.Current.Resources.ContainsKey(Dat1StyleName))
         //   Dat1Style = Application.Current.Resources[Dat1StyleName] as Style;
         //else
         Dat1Style = new Style(typeof(Label)) {
            Setters = {
                   new Setter { Property = Label.TextColorProperty, Value = ElementTextColor },
                   new Setter { Property = Label.FontSizeProperty, Value = ElementTextSize },
                   new Setter { Property = HorizontalOptionsProperty, Value= LayoutOptions.FillAndExpand },
               }
         };

         //if (!string.IsNullOrEmpty(Dat2StyleName) &&
         //    Application.Current.Resources.ContainsKey(Dat2StyleName))
         //   Dat2Style = Application.Current.Resources[Dat2StyleName] as Style;
         //else
         Dat2Style = new Style(typeof(Label)) {
            Setters = {
                   new Setter { Property = Label.TextColorProperty, Value = ElementExtTextColor },
                   new Setter { Property = Label.FontSizeProperty, Value = ElementExtTextSize },
                   new Setter { Property = Label.HorizontalTextAlignmentProperty, Value= TextAlignment.End },
                   new Setter { Property = HorizontalOptionsProperty, Value= LayoutOptions.FillAndExpand },
               }
         };
      }

      /// <summary>
      /// Liste löschen
      /// </summary>
      public void Clear() {
         elementStack.Children.Clear();
         ItemAndFrames.Clear();
      }

      /// <summary>
      /// Element in die Liste einfügen
      /// </summary>
      /// <param name="pos"></param>
      /// <param name="dat"></param>
      public void Insert(int pos, Item dat) {
         if (0 <= pos && pos <= ItemAndFrames.Count) {
            if (pos <= ItemAndFrames.Count)
               Add(dat);
            else {
               Frame frame = createCell(dat);
               ItemAndFrames.Insert(pos, new ItemAndFrame(dat, frame));
               elementStack.Children.Insert(pos, frame);
            }
         }
      }

      /// <summary>
      /// Element in die Liste einfügen
      /// </summary>
      /// <param name="pos"></param>
      /// <param name="text"></param>
      /// <param name="exttext"></param>
      public void Insert(int pos, string text, string exttext) {
         Insert(pos, new Item(text, exttext));
      }

      /// <summary>
      /// Element an die Liste anfügen
      /// </summary>
      /// <param name="dat"></param>
      public void Add(Item dat) {
         Frame frame = createCell(dat);
         ItemAndFrames.Add(new ItemAndFrame(dat, frame));
         elementStack.Children.Add(frame);
      }

      /// <summary>
      /// Element an die Liste anfügen
      /// </summary>
      /// <param name="text"></param>
      /// <param name="exttext"></param>
      public void Add(string text, string exttext) {
         Add(new Item(text, exttext));
      }

      /// <summary>
      /// Element der Liste löschen
      /// </summary>
      /// <param name="pos"></param>
      public void RemoveAt(int pos) {
         if (0 <= pos && pos < ItemAndFrames.Count) {
            ItemAndFrames.RemoveAt(pos);
            elementStack.Children.RemoveAt(pos);
         }
      }

      /// <summary>
      /// liefert ein bestimmtes Element
      /// </summary>
      /// <param name="pos"></param>
      /// <returns></returns>
      public Item Get(int pos) {
         if (0 <= pos && pos < ItemAndFrames.Count)
            return ItemAndFrames[pos].Item;
         return null;
      }

      /* jedes einzelne Element der Liste:
       
      <Frame Style="{StaticResource PseudolistCellFrame}">
         <StackLayout Orientation="Vertical">
            <Label Text="ghi 0123456789" TextColor="Black" />
            <Label Text="123456789 Bytes" TextColor="Black" />
         </StackLayout>
         <Frame.GestureRecognizers>
            <TapGestureRecognizer Tapped="PseudoListviewSrcFiles_Tapped"/>
         </Frame.GestureRecognizers>
      </Frame>
      */
      Frame createCell(Item dat) {
         initStyles();

         StackLayout sl = new StackLayout {
            Orientation = StackOrientation.Vertical
         };

         sl.Children.Add(new Label {
            Style = Dat1Style,
            Text = dat.Data1
         });

         sl.Children.Add(new Label {
            Style = Dat2Style,
            Text = dat.Data2
         });

         Frame frame = new Frame {
            Style = FrameStyle,
            Content = sl
         };

         frame.GestureRecognizers.Add(tapGestureRecognizer4Frame);

         return frame;
      }

      void TapGestureRecognizer4Frame_Tapped(object sender, EventArgs e) {
         if (sender is Frame) {
            Frame frame = sender as Frame;
            for (int i = 0; i < ItemAndFrames.Count; i++)
               if (frame == ItemAndFrames[i].Frame) {
                  OnElementTapped?.Invoke(this, new ElementTappedEventArgs(ItemAndFrames[i].Item, i));
                  break;
               }
         }
      }


   }
}