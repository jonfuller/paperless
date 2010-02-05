using System;
using System.Windows.Forms;
using MongoDB.Driver;

namespace Paperless
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var cabinet = new Cabinet(new Mongo(), "Paperless"))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1(cabinet));
            };
        }
    }
}
