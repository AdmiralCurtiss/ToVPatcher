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
        private byte[] StringSVO;
        private int CurrentString;

        private bool FileOpen;

        public Form1()
        {
            InitializeComponent();
            FileOpen = false;
        }

        private bool OpenFile()
        {
            OpenFileDialog FileDialog = new OpenFileDialog();
            FileDialog.DefaultExt = ".svo";
            FileDialog.Filter = "string.svo|string.svo|*.svo Files|*.svo|All Files|*.*";
            FileDialog.FileName = "string.svo";
            FileDialog.ShowDialog();
            byte[] MaybeStringSVO;
            try
            {
                MaybeStringSVO = System.IO.File.ReadAllBytes(FileDialog.FileName);
                if (MaybeStringSVO.Length != 3889152)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            StringSVO = MaybeStringSVO;
            return true;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (!FileOpen) return;
            CurrentString = (int)numericUpDown1.Value;
            int PointerLocation = 0x8A4 + (CurrentString * 0x44);

            while (true)
            {
                if (StringSVO[PointerLocation] == 0xFF &&
                     StringSVO[PointerLocation+1] == 0xFF &&
                     StringSVO[PointerLocation+2] == 0xFF &&
                     StringSVO[PointerLocation+3] == 0xFF)
                {
                    PointerLocation -= 0x20;
                    break;
                }
                PointerLocation++;
            }
            char[] newline = { '\n' };
            char[] newlinereturn = { '\r', '\n' };
            textBoxText.Text = GetText(PointerLocation, textBoxtextBoxPointerJPN, textBoxPointerLocJPN).Replace(new String(newline), new String(newlinereturn));
            textBoxEnglish.Text = GetText(PointerLocation + 0x10, textBoxPointerENG, textBoxPointerLocENG).Replace(new String(newline), new String(newlinereturn));
        }

        private String GetText(int PointerLocation, TextBox PointerTextBox, TextBox PointerLocationTextBox)
        {
            try
            {
                if (!FileOpen) return null;
                byte[] PointerBytes = {
                                      StringSVO[PointerLocation+3],
                                      StringSVO[PointerLocation+2],
                                      StringSVO[PointerLocation+1],
                                      StringSVO[PointerLocation]
                                  };
                int Pointer = BitConverter.ToInt32(PointerBytes, 0);
                Pointer += 0x00216F48;

                Encoding ShiftJIS = Encoding.GetEncoding("shift-jis");


                PointerTextBox.Text = Pointer.ToString("X");
                PointerLocationTextBox.Text = PointerLocation.ToString("X");

                String Text = ShiftJIS.GetString(StringSVO, Pointer, 2000);
                return Text;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            if (OpenFile())
            {
                FileOpen = true;
                numericUpDown1_ValueChanged(this, null);
            }
        }

        private void numericUpDown1_KeyUp(object sender, KeyEventArgs e)
        {
            numericUpDown1_ValueChanged(sender, null);
        }


    }
}
