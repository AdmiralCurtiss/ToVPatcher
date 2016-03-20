namespace string_svo_reader
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.labelJPN = new System.Windows.Forms.Label();
            this.labelENG = new System.Windows.Forms.Label();
            this.textBoxEnglish = new System.Windows.Forms.TextBox();
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.buttonSaveString = new System.Windows.Forms.Button();
            this.buttonSaveFile = new System.Windows.Forms.Button();
            this.checkBoxAutosave = new System.Windows.Forms.CheckBox();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonImport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(13, 13);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 0;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            this.numericUpDown1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numericUpDown1_KeyUp);
            // 
            // textBoxText
            // 
            this.textBoxText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxText.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxText.Location = new System.Drawing.Point(13, 69);
            this.textBoxText.Multiline = true;
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxText.Size = new System.Drawing.Size(428, 101);
            this.textBoxText.TabIndex = 1;
            // 
            // labelJPN
            // 
            this.labelJPN.AutoSize = true;
            this.labelJPN.Location = new System.Drawing.Point(13, 50);
            this.labelJPN.Name = "labelJPN";
            this.labelJPN.Size = new System.Drawing.Size(56, 13);
            this.labelJPN.TabIndex = 2;
            this.labelJPN.Text = "Japanese:";
            // 
            // labelENG
            // 
            this.labelENG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelENG.AutoSize = true;
            this.labelENG.Location = new System.Drawing.Point(10, 173);
            this.labelENG.Name = "labelENG";
            this.labelENG.Size = new System.Drawing.Size(44, 13);
            this.labelENG.TabIndex = 3;
            this.labelENG.Text = "English:";
            // 
            // textBoxEnglish
            // 
            this.textBoxEnglish.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEnglish.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxEnglish.Location = new System.Drawing.Point(13, 189);
            this.textBoxEnglish.Multiline = true;
            this.textBoxEnglish.Name = "textBoxEnglish";
            this.textBoxEnglish.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxEnglish.Size = new System.Drawing.Size(428, 87);
            this.textBoxEnglish.TabIndex = 4;
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpenFile.Location = new System.Drawing.Point(239, 13);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(97, 23);
            this.buttonOpenFile.TabIndex = 5;
            this.buttonOpenFile.Text = "Open TSS File";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // buttonSaveString
            // 
            this.buttonSaveString.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveString.Location = new System.Drawing.Point(362, 282);
            this.buttonSaveString.Name = "buttonSaveString";
            this.buttonSaveString.Size = new System.Drawing.Size(79, 23);
            this.buttonSaveString.TabIndex = 6;
            this.buttonSaveString.Text = "Save String";
            this.buttonSaveString.UseVisualStyleBackColor = true;
            this.buttonSaveString.Click += new System.EventHandler(this.buttonSaveString_Click);
            // 
            // buttonSaveFile
            // 
            this.buttonSaveFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveFile.Location = new System.Drawing.Point(342, 13);
            this.buttonSaveFile.Name = "buttonSaveFile";
            this.buttonSaveFile.Size = new System.Drawing.Size(98, 23);
            this.buttonSaveFile.TabIndex = 7;
            this.buttonSaveFile.Text = "Save TSS File";
            this.buttonSaveFile.UseVisualStyleBackColor = true;
            this.buttonSaveFile.Click += new System.EventHandler(this.buttonSaveFile_Click);
            // 
            // checkBoxAutosave
            // 
            this.checkBoxAutosave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxAutosave.AutoSize = true;
            this.checkBoxAutosave.Location = new System.Drawing.Point(285, 286);
            this.checkBoxAutosave.Name = "checkBoxAutosave";
            this.checkBoxAutosave.Size = new System.Drawing.Size(71, 17);
            this.checkBoxAutosave.TabIndex = 8;
            this.checkBoxAutosave.Text = "Autosave";
            this.checkBoxAutosave.UseVisualStyleBackColor = true;
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExport.Location = new System.Drawing.Point(365, 39);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(75, 23);
            this.buttonExport.TabIndex = 9;
            this.buttonExport.Text = "Export Text";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonImport
            // 
            this.buttonImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonImport.Location = new System.Drawing.Point(285, 39);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(75, 23);
            this.buttonImport.TabIndex = 10;
            this.buttonImport.Text = "Import Text";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 317);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.checkBoxAutosave);
            this.Controls.Add(this.buttonSaveFile);
            this.Controls.Add(this.buttonSaveString);
            this.Controls.Add(this.buttonOpenFile);
            this.Controls.Add(this.textBoxEnglish);
            this.Controls.Add(this.labelENG);
            this.Controls.Add(this.labelJPN);
            this.Controls.Add(this.textBoxText);
            this.Controls.Add(this.numericUpDown1);
            this.Name = "Form1";
            this.Text = "STRING_DIC.SO Editor";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.TextBox textBoxText;
        private System.Windows.Forms.Label labelJPN;
        private System.Windows.Forms.Label labelENG;
        private System.Windows.Forms.TextBox textBoxEnglish;
        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.Button buttonSaveString;
        private System.Windows.Forms.Button buttonSaveFile;
        private System.Windows.Forms.CheckBox checkBoxAutosave;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonImport;
    }
}

