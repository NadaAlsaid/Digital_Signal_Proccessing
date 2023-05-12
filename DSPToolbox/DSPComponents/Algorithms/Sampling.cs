﻿using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Sampling : Algorithm
    {
        public int L { get; set; } //upsampling factor
        public int M { get; set; } //downsampling factor
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }




        public override void Run()
        {
            // throw new NotImplementedException();
            Signal filterOutput,filterInput;
            List<float> tempSamples=new List<float>();
            List<int> samplesIndecies=new List<int>();
            int sampleIndex;

            FIR lowpass_filter = new FIR();
            lowpass_filter.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
            lowpass_filter.InputFS = 8000;
            lowpass_filter.InputStopBandAttenuation = 50;
            lowpass_filter.InputCutOffFrequency = 1500;
            lowpass_filter.InputTransitionBand = 500;


            if (L==0 && M!=0) //Down Sampling
            {
                lowpass_filter.InputTimeDomainSignal = InputSignal;
                lowpass_filter.Run();
                filterOutput = lowpass_filter.OutputYn;
                sampleIndex = filterOutput.SamplesIndices[0];

                for(int i=0;i<filterOutput.Samples.Count;i+=M)
                {
                    samplesIndecies.Add(sampleIndex);
                    tempSamples.Add(filterOutput.Samples[i]);
                    sampleIndex++;

                }
                OutputSignal = new Signal(tempSamples, samplesIndecies, false);
            }
            else if(L!=0 && M==0) //Up Sampling
            {
                sampleIndex = InputSignal.SamplesIndices[0];

                for (int i = 0; i < InputSignal.Samples.Count; i ++)
                {
                    samplesIndecies.Add(sampleIndex);
                    tempSamples.Add(InputSignal.Samples[i]);
                    sampleIndex++;
                    if (i == InputSignal.Samples.Count - 1)
                        break;
                    for(int j = 0; j < L-1; j++) 
                    {
                        samplesIndecies.Add(sampleIndex);
                        tempSamples.Add(0.0f);
                        sampleIndex++;

                    }

                }
                filterInput = new Signal(tempSamples, samplesIndecies, false);
                lowpass_filter.InputTimeDomainSignal = filterInput;
                lowpass_filter.Run();
                filterOutput = lowpass_filter.OutputYn;
                OutputSignal = filterOutput;


            }
            else if(L != 0 && M != 0) // Up then Down
            {
                sampleIndex = InputSignal.SamplesIndices[0];

                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {
                    samplesIndecies.Add(sampleIndex);
                    tempSamples.Add(InputSignal.Samples[i]);
                    sampleIndex++;
                    if (i == InputSignal.Samples.Count - 1)
                        break;
                    for (int j = 0; j < L - 1; j++)
                    {
                        samplesIndecies.Add(sampleIndex);
                        tempSamples.Add(0);
                        sampleIndex++;

                    }

                }
                filterInput = new Signal(tempSamples, samplesIndecies, false);
                lowpass_filter.InputTimeDomainSignal = filterInput;
                lowpass_filter.Run();
                filterOutput = lowpass_filter.OutputYn;
                sampleIndex = filterOutput.SamplesIndices[0];
                samplesIndecies = new List<int>();
                tempSamples = new List<float>();


                for (int i = 0; i < filterOutput.Samples.Count; i += M)
                {
                    samplesIndecies.Add(sampleIndex);
                    tempSamples.Add(filterOutput.Samples[i]);
                    sampleIndex++;

                }
                OutputSignal = new Signal(tempSamples, samplesIndecies, false);

            }
            

        }
    }

}