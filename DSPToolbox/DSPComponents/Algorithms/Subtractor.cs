using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Subtractor : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputSignal { get; set; }

        /// <summary>
        /// To do: Subtract Signal2 from Signal1 
        /// i.e OutSig = Sig1 - Sig2 
        /// </summary>
        public override void Run()
        {
            int MaxNumOfSamples;
            List <float> OutSample = new List<float>();

            if(InputSignal1.Samples.Count > InputSignal2.Samples.Count)
                MaxNumOfSamples = InputSignal1.Samples.Count;
            else
                MaxNumOfSamples= InputSignal2.Samples.Count;

            for(int sample_i=0; sample_i< MaxNumOfSamples; sample_i++)
            {
                if (sample_i != InputSignal1.SamplesIndices[sample_i])
                    InputSignal1.Samples[sample_i] = 0;

                if (sample_i != InputSignal2.SamplesIndices[sample_i])
                    InputSignal2.Samples[sample_i] = 0;
                
                float x = InputSignal1.Samples[sample_i]- InputSignal2.Samples[sample_i];
                OutSample.Add(x);
            }

            OutputSignal=new Signal(OutSample,false);
            //throw new NotImplementedException();
        }
    }
}