﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AudioLogger.Recorder.WindowsService
{
    [RunInstaller(true)]
    public partial class RecordingServiceInstaller : System.Configuration.Install.Installer
    {
        public RecordingServiceInstaller()
        {
            InitializeComponent();
        }
    }
}