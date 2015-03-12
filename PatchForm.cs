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
			this.Icon = Properties.Resources.icon;
			InitializeComponent();
		}

		bool CheckForExecutable( string file ) {
			if ( !File.Exists( file ) && !File.Exists( file + ".exe" ) ) {
				MessageBox.Show( this,
					file + " could not be found at " + Path.GetFullPath( file + ".exe" ) + "." + Environment.NewLine +
					"Please make sure the archive containing ToVPatcher was fully extracted and no files were moved or renamed, then run the patcher again.",
					file + " found!", MessageBoxButtons.OK, MessageBoxIcon.Error
				);
				return false;
			}
			return true;
		}

		private void PatchForm_Load( object sender, EventArgs e ) {
			TempUtil.RemoveTempFolder();

			if ( !Directory.Exists( "new/patches" ) ) {
				MessageBox.Show( this,
					"Patch folder could not be found at " + Path.GetFullPath( "new/patches" ) + "." + Environment.NewLine +
					"Please make sure the archive containing the patch files was fully extracted and no files were moved or renamed, then run the patcher again.",
					"Patches not found!", MessageBoxButtons.OK, MessageBoxIcon.Error
				);
				Close();
				return;
			}

			if ( !CheckForExecutable( "comptoe" ) || !CheckForExecutable( "xdelta" ) ) {
				Close();
				return;
			}

			string ebootModPath = Path.GetFullPath( "ebootmod/ebootMOD.exe" );
			if ( !File.Exists( ebootModPath ) ) {
				MessageBox.Show( this,
					"ebootMOD could not be found at " + ebootModPath + "." + Environment.NewLine +
					"ebootMOD is required to patch EBOOT.BIN. Please read the readme, find a copy of ebootMOD, and place it at the appropriate location." + Environment.NewLine +
					"The patcher will still run, but will not be able to patch EBOOT.BIN until you do so.",
					"ebootMOD not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning
				);
			}

			LoadOutputChecksums();

			fileSelectControlElf.LabelText = "EBOOT.BIN";
			fileSelectControlElf.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "EBOOT.BIN" );
			fileSelectControlElf.PatchDir = "new/patches";
			fileSelectControlElf.PatchFunction = ElfPatcher.PatchElf;
			fileSelectControlElf.OutputChecksum = OutputChecksums.GetValueOrDefault( "ToV.elf", null );

			fileSelectControlString.LabelText = "string.svo";
			fileSelectControlString.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "string.svo" );
			fileSelectControlString.PatchDir = "new/patches";
			fileSelectControlString.PatchFunction = Patcher.PatchString;
			fileSelectControlString.OutputChecksum = OutputChecksums.GetValueOrDefault( "string.svo", null );

			fileSelectControlScenario.LabelText = "scenario.dat";
			fileSelectControlScenario.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "scenario.dat" );
			fileSelectControlScenario.PatchDir = "new/patches/scenario";
			fileSelectControlScenario.PatchFunction = Patcher.PatchScenario;
			fileSelectControlScenario.OutputChecksum = OutputChecksums.GetValueOrDefault( "scenario.dat", null );

			fileSelectControlBtl.LabelText = "btl.svo";
			fileSelectControlBtl.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "btl.svo" );
			fileSelectControlBtl.PatchDir = "new/patches/btl";
			fileSelectControlBtl.PatchFunction = Patcher.PatchBtl;
			fileSelectControlBtl.OutputChecksum = OutputChecksums.GetValueOrDefault( "btl.svo", null );

			fileSelectControlChat.LabelText = "chat.svo";
			fileSelectControlChat.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "chat.svo" );
			fileSelectControlChat.PatchDir = "new/patches/chat";
			fileSelectControlChat.PatchFunction = Patcher.PatchChat;
			fileSelectControlChat.OutputChecksum = OutputChecksums.GetValueOrDefault( "chat.svo", null );

			fileSelectControlUI.LabelText = "UI.svo";
			fileSelectControlUI.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "UI.svo" );
			fileSelectControlUI.PatchDir = "new/patches/UI";
			fileSelectControlUI.PatchFunction = Patcher.PatchUI;
			fileSelectControlUI.OutputChecksum = OutputChecksums.GetValueOrDefault( "UI.svo", null );

			fileSelectControlEffect.LabelText = "effect.svo";
			fileSelectControlEffect.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "effect.svo" );
			fileSelectControlEffect.PatchDir = "new/patches/effect";
			fileSelectControlEffect.PatchFunction = Patcher.PatchEffect;
			fileSelectControlEffect.OutputChecksum = OutputChecksums.GetValueOrDefault( "effect.svo", null );

			fileSelectControlChara.LabelText = "chara.svo";
			fileSelectControlChara.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "chara.svo" );
			fileSelectControlChara.PatchDir = "new/patches/chara";
			fileSelectControlChara.PatchFunction = Patcher.PatchChara;
			fileSelectControlChara.OutputChecksum = OutputChecksums.GetValueOrDefault( "chara.svo", null );

			fileSelectControlMenu.LabelText = "menu.svo";
			fileSelectControlMenu.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "menu.svo" );
			fileSelectControlMenu.PatchDir = "new/patches";
			fileSelectControlMenu.PatchFunction = Patcher.PatchMenu;
			fileSelectControlMenu.OutputChecksum = OutputChecksums.GetValueOrDefault( "menu.svo", null );

			fileSelectControlParam.LabelText = "PARAM.SFO";
			fileSelectControlParam.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "PARAM.SFO" );
			fileSelectControlParam.PatchDir = null;
			fileSelectControlParam.PatchFunction = Patcher.PatchParam;
			fileSelectControlParam.OutputChecksum = OutputChecksums.GetValueOrDefault( "PARAM.SFO", null );

			fileSelectControlTrophy.LabelText = "TROPHY.TRP";
			fileSelectControlTrophy.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "TROPHY.TRP" );
			fileSelectControlTrophy.PatchDir = "new/patches";
			fileSelectControlTrophy.PatchFunction = PatchTrophyAfterWarning;
			fileSelectControlTrophy.OutputChecksum = OutputChecksums.GetValueOrDefault( "TROPHY.TRP", null );

			FileSelectControls.Add( fileSelectControlElf );
			FileSelectControls.Add( fileSelectControlBtl );
			FileSelectControls.Add( fileSelectControlChara );
			FileSelectControls.Add( fileSelectControlChat );
			FileSelectControls.Add( fileSelectControlEffect );
			FileSelectControls.Add( fileSelectControlMenu );
			FileSelectControls.Add( fileSelectControlScenario );
			FileSelectControls.Add( fileSelectControlString );
			FileSelectControls.Add( fileSelectControlUI );

			FileSelectControls.Add( fileSelectControlParam );
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
					"Please make sure the archive containing the patch files was fully extracted and no files were moved or renamed." + Environment.NewLine +
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

		public static void PatchTrophyAfterWarning( string trophyTrp, string patchDir, string outDir, string outMd5 = null, BackgroundWorker worker = null ) {
			if ( File.Exists( trophyTrp ) ) {
				var result = MessageBox.Show(
					"TROPHY.TRP is officially signed, so the patched file will not work (and will, in fact, delete all your Tales of Vesperia trophies " +
					"if you happen to have some!) unless you can find a way to make the console not confirm that TROPHY.TRP is signed correctly. " +
					"This patching process is only provided in case such a thing becomes possible in the future. " + 
					"It is also not recommend to use a modified TROPHY.TRP file if you ever plan to sync your trophies with the official PSN servers." +
					"\n\nAre you absolutely sure you want to patch TROPHY.TRP?", "A Note on TROPHY.TRP", MessageBoxButtons.YesNo, MessageBoxIcon.Warning );
				if ( result == DialogResult.Yes ) {
					Patcher.PatchTrophy( trophyTrp, patchDir, outDir, outMd5, worker );
				} else {
					throw new PatchingException( "Cancelled by user." );
				}
			} else {
				throw new PatchingException( "File not found: " + trophyTrp );
			}
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

				TempUtil.RemoveTempFolder();

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
