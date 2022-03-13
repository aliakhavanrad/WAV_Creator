using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAV_Creator
{

    public class WAVHeader
    {
        private List<byte> header;

        const string RIFF = "RIFF";
        const string WAVE = "WAVE";
        const string FMT = "fmt ";
        const string DATA = "data";


        // Format Header                                                    
        static char[] fmt_header = FMT.ToArray();                           // Contains "fmt " (includes trailing space)
        static int fmt_chunk_size = 16;                                     // Should be 16 for PCM
        static short audio_format = 1;                                      // Should be 1 for PCM. 3 for IEEE Float
        const short Default_Num_Channels = 1;
        const int Default_Sample_Rate = 44100;

        //static int byte_rate = sample_rate * num_channels * bit_depth / 8;  // Number of bytes per second. sample_rate * num_channels * Bytes Per Sample
        static short sample_alignment = 2;                                  // num_channels * Bytes Per Sample
        const short Default_Bit_Depth = 16;                                        // Number of bits per sample


        readonly short NumberOfChannels;
        public readonly int SampleRate;
        readonly short BitDepth;

        public WAVHeader(short numberOfChannels = Default_Num_Channels, int sampleRate = Default_Sample_Rate, short bitDepth = Default_Bit_Depth)
        {
            NumberOfChannels = numberOfChannels;
            SampleRate = sampleRate;
            BitDepth = bitDepth;


            header = new List<byte>();

            header.AddBytes(RIFF.ToByteArray())
                   .AddBytes(new byte[4])
                   .AddBytes(WAVE.ToByteArray())
                   .AddBytes(FMT.ToByteArray())
                   .AddBytes(BitConverter.GetBytes(fmt_chunk_size))
                   .AddBytes(BitConverter.GetBytes(audio_format))
                   .AddBytes(BitConverter.GetBytes(numberOfChannels))
                   .AddBytes(BitConverter.GetBytes(sampleRate))
                   .AddBytes(BitConverter.GetBytes(sampleRate * numberOfChannels * bitDepth / 8))
                   .AddBytes(BitConverter.GetBytes(sample_alignment))
                   .AddBytes(BitConverter.GetBytes(bitDepth))

                   .AddBytes(DATA.ToByteArray())
                   .AddBytes(new byte[4]);
        }

        public List<byte> GetContent()
        {
            return header;
        }


    }
}
