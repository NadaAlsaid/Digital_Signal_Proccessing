using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Normalizer : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputMinRange { get; set; }
        public float InputMaxRange { get; set; }
        public Signal OutputNormalizedSignal { get; set; }

        public override void Run()
        {
            float MaxSample =InputSignal.Samples.Max();
            float MinSample = InputSignal.Samples.Min();
            List<float> OutSample = new List<float>();

            for (int sample_i =0; sample_i<InputSignal.Samples.Count; sample_i++)
            {
                float x = ((InputMaxRange - InputMinRange) *(InputSignal.Samples[sample_i]-MinSample)/(MaxSample-MinSample)) +InputMinRange;
                OutSample.Add(x);   
            }
            
            OutputNormalizedSignal=new Signal(OutSample,false);
           // throw new NotImplementedException();
        }
    }
}
