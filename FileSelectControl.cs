using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ToVPatcher.Properties;

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

		public bool Successful;

		public FileSelectControl() {
			InitializeComponent();
			pictureBox1.Hide();
			Successful = false;
			backgroundWorker.WorkerReportsProgress = true;
		}

		private void selectFileButton_Click( object sender, EventArgs e ) {
			var dialog = new OpenFileDialog();
			dialog.FileName = LabelText;
			var result = dialog.ShowDialog();
			if ( result == DialogResult.OK ) {
				FilePath = dialog.FileName;
				Successful = false;
			}
		}

		public void ShowIconNone() {
			pictureBox1.Hide();
		}
		public void ShowIconSuccess() {
			pictureBox1.Image = Resources.dialog_clean;
			pictureBox1.Show();
		}
		public void ShowIconError() {
			pictureBox1.Image = Resources.exclamation;
			pictureBox1.Show();
		}
		public void ShowIconLoading() {
			pictureBox1.Image = Resources.loading;
			pictureBox1.Show();
		}

		public delegate void PatchDelegate( string file, string patchDir, string outDir );
		public PatchDelegate PatchFunction;
		public string OutDir;

		public void StartWorker() {
			if ( !Successful && !backgroundWorker.IsBusy ) {
				ShowIconLoading();
				backgroundWorker.RunWorkerAsync();
			}
		}

		private void backgroundWorker_DoWork( object sender, DoWorkEventArgs e ) {
			BackgroundWorker worker = ( (BackgroundWorker)sender );
			worker.ReportProgress( 0, "Patching " + LabelText + "..." );
			PatchFunction( FilePath, PatchDir, OutDir );
			worker.ReportProgress( 100, "Successfully patched " + LabelText + "!" );
		}

		private void backgroundWorker_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			if ( e.UserState != null ) {
				labelStatusMessage.Text = (string)e.UserState;
			}
		}

		private void backgroundWorker_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			if ( e.Error != null ) {
				ShowIconError();
				labelStatusMessage.Text = "Error: " + e.Error.GetType() + ": " + e.Error.Message;
			} else {
				ShowIconSuccess();
				Successful = true;
			}
		}
	}
}
