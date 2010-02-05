using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Paperless
{
    public static class FileHelper
    {
        public static byte[] GetPdfContent(string filename)
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
