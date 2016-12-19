using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Diagnostics;
using System.Runtime.InteropServices;



namespace SampleRecordingApp
{
    public partial class Form1 : Form
    {

        static string WorkingFileNameValue = "";
        static string WorkingBytesEncoded = "";
        static bool Flushed = false;

        [DllImport("kernel32.dll")]
        public static extern bool GetProcessWorkingSetSize(IntPtr proc, out int min, out int max);
        [DllImport("kernel32.dll")]
        public static extern bool SetProcessWorkingSetSize(IntPtr proc, int min, int max);


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //cmbWaveIn.DataSource = mp3Helper.getWaveINDevices();
            //cmbBitRates.DataSource = Enum.GetNames(typeof(mp3_stream.BitRate));
            //cmbSampleRates.DataSource = Enum.GetNames(typeof(mp3_stream.SampleRate));
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //listLines.DataSource = mp3Helper.getLines(cmbWaveIn.SelectedValue.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //helper.RecycleInterval = 10000;
            //helper.OnRecycle += new mp3Helper.RecycleHandler(OnRecycle);
            //helper.OnBufferWritten += new mp3Helper.BufferWrittenHandler(OnBufferWritten);
            //helper.WorkingDirectory = txtRuta.Text;
            //helper.StartRecording(cmbWaveIn.SelectedValue.ToString(), listLines.SelectedValue.ToString(),
            //    (uint)trcVolume.Value,
            //    (mp3_stream.BitRate)Enum.Parse(typeof(mp3_stream.BitRate), (string)cmbBitRates.SelectedValue),
            //    (mp3_stream.SampleRate)Enum.Parse(typeof(mp3_stream.SampleRate), (string)cmbSampleRates.SelectedValue));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //helper.StopRecording();
        }

        private void OnRecycle(String filename)
        {
            WorkingFileNameValue = filename;
            FlushMemory();
        }

        private void OnBufferWritten(uint size)
        {
            WorkingBytesEncoded = size.ToString();
            Flushed = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblFileNameValue.Text = WorkingFileNameValue;
            lblBytesEncodedValue.Text = WorkingBytesEncoded;
            if (Flushed)
            {
                if (progressBar1.Value + 1 > progressBar1.Maximum) progressBar1.Value = 0;
                progressBar1.Value += 1;
                Flushed = false;
            }
            //lblEstatus.Text = (helper.IsRecording ? "Grabando" : "Detenido");
        }

        private void FlushMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            //System.Diagnostics.Process.GetCurrentProcess().PagedMemorySize
        }


    }
}