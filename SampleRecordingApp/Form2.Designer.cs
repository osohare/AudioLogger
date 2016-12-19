namespace SampleRecordingApp
{
    partial class Form2
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
            this.button1 = new System.Windows.Forms.Button();
            this.cmbSampleRates = new System.Windows.Forms.ComboBox();
            this.cmbBitRates = new System.Windows.Forms.ComboBox();
            this.trcVolume = new System.Windows.Forms.TrackBar();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.listLines = new System.Windows.Forms.ListBox();
            this.cmbWaveIn = new System.Windows.Forms.ComboBox();
            this.cmbLoggers = new System.Windows.Forms.ComboBox();
            this.txtStation = new System.Windows.Forms.TextBox();
            this.lblStationID = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.chkRecording = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trcVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(18, 311);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save Config";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbSampleRates
            // 
            this.cmbSampleRates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSampleRates.FormattingEnabled = true;
            this.cmbSampleRates.Location = new System.Drawing.Point(162, 272);
            this.cmbSampleRates.Name = "cmbSampleRates";
            this.cmbSampleRates.Size = new System.Drawing.Size(121, 21);
            this.cmbSampleRates.TabIndex = 26;
            // 
            // cmbBitRates
            // 
            this.cmbBitRates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBitRates.FormattingEnabled = true;
            this.cmbBitRates.Location = new System.Drawing.Point(12, 272);
            this.cmbBitRates.Name = "cmbBitRates";
            this.cmbBitRates.Size = new System.Drawing.Size(121, 21);
            this.cmbBitRates.TabIndex = 25;
            // 
            // trcVolume
            // 
            this.trcVolume.Location = new System.Drawing.Point(18, 221);
            this.trcVolume.Maximum = 100;
            this.trcVolume.Name = "trcVolume";
            this.trcVolume.Size = new System.Drawing.Size(265, 45);
            this.trcVolume.SmallChange = 10;
            this.trcVolume.TabIndex = 24;
            this.trcVolume.TickFrequency = 10;
            this.trcVolume.Value = 70;
            // 
            // txtRuta
            // 
            this.txtRuta.Location = new System.Drawing.Point(18, 157);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(265, 20);
            this.txtRuta.TabIndex = 23;
            // 
            // listLines
            // 
            this.listLines.FormattingEnabled = true;
            this.listLines.Location = new System.Drawing.Point(12, 95);
            this.listLines.Name = "listLines";
            this.listLines.Size = new System.Drawing.Size(277, 56);
            this.listLines.TabIndex = 17;
            // 
            // cmbWaveIn
            // 
            this.cmbWaveIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWaveIn.FormattingEnabled = true;
            this.cmbWaveIn.Items.AddRange(new object[] {
            "(Seleccione)"});
            this.cmbWaveIn.Location = new System.Drawing.Point(12, 68);
            this.cmbWaveIn.Name = "cmbWaveIn";
            this.cmbWaveIn.Size = new System.Drawing.Size(277, 21);
            this.cmbWaveIn.TabIndex = 16;
            this.cmbWaveIn.SelectedIndexChanged += new System.EventHandler(this.cmbWaveIn_SelectedIndexChanged);
            // 
            // cmbLoggers
            // 
            this.cmbLoggers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLoggers.FormattingEnabled = true;
            this.cmbLoggers.Items.AddRange(new object[] {
            "(Seleccione)"});
            this.cmbLoggers.Location = new System.Drawing.Point(12, 12);
            this.cmbLoggers.Name = "cmbLoggers";
            this.cmbLoggers.Size = new System.Drawing.Size(277, 21);
            this.cmbLoggers.TabIndex = 27;
            this.cmbLoggers.SelectedIndexChanged += new System.EventHandler(this.cmbLoggers_SelectedIndexChanged);
            // 
            // txtStation
            // 
            this.txtStation.Location = new System.Drawing.Point(87, 39);
            this.txtStation.Name = "txtStation";
            this.txtStation.Size = new System.Drawing.Size(202, 20);
            this.txtStation.TabIndex = 28;
            // 
            // lblStationID
            // 
            this.lblStationID.AutoSize = true;
            this.lblStationID.Location = new System.Drawing.Point(71, 42);
            this.lblStationID.Name = "lblStationID";
            this.lblStationID.Size = new System.Drawing.Size(10, 13);
            this.lblStationID.TabIndex = 29;
            this.lblStationID.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Station";
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Location = new System.Drawing.Point(38, 198);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(65, 17);
            this.chkEnabled.TabIndex = 31;
            this.chkEnabled.Text = "Enabled";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // chkRecording
            // 
            this.chkRecording.AutoSize = true;
            this.chkRecording.Enabled = false;
            this.chkRecording.Location = new System.Drawing.Point(185, 198);
            this.chkRecording.Name = "chkRecording";
            this.chkRecording.Size = new System.Drawing.Size(75, 17);
            this.chkRecording.TabIndex = 32;
            this.chkRecording.Text = "Recording";
            this.chkRecording.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(115, 311);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 33;
            this.button2.Text = "Start Logger";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(208, 311);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 34;
            this.button3.Text = "Stop Logger";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 356);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.chkRecording);
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblStationID);
            this.Controls.Add(this.txtStation);
            this.Controls.Add(this.cmbLoggers);
            this.Controls.Add(this.cmbSampleRates);
            this.Controls.Add(this.cmbBitRates);
            this.Controls.Add(this.trcVolume);
            this.Controls.Add(this.txtRuta);
            this.Controls.Add(this.listLines);
            this.Controls.Add(this.cmbWaveIn);
            this.Controls.Add(this.button1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trcVolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbSampleRates;
        private System.Windows.Forms.ComboBox cmbBitRates;
        private System.Windows.Forms.TrackBar trcVolume;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.ListBox listLines;
        private System.Windows.Forms.ComboBox cmbWaveIn;
        private System.Windows.Forms.ComboBox cmbLoggers;
        private System.Windows.Forms.TextBox txtStation;
        private System.Windows.Forms.Label lblStationID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.CheckBox chkRecording;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}