using System;
using System.IO;
using System.Windows.Forms;
using MongoDB.Driver;

namespace Paperless
{
    public partial class Form1 : Form
    {
        private Cabinet _cabinet;

        public Form1(Cabinet cabinet)
        {
            InitializeComponent();

            _cabinet = cabinet;

            ResetForm();
        }


        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "PDF files (*.pdf)|*.pdf";
                dlg.Multiselect = false;

                if (dlg.ShowDialog() != DialogResult.OK) return;

                if (!File.Exists(dlg.FileName)) return;

                txtFilename.Text = dlg.FileName;
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            _cabinet.FileDocument(
                dpDate.Value,
                txtTags.Text.Split(',').Map(s => s.Trim()),
                txtFilename.Text,
                FileHelper.GetPdfContent(txtFilename.Text));

            ResetForm();
        }

        private void ResetForm()
        {
            txtTags.Text = String.Empty;
            dpDate.Value = DateTime.Today;
            txtFilename.Text = String.Empty;
        }
    }
}
