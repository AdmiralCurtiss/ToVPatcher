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

		public delegate void PatchDelegate( string file, string patchDir, string outDir, BackgroundWorker worker = null );
		public PatchDelegate PatchFunction;
		public string OutDir;

		public void StartWorker() {
			if ( !Successful && !backgroundWorker.IsBusy ) {
				backgroundWorker.RunWorkerAsync();
			}
		}

		public bool IsWorkerRunning() {
			return backgroundWorker.IsBusy;
		}

		public delegate void VoidDelegate();
		public delegate void StringDelegate( string text );
		private void UpdateStatusMessage( string text ) {
			labelStatusMessage.Text = text;
		}

		private void backgroundWorker_DoWork( object sender, DoWorkEventArgs e ) {
			BackgroundWorker worker = ( (BackgroundWorker)sender );
			Invoke( new VoidDelegate( ShowIconLoading ) );
			worker.ReportProgress( 0, "Patching " + LabelText + "..." );
			PatchFunction( FilePath, PatchDir, OutDir, worker );
			worker.ReportProgress( 100, "Successfully patched " + LabelText + "!" );
		}

		private void backgroundWorker_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			if ( e.UserState != null ) {
				Invoke( new StringDelegate( UpdateStatusMessage ), (string)e.UserState );
			}
		}

		private void backgroundWorker_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			if ( e.Error != null ) {
				Invoke( new VoidDelegate( ShowIconError ) );
				Invoke( new StringDelegate( UpdateStatusMessage ), "Error: " + e.Error.GetType() + ": " + e.Error.Message );
			} else {
				Invoke( new VoidDelegate( ShowIconSuccess ) );
				Successful = true;
			}
		}

		public void SetInteractionEnabled( bool value ) {
			selectFileButton.Enabled = value;
			textBox1.Enabled = value;
		}
	}
}
