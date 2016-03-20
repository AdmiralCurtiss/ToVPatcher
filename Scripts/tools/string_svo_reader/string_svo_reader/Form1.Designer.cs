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
            this.textBoxPointerENG = new System.Windows.Forms.TextBox();
            this.textBoxtextBoxPointerJPN = new System.Windows.Forms.TextBox();
            this.textBoxPointerLocJPN = new System.Windows.Forms.TextBox();
            this.textBoxPointerLocENG = new System.Windows.Forms.TextBox();
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
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
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
            this.textBoxText.ReadOnly = true;
            this.textBoxText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxText.Size = new System.Drawing.Size(428, 87);
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
            this.labelENG.Location = new System.Drawing.Point(13, 172);
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
            this.textBoxEnglish.Location = new System.Drawing.Point(13, 188);
            this.textBoxEnglish.Multiline = true;
            this.textBoxEnglish.Name = "textBoxEnglish";
            this.textBoxEnglish.ReadOnly = true;
            this.textBoxEnglish.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxEnglish.Size = new System.Drawing.Size(428, 87);
            this.textBoxEnglish.TabIndex = 4;
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpenFile.Location = new System.Drawing.Point(344, 13);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(97, 23);
            this.buttonOpenFile.TabIndex = 5;
            this.buttonOpenFile.Text = "Open string.svo";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // textBoxPointerENG
            // 
            this.textBoxPointerENG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPointerENG.Location = new System.Drawing.Point(375, 162);
            this.textBoxPointerENG.Name = "textBoxPointerENG";
            this.textBoxPointerENG.ReadOnly = true;
            this.textBoxPointerENG.Size = new System.Drawing.Size(66, 20);
            this.textBoxPointerENG.TabIndex = 6;
            // 
            // textBoxtextBoxPointerJPN
            // 
            this.textBoxtextBoxPointerJPN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxtextBoxPointerJPN.Location = new System.Drawing.Point(375, 43);
            this.textBoxtextBoxPointerJPN.Name = "textBoxtextBoxPointerJPN";
            this.textBoxtextBoxPointerJPN.ReadOnly = true;
            this.textBoxtextBoxPointerJPN.Size = new System.Drawing.Size(66, 20);
            this.textBoxtextBoxPointerJPN.TabIndex = 7;
            // 
            // textBoxPointerLocJPN
            // 
            this.textBoxPointerLocJPN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPointerLocJPN.Location = new System.Drawing.Point(303, 43);
            this.textBoxPointerLocJPN.Name = "textBoxPointerLocJPN";
            this.textBoxPointerLocJPN.ReadOnly = true;
            this.textBoxPointerLocJPN.Size = new System.Drawing.Size(66, 20);
            this.textBoxPointerLocJPN.TabIndex = 8;
            // 
            // textBoxPointerLocENG
            // 
            this.textBoxPointerLocENG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPointerLocENG.Location = new System.Drawing.Point(303, 162);
            this.textBoxPointerLocENG.Name = "textBoxPointerLocENG";
            this.textBoxPointerLocENG.ReadOnly = true;
            this.textBoxPointerLocENG.Size = new System.Drawing.Size(66, 20);
            this.textBoxPointerLocENG.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 295);
            this.Controls.Add(this.textBoxPointerLocENG);
            this.Controls.Add(this.textBoxPointerLocJPN);
            this.Controls.Add(this.textBoxtextBoxPointerJPN);
            this.Controls.Add(this.textBoxPointerENG);
            this.Controls.Add(this.buttonOpenFile);
            this.Controls.Add(this.textBoxEnglish);
            this.Controls.Add(this.labelENG);
            this.Controls.Add(this.labelJPN);
            this.Controls.Add(this.textBoxText);
            this.Controls.Add(this.numericUpDown1);
            this.Name = "Form1";
            this.Text = "string.svo Reader";
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
        private System.Windows.Forms.TextBox textBoxPointerENG;
        private System.Windows.Forms.TextBox textBoxtextBoxPointerJPN;
        private System.Windows.Forms.TextBox textBoxPointerLocJPN;
        private System.Windows.Forms.TextBox textBoxPointerLocENG;
    }
}

