namespace RecordingService
{
    partial class RecordingService
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmrRecycle = new System.Windows.Forms.Timer(this.components);
            // 
            // tmrRecycle
            // 
            this.tmrRecycle.Enabled = true;
            this.tmrRecycle.Interval = 30000;
            this.tmrRecycle.Tick += new System.EventHandler(this.tmrRecycle_Tick);
            // 
            // RecordingService
            // 
            this.ServiceName = "RecordingService";

        }

        #endregion

        private System.Windows.Forms.Timer tmrRecycle;
    }
}
