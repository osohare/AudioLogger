namespace SampleRecordingApp
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
            this.components = new System.ComponentModel.Container();
            this.btnRecord = new System.Windows.Forms.Button();
            this.cmbWaveIn = new System.Windows.Forms.ComboBox();
            this.listLines = new System.Windows.Forms.ListBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblBytesEncoded = new System.Windows.Forms.Label();
            this.lblBytesEncodedValue = new System.Windows.Forms.Label();
            this.lblFileNameValue = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.trcVolume = new System.Windows.Forms.TrackBar();
            this.cmbBitRates = new System.Windows.Forms.ComboBox();
            this.cmbSampleRates = new System.Windows.Forms.ComboBox();
            this.lblEstatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trcVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRecord
            // 
            this.btnRecord.Location = new System.Drawing.Point(127, 290);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(75, 23);
            this.btnRecord.TabIndex = 0;
            this.btnRecord.Text = "Grabar";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbWaveIn
            // 
            this.cmbWaveIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWaveIn.FormattingEnabled = true;
            this.cmbWaveIn.Items.AddRange(new object[] {
            "(Seleccione)"});
            this.cmbWaveIn.Location = new System.Drawing.Point(12, 12);
            this.cmbWaveIn.Name = "cmbWaveIn";
            this.cmbWaveIn.Size = new System.Drawing.Size(277, 21);
            this.cmbWaveIn.TabIndex = 1;
            this.cmbWaveIn.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // listLines
            // 
            this.listLines.FormattingEnabled = true;
            this.listLines.Location = new System.Drawing.Point(12, 39);
            this.listLines.Name = "listLines";
            this.listLines.Size = new System.Drawing.Size(277, 56);
            this.listLines.TabIndex = 2;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(208, 290);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Detener";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.button2_Click);
            // 
            // lblBytesEncoded
            // 
            this.lblBytesEncoded.AutoSize = true;
            this.lblBytesEncoded.Location = new System.Drawing.Point(12, 124);
            this.lblBytesEncoded.Name = "lblBytesEncoded";
            this.lblBytesEncoded.Size = new System.Drawing.Size(90, 13);
            this.lblBytesEncoded.TabIndex = 5;
            this.lblBytesEncoded.Text = "Bytes codificados";
            // 
            // lblBytesEncodedValue
            // 
            this.lblBytesEncodedValue.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblBytesEncodedValue.Location = new System.Drawing.Point(108, 124);
            this.lblBytesEncodedValue.Name = "lblBytesEncodedValue";
            this.lblBytesEncodedValue.Size = new System.Drawing.Size(180, 13);
            this.lblBytesEncodedValue.TabIndex = 6;
            this.lblBytesEncodedValue.Text = "-";
            this.lblBytesEncodedValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblFileNameValue
            // 
            this.lblFileNameValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFileNameValue.Location = new System.Drawing.Point(109, 142);
            this.lblFileNameValue.Name = "lblFileNameValue";
            this.lblFileNameValue.Size = new System.Drawing.Size(180, 13);
            this.lblFileNameValue.TabIndex = 8;
            this.lblFileNameValue.Text = "-";
            this.lblFileNameValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(12, 142);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(100, 13);
            this.lblFileName.TabIndex = 7;
            this.lblFileName.Text = "Nombre de archivo ";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(16, 158);
            this.progressBar1.Maximum = 30;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(268, 13);
            this.progressBar1.TabIndex = 9;
            // 
            // txtRuta
            // 
            this.txtRuta.Location = new System.Drawing.Point(18, 177);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(265, 20);
            this.txtRuta.TabIndex = 10;
            // 
            // trcVolume
            // 
            this.trcVolume.Location = new System.Drawing.Point(18, 203);
            this.trcVolume.Maximum = 100;
            this.trcVolume.Name = "trcVolume";
            this.trcVolume.Size = new System.Drawing.Size(265, 45);
            this.trcVolume.SmallChange = 10;
            this.trcVolume.TabIndex = 11;
            this.trcVolume.TickFrequency = 10;
            this.trcVolume.Value = 70;
            // 
            // cmbBitRates
            // 
            this.cmbBitRates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBitRates.FormattingEnabled = true;
            this.cmbBitRates.Location = new System.Drawing.Point(12, 254);
            this.cmbBitRates.Name = "cmbBitRates";
            this.cmbBitRates.Size = new System.Drawing.Size(121, 21);
            this.cmbBitRates.TabIndex = 12;
            // 
            // cmbSampleRates
            // 
            this.cmbSampleRates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSampleRates.FormattingEnabled = true;
            this.cmbSampleRates.Location = new System.Drawing.Point(162, 254);
            this.cmbSampleRates.Name = "cmbSampleRates";
            this.cmbSampleRates.Size = new System.Drawing.Size(121, 21);
            this.cmbSampleRates.TabIndex = 13;
            // 
            // lblEstatus
            // 
            this.lblEstatus.AutoSize = true;
            this.lblEstatus.Location = new System.Drawing.Point(15, 295);
            this.lblEstatus.Name = "lblEstatus";
            this.lblEstatus.Size = new System.Drawing.Size(10, 13);
            this.lblEstatus.TabIndex = 14;
            this.lblEstatus.Text = "-";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 322);
            this.Controls.Add(this.lblEstatus);
            this.Controls.Add(this.cmbSampleRates);
            this.Controls.Add(this.cmbBitRates);
            this.Controls.Add(this.trcVolume);
            this.Controls.Add(this.txtRuta);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblFileNameValue);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.lblBytesEncodedValue);
            this.Controls.Add(this.lblBytesEncoded);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.listLines);
            this.Controls.Add(this.cmbWaveIn);
            this.Controls.Add(this.btnRecord);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trcVolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.ComboBox cmbWaveIn;
        private System.Windows.Forms.ListBox listLines;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblBytesEncoded;
        private System.Windows.Forms.Label lblBytesEncodedValue;
        private System.Windows.Forms.Label lblFileNameValue;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.TrackBar trcVolume;
        private System.Windows.Forms.ComboBox cmbBitRates;
        private System.Windows.Forms.ComboBox cmbSampleRates;
        private System.Windows.Forms.Label lblEstatus;
    }
}

