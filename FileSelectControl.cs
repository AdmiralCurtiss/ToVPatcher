using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToVPatcher {
	public partial class FileSelectControl : UserControl {
		public string LabelText {
			get { return nameLabel.Text; }
			set { nameLabel.Text = value; }
		}
		
		public string FilePath {
			get { return textBox1.Text; }
			set { textBox1.Text = value; }
		}

		public string PatchDir;

		public bool Finished;

		public FileSelectControl() {
			InitializeComponent();
			Finished = false;
		}

		private void selectFileButton_Click( object sender, EventArgs e ) {
			var dialog = new OpenFileDialog();
			dialog.FileName = LabelText;
			var result = dialog.ShowDialog();
			if ( result == DialogResult.OK ) {
				FilePath = dialog.FileName;
				Finished = false;
			}
		}
	}
}
