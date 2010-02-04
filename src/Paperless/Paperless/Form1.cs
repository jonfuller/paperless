using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MongoDB.Driver;

// date needs to be defaulted to today, but changeable.

namespace Paperless
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Whatever();
        }

        private void Whatever()
        {
            var mongo = new Mongo();
            mongo.Connect();
            var db = mongo.getDB("Paperless");
            var collection = db.GetCollection("docs");

            collection.Insert(new Document()
                                      {
                                          {"tags", new[]{"bill", "sandi", "receipt", "2010"}},
                                          {"date", DateTime.Now},
                                          {"original_filename", "check it out.pdf"},
                                          {"content", new Binary( GetPdfContent(@"C:\Users\jon\Dev\Paperless\test\1page.pdf"))}
                                      });
        
        }

        private byte[] GetPdfContent(string filename)
        {
            using (var file = new FileStream(filename, FileMode.Open))
            using (var reader = new BinaryReader(file))
            {
                var content = new List<byte>();

                byte[] readBuffer = new byte[4096];
                int lengthRead;

                do
                {
                    lengthRead = reader.Read(readBuffer, 0, readBuffer.Length);

                    content.AddRange(readBuffer.ToList().GetRange(0, lengthRead));
                } while (lengthRead > 0);

                return content.ToArray();
            }
        }
    }
}
