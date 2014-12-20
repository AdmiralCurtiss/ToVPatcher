using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using HyoutaTools;

namespace ToVPatcher {
	public partial class PatchForm : Form {
		List<FileSelectControl> FileSelectControls = new List<FileSelectControl>();
		Dictionary<string, string> OutputChecksums = new Dictionary<string,string>();
		bool IsInteractionEnabled = true;

		public PatchForm() {
			InitializeComponent();
		}

		private void PatchForm_Load( object sender, EventArgs e ) {
			LoadOutputChecksums();

			fileSelectControlElf.LabelText = "EBOOT.BIN";
			fileSelectControlElf.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "EBOOT.BIN" );
			fileSelectControlElf.PatchDir = "new/patches";
			fileSelectControlElf.PatchFunction = ElfPatcher.PatchElf;
			fileSelectControlElf.OutputChecksum = OutputChecksums.GetValueOrDefault( "ToV.elf", null );
			FileSelectControls.Add( fileSelectControlElf );

			fileSelectControlString.LabelText = "string.svo";
			fileSelectControlString.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "string.svo" );
			fileSelectControlString.PatchDir = "new/patches";
			fileSelectControlString.PatchFunction = Patcher.PatchString;
			fileSelectControlString.OutputChecksum = OutputChecksums.GetValueOrDefault( "string.svo", null );
			FileSelectControls.Add( fileSelectControlString ); 

			fileSelectControlScenario.LabelText = "scenario.dat";
			fileSelectControlScenario.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "scenario.dat" );
			fileSelectControlScenario.PatchDir = "new/patches/scenario";
			fileSelectControlScenario.PatchFunction = Patcher.PatchScenario;
			fileSelectControlScenario.OutputChecksum = OutputChecksums.GetValueOrDefault( "scenario.dat", null );
			FileSelectControls.Add( fileSelectControlScenario ); 

			fileSelectControlBtl.LabelText = "btl.svo";
			fileSelectControlBtl.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "btl.svo" );
			fileSelectControlBtl.PatchDir = "new/patches/btl";
			fileSelectControlBtl.PatchFunction = Patcher.PatchBtl;
			fileSelectControlBtl.OutputChecksum = OutputChecksums.GetValueOrDefault( "btl.svo", null );
			FileSelectControls.Add( fileSelectControlBtl ); 

			fileSelectControlChat.LabelText = "chat.svo";
			fileSelectControlChat.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "chat.svo" );
			fileSelectControlChat.PatchDir = "new/patches/chat";
			fileSelectControlChat.PatchFunction = Patcher.PatchChat;
			fileSelectControlChat.OutputChecksum = OutputChecksums.GetValueOrDefault( "chat.svo", null );
			FileSelectControls.Add( fileSelectControlChat ); 

			fileSelectControlUI.LabelText = "UI.svo";
			fileSelectControlUI.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "UI.svo" );
			fileSelectControlUI.PatchDir = "new/patches/UI";
			fileSelectControlUI.PatchFunction = Patcher.PatchUI;
			fileSelectControlUI.OutputChecksum = OutputChecksums.GetValueOrDefault( "UI.svo", null );
			FileSelectControls.Add( fileSelectControlUI ); 

			fileSelectControlEffect.LabelText = "effect.svo";
			fileSelectControlEffect.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "effect.svo" );
			fileSelectControlEffect.PatchDir = "new/patches/effect";
			fileSelectControlEffect.PatchFunction = Patcher.PatchEffect;
			fileSelectControlEffect.OutputChecksum = OutputChecksums.GetValueOrDefault( "effect.svo", null );
			FileSelectControls.Add( fileSelectControlEffect ); 

			fileSelectControlChara.LabelText = "chara.svo";
			fileSelectControlChara.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "chara.svo" );
			fileSelectControlChara.PatchDir = "new/patches/chara";
			fileSelectControlChara.PatchFunction = Patcher.PatchChara;
			fileSelectControlChara.OutputChecksum = OutputChecksums.GetValueOrDefault( "chara.svo", null );
			FileSelectControls.Add( fileSelectControlChara ); 

			fileSelectControlParam.LabelText = "PARAM.SFO";
			fileSelectControlParam.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "PARAM.SFO" );
			fileSelectControlParam.PatchDir = null;
			fileSelectControlParam.PatchFunction = Patcher.PatchParam;
			fileSelectControlParam.OutputChecksum = OutputChecksums.GetValueOrDefault( "PARAM.SFO", null );
			FileSelectControls.Add( fileSelectControlParam ); 

			fileSelectControlTrophy.LabelText = "TROPHY.TRP";
			fileSelectControlTrophy.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "TROPHY.TRP" );
			fileSelectControlTrophy.PatchDir = "new/patches";
			fileSelectControlTrophy.PatchFunction = Patcher.PatchTrophy;
			fileSelectControlTrophy.OutputChecksum = OutputChecksums.GetValueOrDefault( "TROPHY.TRP", null );
			FileSelectControls.Add( fileSelectControlTrophy );
		}

		private void LoadOutputChecksums() {
			string path = "new/patches/checksums.md5";
			if ( File.Exists( path ) ) {
				foreach ( string line in File.ReadAllLines( path ) ) {
					string md5 = line.Substring( 0, 32 );
					string filename = line.Substring( 34 );
					OutputChecksums.Add( filename, md5 );
				}
			} else {
				MessageBox.Show( this,
					"File containing checksums for the patched files could not be found." + Environment.NewLine +
					"Please make sure the archive containing the patch was fully extracted and no files were moved or renamed." + Environment.NewLine +
					"The patcher will still run, but protection against incorrect patches will not be provided.",
					"Checksums not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning
				);
			}
		}

		private delegate void BoolDelegate( bool value );
		private void SetInteractionEnabled( bool value ) {
			buttonPatch.Enabled = value;
			foreach ( var ctrl in FileSelectControls ) {
				ctrl.SetInteractionEnabled( value );
			}
			IsInteractionEnabled = value;
		}

		private void buttonPatch_Click( object sender, EventArgs e ) {
			Invoke( new BoolDelegate( SetInteractionEnabled ), false );

			string outDirPath = @"new/patched";
			var outDir = Directory.CreateDirectory( outDirPath );

			Thread thread = new Thread( delegate() {
				foreach ( var ctrl in FileSelectControls ) {
					ctrl.OutDir = outDir.FullName;
					ctrl.StartWorker();
					while ( ctrl.IsWorkerRunning() ) {
						Thread.Sleep( 300 );
					}
				}

				Invoke( new BoolDelegate( SetInteractionEnabled ), true );
			} );
			thread.Start();
		}

		protected override void OnFormClosing( FormClosingEventArgs e ) {
			base.OnFormClosing( e );
			if ( e.CloseReason == CloseReason.WindowsShutDown ) return;

			if ( !IsInteractionEnabled ) {
				MessageBox.Show( this, "Please wait until the patching has completed." );
				e.Cancel = true;
			}
		}
	}
}
