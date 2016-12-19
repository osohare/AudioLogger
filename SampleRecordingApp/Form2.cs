using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using SampleRecordingApp.AdminServices;

namespace SampleRecordingApp
{
    public partial class Form2 : Form
    {
        private AdminServicesService service = new AdminServicesService();
        private AdminServices.KLogger[] loggers;

        public Form2()
        {
            InitializeComponent();
        }

        private KLogger CurrentLogger
        {
            get { return (KLogger)cmbLoggers.SelectedItem;  }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            cmbBitRates.DataSource = Enum.GetNames(typeof(AdminServices.BitRate));
            cmbSampleRates.DataSource = Enum.GetNames(typeof(AdminServices.SampleRate));
            cmbWaveIn.DataSource = service.getDevices();
            refreshLoggers();
        }

        private void refreshLoggers()
        {
            loggers = service.getConfig();
            cmbLoggers.DataSource = loggers;
            cmbLoggers.DisplayMember = "station";
            cmbLoggers.ValueMember = "stationID";
            if (loggers.Length > 0)
                setLogger(loggers[0]);
        }

        private void setLogger(KLogger k)
        {
            cmbLoggers.SelectedItem = k;
            cmbWaveIn.SelectedItem = k.deviceName;
            listLines.SelectedItem = k.lineName;
            txtRuta.Text = k.workingDirectory;
            trcVolume.Value = (int)k.volume;
            cmbBitRates.SelectedItem = k.bitRate.ToString();
            cmbSampleRates.SelectedItem = k.sampleRate.ToString();
            txtStation.Text = k.station;
            lblStationID.Text = k.stationID.ToString();
            chkEnabled.Checked = k.enabled;
            chkRecording.Checked = k.isRecording;
            //k.recycleInterval
        }

        private KLogger getLogger()
        {
            KLogger k = CurrentLogger;
            k.deviceName = cmbWaveIn.SelectedItem.ToString();
            k.lineName = listLines.SelectedItem.ToString();
            k.workingDirectory = txtRuta.Text;
            k.volume = (uint)trcVolume.Value;
            k.bitRate = (AdminServices.BitRate)Enum.Parse(typeof(AdminServices.BitRate), (string)cmbBitRates.SelectedValue);
            k.sampleRate = (AdminServices.SampleRate)Enum.Parse(typeof(AdminServices.SampleRate), (string)cmbSampleRates.SelectedValue);
            k.station = txtStation.Text;
            k.enabled = chkEnabled.Checked;
            //k.recycleInterval
            return k;
        }

        private void cmbLoggers_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbWaveIn.SelectedItem = CurrentLogger.deviceName;
        }

        private void cmbWaveIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            listLines.DataSource = service.getLines(cmbWaveIn.SelectedValue.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            service.setConfig(getLogger());
            refreshLoggers();
        }

        private void button2_Click(object sender, EventArgs e)
        {   
            service.startLogger((int)CurrentLogger.stationID);
            Thread.Sleep(1000);
            refreshLoggers();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            service.stopLogger((int)CurrentLogger.stationID);
            Thread.Sleep(1000);
            refreshLoggers();
        }

    }
}