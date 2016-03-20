using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace string_svo_reader
{
    public partial class Form1 : Form
    {
        private TSSFile TSS;

        private int CurrentString;
        private bool FileOpen;
        private char[] newline = { '\n' };
        private char[] newlinereturn = { '\r', '\n' };
        private String CurrentFilename;

        public Form1()
        {
            InitializeComponent();
            FileOpen = false;
        }

        private bool OpenFile()
        {
            OpenFileDialog FileDialog = new OpenFileDialog();
            FileDialog.DefaultExt = ".so";
            FileDialog.Filter = "TSS files|*.*";
            FileDialog.FileName = "STRING_DIC.SO";
            if (FileDialog.ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            byte[] MaybeTSSFile;
            try
            {
                MaybeTSSFile = System.IO.File.ReadAllBytes(FileDialog.FileName);
                TSS = new TSSFile(MaybeTSSFile);
                numericUpDown1.Maximum = TSS.Entries.Length-1;
                CurrentFilename = FileDialog.FileName;
                return true;
            }
            catch (Exception e)
            {
                Util.DisplayException(e);
                return false;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try {
                if (!FileOpen) return;

                if (checkBoxAutosave.Checked)
                {
                    this.buttonSaveString_Click(sender, e);
                }

                CurrentString = (int)numericUpDown1.Value;

                if (TSS.Entries[CurrentString].StringJPN != null)
                {
                    textBoxText.Text = TSS.Entries[CurrentString].StringJPN.Replace(new String(newline), new String(newlinereturn));
                    textBoxText.ReadOnly = false;
                    textBoxText.BackColor = Color.White;
                }
                else
                {
                    textBoxText.Text = "";
                    textBoxText.ReadOnly = true;
                    textBoxText.BackColor = Color.Gray;
                }

                if (TSS.Entries[CurrentString].StringENG != null)
                {
                    textBoxEnglish.Text = TSS.Entries[CurrentString].StringENG.Replace(new String(newline), new String(newlinereturn));
                    textBoxEnglish.ReadOnly = false;
                    textBoxEnglish.BackColor = Color.White;
                }
                else
                {
                    textBoxEnglish.Text = "";
                    textBoxEnglish.ReadOnly = true;
                    textBoxEnglish.BackColor = Color.Gray;
                }
            } catch (Exception) {
                
            }
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            if (OpenFile())
            {
                FileOpen = true;
                numericUpDown1_ValueChanged(this, null);
            }
            else
            {
                MessageBox.Show("Failed opening file!");
            }
        }

        private void numericUpDown1_KeyUp(object sender, KeyEventArgs e)
        {
            numericUpDown1_ValueChanged(sender, null);
        }

        private void buttonSaveString_Click(object sender, EventArgs e)
        {
            if (!FileOpen) return;
            try
            {
                if (TSS.Entries[CurrentString].StringJPN != null)
                {
                    TSS.Entries[CurrentString].StringJPN = textBoxText.Text.Replace(new String(newlinereturn), new String(newline));
                }
                if (TSS.Entries[CurrentString].StringENG != null)
                {
                    TSS.Entries[CurrentString].StringENG = textBoxEnglish.Text.Replace(new String(newlinereturn), new String(newline));
                }
            }
            catch (Exception)
            {

            }
        }

        private void buttonSaveFile_Click(object sender, EventArgs e)
        {
            if (!FileOpen) return;
            SaveFileDialog FileDialog = new SaveFileDialog();
            FileDialog.DefaultExt = ".so";
            FileDialog.Filter = "TSS files|*.*";
            int dot = CurrentFilename.LastIndexOf('.');
            FileDialog.FileName = CurrentFilename.Substring(0, dot) + "_new" + CurrentFilename.Substring(dot);
            if (FileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            
            byte[] TSSnew = TSS.Serialize();
            System.IO.File.WriteAllBytes(FileDialog.FileName, TSSnew);
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (!FileOpen) return;
            SaveFileDialog FileDialog = new SaveFileDialog();
            FileDialog.DefaultExt = ".txt";
            FileDialog.Filter = "Text file|*.txt|Any file|*.*";
            FileDialog.FileName = CurrentFilename + "_export.txt";
            if (FileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            System.IO.File.WriteAllBytes(FileDialog.FileName, TSS.ExportText());
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            if (!FileOpen) return;

            OpenFileDialog FileDialog = new OpenFileDialog();
            FileDialog.DefaultExt = ".txt";
            FileDialog.Filter = "Text file|*.txt|Any file|*.*";
            FileDialog.FileName = CurrentFilename + "_export.txt";
            if (FileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            byte[] ImportFile;
            try
            {
                ImportFile = System.IO.File.ReadAllBytes(FileDialog.FileName);
                TSS.ImportText(ImportFile);
            }
            catch (Exception ex)
            {
                Util.DisplayException(ex);
            }

            if (checkBoxAutosave.Checked)
            {
                checkBoxAutosave.Checked = false;
                numericUpDown1_ValueChanged(sender, null);
                checkBoxAutosave.Checked = true;
            }
            else
            {
                numericUpDown1_ValueChanged(sender, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
