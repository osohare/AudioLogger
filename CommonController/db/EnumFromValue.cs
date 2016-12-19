using System;
using System.Collections.Generic;
using System.Text;

using mp3_stream;

namespace CommonController.db
{
    public class EnumFromValue
    {
        public static BitRate FromBitRate(int bitRate)
        {
            switch (bitRate)
            {
                case 0:
                    return BitRate.br_default;
                case 8:
                    return BitRate.br8;
                case 16:
                    return BitRate.br16;
                case 24:
                    return BitRate.br24;
                case 32:
                    return BitRate.br32;
                case 64:
                    return BitRate.br64;
                case 128:
                    return BitRate.br128;
                case 256:
                    return BitRate.br256;
                case 320:
                    return BitRate.br320;
            }
            return BitRate.br_default;
        }
         
        public static SampleRate FromSampleRate(int sampleRate)
        {
            switch (sampleRate)
            {
                case 0:
                    return SampleRate.sr_default;
                case 8000:
                    return SampleRate.sr8000;
                case 11025:
                    return SampleRate.sr11025;
                case 22050:
                    return SampleRate.sr22050;
                case 44100:
                    return SampleRate.sr44100;
            }
            return SampleRate.sr_default;
        }

    }
}
