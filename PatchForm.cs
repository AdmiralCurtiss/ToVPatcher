using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ToVPatcher {
	public partial class PatchForm : Form {
		public PatchForm() {
			InitializeComponent();
		}

		private void PatchForm_Load( object sender, EventArgs e ) {
			fileSelectControlString.LabelText = "string.svo";
			fileSelectControlString.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "string.svo" );
			fileSelectControlString.PatchDir = "new/patches";

			fileSelectControlScenario.LabelText = "scenario.dat";
			fileSelectControlScenario.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "scenario.dat" );
			fileSelectControlScenario.PatchDir = "new/patches/scenario";

			fileSelectControlBtl.LabelText = "btl.svo";
			fileSelectControlBtl.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "btl.svo" );
			fileSelectControlBtl.PatchDir = "new/patches/btl";

			fileSelectControlChat.LabelText = "chat.svo";
			fileSelectControlChat.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "chat.svo" );
			fileSelectControlChat.PatchDir = "new/patches/chat";

			fileSelectControlUI.LabelText = "UI.svo";
			fileSelectControlUI.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "UI.svo" );
			fileSelectControlUI.PatchDir = "new/patches";

			fileSelectControlEffect.LabelText = "effect.svo";
			fileSelectControlEffect.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "effect.svo" );
			fileSelectControlEffect.PatchDir = "new/patches/effect";

			fileSelectControlChara.LabelText = "chara.svo";
			fileSelectControlChara.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "chara.svo" );
			fileSelectControlChara.PatchDir = "new/patches/chara";

			fileSelectControlParam.LabelText = "PARAM.SFO";
			fileSelectControlParam.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "PARAM.SFO" );
			fileSelectControlChara.PatchDir = null;

			fileSelectControlTrophy.LabelText = "TROPHY.TRP";
			fileSelectControlTrophy.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "TROPHY.TRP" );
			fileSelectControlTrophy.PatchDir = "new/patches";
		}

		public delegate void PatchDelegate( string file, string patchDir, string outDir );
		private void buttonPatch_Click( object sender, EventArgs e ) {
			textBoxLog.Clear();

			string outDirPath = @"new/patched";
			var outDir = Directory.CreateDirectory( outDirPath );

			PatchOneFile( Patcher.PatchString, fileSelectControlString, outDir.FullName );
			PatchOneFile( Patcher.PatchScenario, fileSelectControlScenario, outDir.FullName );
			PatchOneFile( Patcher.PatchBtl, fileSelectControlBtl, outDir.FullName );
			PatchOneFile( Patcher.PatchChat, fileSelectControlChat, outDir.FullName );
			PatchOneFile( Patcher.PatchUI, fileSelectControlUI, outDir.FullName );
			PatchOneFile( Patcher.PatchEffect, fileSelectControlEffect, outDir.FullName );
			PatchOneFile( Patcher.PatchChara, fileSelectControlChara, outDir.FullName );
			PatchOneFile( Patcher.PatchParam, fileSelectControlParam, outDir.FullName );
			PatchOneFile( Patcher.PatchTrophy, fileSelectControlTrophy, outDir.FullName );
		}

		private void PatchOneFile( PatchDelegate func, FileSelectControl ctrl, string outDir ) {
			try {
				if ( !ctrl.Finished ) {
					textBoxLog.AppendText( "Patching " + ctrl.LabelText + "..." + Environment.NewLine );
					func( ctrl.FilePath, ctrl.PatchDir, outDir );
					textBoxLog.AppendText( "Successfully patched " + ctrl.LabelText + "!" + Environment.NewLine );
					ctrl.Finished = true;
				}
			} catch ( PatchingException ex ) {
				textBoxLog.AppendText( ex.Message + Environment.NewLine );
			}
		}
	}
}
