using System;
using System.IO;

namespace GpxToolExt {
   /// <summary>
   /// <see cref="MemoryStream"/> der ein <see cref="NewTextEvent"/> auslöst, wenn Text angefügt wird
   /// </summary>
   class MyMemoryStream : MemoryStream {

      public class NewTextEventArgs : EventArgs {
         public NewTextEventArgs(string text) {
            NewText = text;
         }

         public string NewText { get; set; }
      }

      public event EventHandler<NewTextEventArgs> NewTextEvent;


      public MyMemoryStream(int cap) : base(cap) { }

      public override void Write(byte[] buffer, int offset, int count) {
         base.Write(buffer, offset, count);

         byte[] tmp = new byte[count];
         Array.Copy(buffer, offset, tmp, 0, count);
         using (StreamReader sr = new StreamReader(new MemoryStream(tmp))) {
            NewTextEvent?.Invoke(this, new NewTextEventArgs(sr.ReadToEnd()));
         }
      }

      //public override void WriteByte(byte value) {
      //   base.WriteByte(value);
      //}

      //public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) {
      //   return base.WriteAsync(buffer, offset, count, cancellationToken);
      //}

   }
}
