using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MultiplySignalByConstant : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputConstant { get; set; }
        public Signal OutputMultipliedSignal { get; set; }

        public override void Run()
        {
            List<float> OutSample = new List<float>(); 
            for(int sample_i=0; sample_i< InputSignal.Samples.Count; sample_i++)
            {
                /*if (sample_i != InputSignal.SamplesIndices[sample_i])
                    InputSignal.Samples[sample_i] = 0;*/

                float x=InputSignal.Samples[sample_i] * InputConstant;
                OutSample.Add(x);
            }

            OutputMultipliedSignal=new Signal (OutSample,false); 
            //throw new NotImplementedException();
        }
    }
}
