using System;
using System.Threading;
using System.Threading.Tasks;

using NAudio.Wave;
using NAudio.Utils;
using NAudio.Wave.SampleProviders;

namespace HueController
{
    class Program
    {

        private static Light _bedroom;

        static void Main(string[] args)
        {

            HueTalker.init("thefutureishere");
            _bedroom = Light.getLight(3);
            _bedroom.state.on = true;
            _bedroom.updateState();
            /*HueTalker.username = "thefutureishere";
            
            if (null != _bedroom)
            {
                _bedroom.state.bri = 0;
                Thread thread = new Thread(new ThreadStart(startRecording));
                thread.Start();
                while (!thread.IsAlive) ;
                thread.Join();
            }*/


        }

        static void WaveData(object sender, WaveInEventArgs args)
        {

            Light bedroom = null;

            lock (_bedroom)
            {
                bedroom = _bedroom;
            }

            Int64 avgMagnitude = 0;
            Int16 min = Int16.MaxValue, max = Int16.MinValue;
            // ComplexF[] magnitudes = new ComplexF[1024];
            //Console.WriteLine(args.BytesRecorded);
            
            for (int i = 0; i < args.BytesRecorded; i += 2)
            {
                // magnitudes[i].Re = (float)args.Buffer[i] / 256;
                Int16 value = (short)(args.Buffer[i] | (args.Buffer[i + 1] << 8));
                min = value < min ? value : min;
                max = value > max ? value : max;
                avgMagnitude += value;
            }
            uint dist = (uint)(Math.Abs(min) + max);
            Console.WriteLine(min + ", " + max + ", " + dist);
            bedroom.state.on = true;
            bedroom.state.hue = dist;
            bedroom.state.colormode = "hs";
            bedroom.updateState();
            /*
            float first = magnitudes[1].Re;
            Fourier.FFT(magnitudes, FourierDirection.Forward);
            Console.WriteLine(first + ", " + magnitudes[2].Im);
             */
            
        }

        static void startRecording()
        {
            WaveInEvent record = new WaveInEvent();
            record.DataAvailable += WaveData;
            record.DeviceNumber = 0;
            record.WaveFormat = new WaveFormat(8000, 1);
            Console.WriteLine(record.WaveFormat.BitsPerSample);
            record.StartRecording();
            while (true) ;
        }


    }
}
