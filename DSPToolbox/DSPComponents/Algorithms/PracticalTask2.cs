﻿using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace DSPAlgorithms.Algorithms
{
    public class PracticalTask2 : Algorithm
    {
        public String SignalPath { get; set; }
        public float Fs { get; set; }
        public float miniF { get; set; }
        public float maxF { get; set; }
        public float newFs { get; set; }
        public int L { get; set; } //upsampling factor
        public int M { get; set; } //downsampling factor
        public Signal OutputFreqDomainSignal { get; set; }

        public override void Run()
        {
            Signal InputSignal = LoadSignal(SignalPath);
            FIR filter = new FIR();
            filter.InputTimeDomainSignal = InputSignal;
            filter.InputFS= Fs;
            filter.InputStopBandAttenuation = 50;
            filter.InputTransitionBand = 500;
            filter.InputF1 = miniF;
            filter.InputF2 = maxF;
            filter.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.BAND_PASS;
            /*if (miniF==0)
            {
                filter.InputCutOffFrequency = maxF;
                filter.InputFilterType= DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
            }
            else
            {
                filter.InputF1 = miniF;
                filter.InputF2 = maxF;
                filter.InputFilterType= DSPAlgorithms.DataStructures.FILTER_TYPES.BAND_PASS;
            }*/
            filter.Run();
            InputSignal = filter.OutputYn;
            timeDomainInFile("D:/5th semester/DSP/DSP_tasks/DSPToolbox/DSPComponentsUnitTest/OutputSignals/filteredSignal.ds", InputSignal);
             
            bool flag = false;
            if (newFs >= 2*maxF)
            {
                flag = true;
                Sampling resampling = new Sampling();
                resampling.InputSignal = InputSignal;
                resampling.M = M;
                resampling.L = L;
                resampling.Run();
                InputSignal = resampling.OutputSignal;
                timeDomainInFile("D:/5th semester/DSP/DSP_tasks/DSPToolbox/DSPComponentsUnitTest/OutputSignals/resamplingSignal.ds", InputSignal);

            }
            DC_Component dC_Component = new DC_Component();
            dC_Component.InputSignal = InputSignal;
            dC_Component.Run();
            InputSignal=dC_Component.OutputSignal;
            timeDomainInFile("D:/5th semester/DSP/DSP_tasks/DSPToolbox/DSPComponentsUnitTest/OutputSignals/removedDCSignal.ds", InputSignal);
            
            Normalizer normalizer = new Normalizer();
            normalizer.InputSignal = InputSignal;
            normalizer.InputMinRange = -1;
            normalizer.InputMaxRange = 1;
            normalizer.Run();
            InputSignal = normalizer.OutputNormalizedSignal;
            timeDomainInFile("D:/5th semester/DSP/DSP_tasks/DSPToolbox/DSPComponentsUnitTest/OutputSignals/normalizedSignal.ds", InputSignal);

            DiscreteFourierTransform discreteFourierTransform = new DiscreteFourierTransform();
            discreteFourierTransform.InputTimeDomainSignal = InputSignal;
            if(flag)
                discreteFourierTransform.InputSamplingFrequency = newFs;
            else
                discreteFourierTransform.InputSamplingFrequency = Fs;
            discreteFourierTransform.Run();
            OutputFreqDomainSignal=discreteFourierTransform.OutputFreqDomainSignal;
            frequencyDomainInFile("D:/5th semester/DSP/DSP_tasks/DSPToolbox/DSPComponentsUnitTest/OutputSignals/OutputFreqDomainSignal.ds", OutputFreqDomainSignal);





        }

        public Signal LoadSignal(string filePath)
        {
            Stream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var sr = new StreamReader(stream);

            var sigType = byte.Parse(sr.ReadLine());
            var isPeriodic = byte.Parse(sr.ReadLine());
            long N1 = long.Parse(sr.ReadLine());

            List<float> SigSamples = new List<float>(unchecked((int)N1));
            List<int> SigIndices = new List<int>(unchecked((int)N1));
            List<float> SigFreq = new List<float>(unchecked((int)N1));
            List<float> SigFreqAmp = new List<float>(unchecked((int)N1));
            List<float> SigPhaseShift = new List<float>(unchecked((int)N1));

            if (sigType == 1)
            {
                SigSamples = null;
                SigIndices = null;
            }

            for (int i = 0; i < N1; i++)
            {
                if (sigType == 0 || sigType == 2)
                {
                    var timeIndex_SampleAmplitude = sr.ReadLine().Split();
                    SigIndices.Add(int.Parse(timeIndex_SampleAmplitude[0]));
                    SigSamples.Add(float.Parse(timeIndex_SampleAmplitude[1]));
                }
                else
                {
                    var Freq_Amp_PhaseShift = sr.ReadLine().Split();
                    SigFreq.Add(float.Parse(Freq_Amp_PhaseShift[0]));
                    SigFreqAmp.Add(float.Parse(Freq_Amp_PhaseShift[1]));
                    SigPhaseShift.Add(float.Parse(Freq_Amp_PhaseShift[2]));
                }
            }

            if (!sr.EndOfStream)
            {
                long N2 = long.Parse(sr.ReadLine());

                for (int i = 0; i < N2; i++)
                {
                    var Freq_Amp_PhaseShift = sr.ReadLine().Split();
                    SigFreq.Add(float.Parse(Freq_Amp_PhaseShift[0]));
                    SigFreqAmp.Add(float.Parse(Freq_Amp_PhaseShift[1]));
                    SigPhaseShift.Add(float.Parse(Freq_Amp_PhaseShift[2]));
                }
            }

            stream.Close();
            return new Signal(SigSamples, SigIndices, isPeriodic == 1, SigFreq, SigFreqAmp, SigPhaseShift);
        }

        public void timeDomainInFile(String fullPath,Signal timeDomainSignal)
        {
            using (StreamWriter sw = new StreamWriter(fullPath))
            {
                sw.WriteLine(0);
                int Periodic;
                if (timeDomainSignal.Periodic == true)
                    Periodic = 1;
                else
                    Periodic = 0;
                sw.WriteLine(Periodic);
                sw.WriteLine(timeDomainSignal.Samples.Count);
                for(int i = 0;i< timeDomainSignal.Samples.Count; i++)
                {
                    sw.Write(timeDomainSignal.SamplesIndices[i]);
                    sw.Write(" ");
                    sw.WriteLine(timeDomainSignal.Samples[i]);
                    
                }
            }
        }

        public void frequencyDomainInFile(String fullPath, Signal frequencyDomainSignal)
        {
            using (StreamWriter sw = new StreamWriter(fullPath))
            {
                sw.WriteLine(1);
                int Periodic;
                if (frequencyDomainSignal.Periodic == true)
                    Periodic = 1;
                else
                    Periodic = 0;
                sw.WriteLine(Periodic);
                sw.WriteLine(frequencyDomainSignal.Frequencies.Count);
                for (int i = 0; i < frequencyDomainSignal.Frequencies.Count; i++)
                {
                    sw.Write(frequencyDomainSignal.Frequencies[i]);
                    sw.Write(" ");
                    sw.Write(frequencyDomainSignal.FrequenciesAmplitudes[i]);
                    sw.Write(" ");
                    sw.WriteLine(frequencyDomainSignal.FrequenciesPhaseShifts[i]);


                }
            }
        }
    }
}
