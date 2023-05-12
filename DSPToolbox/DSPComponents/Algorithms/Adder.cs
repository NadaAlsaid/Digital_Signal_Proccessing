using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Adder : Algorithm
    {
        public List<Signal> InputSignals { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            List<int> NumOfSamples = new List<int> { 0 };
            List <float> OutSamples = new List<float>();

            for (int i=0; i<InputSignals.Count; i++)
            {
                NumOfSamples.Add(InputSignals[i].Samples.Count);
            }
            int MaxNumOfSamples =NumOfSamples.Max();

            for(int sample_i =0; sample_i< MaxNumOfSamples; sample_i++) 
            {
                float x=0;
                for (int signal_i =0; signal_i< InputSignals.Count; signal_i++)
                {
                    if (sample_i != InputSignals[signal_i].SamplesIndices[sample_i])
                        InputSignals[signal_i].Samples[sample_i] = 0;
                    
                         x += InputSignals[signal_i].Samples[sample_i];

                }
                OutSamples.Add(x);  

            }

            OutputSignal=new Signal(OutSamples, false);
           // throw new NotImplementedException();
        }
    }
}