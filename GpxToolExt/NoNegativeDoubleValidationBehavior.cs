using System;
using Xamarin.Forms;

namespace GpxToolExt {
   class NoNegativeDoubleValidationBehavior : Behavior<Entry> {
      protected override void OnAttachedTo(Entry entry) {
         entry.TextChanged += OnEntryTextChanged;
         base.OnAttachedTo(entry);
      }

      protected override void OnDetachingFrom(Entry entry) {
         entry.TextChanged -= OnEntryTextChanged;
         base.OnDetachingFrom(entry);
      }

      private static void OnEntryTextChanged(object sender, TextChangedEventArgs args) {
         if (!string.IsNullOrWhiteSpace(args.NewTextValue)) {
            bool isValid;
            try {
               isValid = Convert.ToDouble(args.NewTextValue) >= 0;
            } catch {
               isValid = false;
            }

            ((Entry)sender).Text = isValid ?
                                       args.NewTextValue :
                                       args.NewTextValue.Remove(args.NewTextValue.Length - 1);
         }
      }
   }
}
