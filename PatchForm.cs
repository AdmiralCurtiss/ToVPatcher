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
		List<FileSelectControl> FileSelectControls = new List<FileSelectControl>();

		public PatchForm() {
			InitializeComponent();
		}

		private void PatchForm_Load( object sender, EventArgs e ) {
			fileSelectControlString.LabelText = "string.svo";
			fileSelectControlString.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "string.svo" );
			fileSelectControlString.PatchDir = "new/patches";
			fileSelectControlString.PatchFunction = Patcher.PatchString;
			FileSelectControls.Add( fileSelectControlString ); 

			fileSelectControlScenario.LabelText = "scenario.dat";
			fileSelectControlScenario.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "scenario.dat" );
			fileSelectControlScenario.PatchDir = "new/patches/scenario";
			fileSelectControlScenario.PatchFunction = Patcher.PatchScenario;
			FileSelectControls.Add( fileSelectControlScenario ); 

			fileSelectControlBtl.LabelText = "btl.svo";
			fileSelectControlBtl.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "btl.svo" );
			fileSelectControlBtl.PatchDir = "new/patches/btl";
			fileSelectControlBtl.PatchFunction = Patcher.PatchBtl;
			FileSelectControls.Add( fileSelectControlBtl ); 

			fileSelectControlChat.LabelText = "chat.svo";
			fileSelectControlChat.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "chat.svo" );
			fileSelectControlChat.PatchDir = "new/patches/chat";
			fileSelectControlChat.PatchFunction = Patcher.PatchChat;
			FileSelectControls.Add( fileSelectControlChat ); 

			fileSelectControlUI.LabelText = "UI.svo";
			fileSelectControlUI.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "UI.svo" );
			fileSelectControlUI.PatchDir = "new/patches";
			fileSelectControlUI.PatchFunction = Patcher.PatchUI;
			FileSelectControls.Add( fileSelectControlUI ); 

			fileSelectControlEffect.LabelText = "effect.svo";
			fileSelectControlEffect.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "effect.svo" );
			fileSelectControlEffect.PatchDir = "new/patches/effect";
			fileSelectControlEffect.PatchFunction = Patcher.PatchEffect;
			FileSelectControls.Add( fileSelectControlEffect ); 

			fileSelectControlChara.LabelText = "chara.svo";
			fileSelectControlChara.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "chara.svo" );
			fileSelectControlChara.PatchDir = "new/patches/chara";
			fileSelectControlChara.PatchFunction = Patcher.PatchChara;
			FileSelectControls.Add( fileSelectControlChara ); 

			fileSelectControlParam.LabelText = "PARAM.SFO";
			fileSelectControlParam.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "PARAM.SFO" );
			fileSelectControlParam.PatchDir = null;
			fileSelectControlParam.PatchFunction = Patcher.PatchParam;
			FileSelectControls.Add( fileSelectControlParam ); 

			fileSelectControlTrophy.LabelText = "TROPHY.TRP";
			fileSelectControlTrophy.FilePath = Path.Combine( Directory.GetCurrentDirectory(), "TROPHY.TRP" );
			fileSelectControlTrophy.PatchDir = "new/patches";
			fileSelectControlTrophy.PatchFunction = Patcher.PatchTrophy;
			FileSelectControls.Add( fileSelectControlTrophy );
		}

		private void buttonPatch_Click( object sender, EventArgs e ) {
			buttonPatch.Enabled = false;

			string outDirPath = @"new/patched";
			var outDir = Directory.CreateDirectory( outDirPath );

			foreach ( var ctrl in FileSelectControls ) {
				ctrl.OutDir = outDir.FullName;
				ctrl.StartWorker();
			}

			buttonPatch.Enabled = true;
		}
	}
}
