using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAV_Creator
{
    public class WAV
    {
        private List<byte> content;

        private readonly WAVHeader header;

        public WAV()
        {

            content = new List<byte>();

            header = new WAVHeader();
            content.AddRange(header.GetContent());
        }

        public WAV(short numberOfChannels, int sampleRate, short bitDepth)
        {

            content = new List<byte>();

            var header = new WAVHeader(numberOfChannels, sampleRate, bitDepth);
            content.AddRange(header.GetContent());
        }

        public void AddSample(short sample)
        {
            byte[] sampleBytes = BitConverter.GetBytes(sample);
            content.AddBytes(sampleBytes);
        }

        public void Export(string filePath)
        {
            if (!Path.GetExtension(filePath).ToLower().Contains("wav"))
            {
                throw new Exception("File extension need to be WAV");
            }


            var fileSize = content.Count;

            var subtract_8_from_fileSize = fileSize - 8;
            var subtract_8_from_fileSize_ByteArray = BitConverter.GetBytes(subtract_8_from_fileSize);

            for (int i = 4; i < 8; i++)
            {
                content[i] = subtract_8_from_fileSize_ByteArray[i - 4];
            }



            var subtract_44_from_fileSize = fileSize - 44;
            var subtract_44_from_fileSize_ByteArray = BitConverter.GetBytes(subtract_44_from_fileSize);

            for (int i = 40; i < 44; i++)
            {
                content[i] = subtract_44_from_fileSize_ByteArray[i - 40];
            }


            File.WriteAllBytes(filePath, content.ToArray());

        }

        public void Create(int frequency, int attackAmplitude, double duration, string folderPath)
        {
            for (int i = 0; i < duration * header.SampleRate; i++)
            {
                var amplitude = attackAmplitude - attackAmplitude * i / (duration * header.SampleRate);

                double theta = frequency * ((double)i / header.SampleRate) * (2 * Math.PI);

                short sampleValue = (short)(amplitude * (Math.Sin(theta)));

                AddSample(sampleValue);
            }

            Export($"{folderPath}\\{frequency}.wav");
        }

    }
}
