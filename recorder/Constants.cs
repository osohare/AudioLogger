using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioLogger.Recorder
{
    /// <summary>
    /// Available recording rates
    /// </summary>
    public enum RecordingSampleRate : int
    {
        SAMPLE_RATE_08000 = 8000,
        SAMPLE_RATE_11025 = 11025,
        SAMPLE_RATE_22050 = 22050,
        SAMPLE_RATE_44100 = 44100      
    };

    /// <summary>
    /// Available recording bitrates
    /// </summary>
    public enum RecordingBitRate : int
    {
        BITRATE_8 = 8,
        BITRATE_16 = 16,
        BITRATE_24 = 24,
        BITRATE_32 = 32,
        BITRATE_64 = 64,
        BITRATE_128 = 128,
        BITRATE_256 = 256,
        BITRATE_320 = 320
    };

    /// <summary>
    /// Available recording channels
    /// </summary>
    public enum RecordingChannels : int
    {
        CHANNELS_MONO = 1,
        CHANNELS_STEREO = 2
    }
}
