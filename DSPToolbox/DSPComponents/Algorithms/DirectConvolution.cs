using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            
            List<float> Samples = new List<float>();
            List<int> SamplesIndices = new List<int>();
            int start, end;

            if (InputSignal1.SamplesIndices[0] == InputSignal2.SamplesIndices[0])
                start = InputSignal1.SamplesIndices[0];
            else
                start = InputSignal1.SamplesIndices[0] + InputSignal2.SamplesIndices[0];

            if (InputSignal1.SamplesIndices[InputSignal1.SamplesIndices.Count - 1] == InputSignal2.SamplesIndices[InputSignal2.SamplesIndices.Count - 1])
                end = InputSignal1.SamplesIndices[InputSignal1.SamplesIndices.Count - 1];
            else
                end = InputSignal1.SamplesIndices[InputSignal1.SamplesIndices.Count - 1] + InputSignal2.SamplesIndices[InputSignal2.SamplesIndices.Count - 1];


            for (int n = start,i=0; n <= end; n++,i++)
            {
                float result = 0;
                for (int k = InputSignal1.SamplesIndices[0],j=0; k <= end && j<=i; k++,j++)
                {
                    if ((i-j) < InputSignal2.Samples.Count && j< InputSignal1.Samples.Count) { 
                         float tmp = InputSignal1.Samples[j] * InputSignal2.Samples[i - j];
                         result += tmp;
                    }
                    
                }
                Samples.Add(result);
                SamplesIndices.Add(n);


            }


            OutputConvolvedSignal = new Signal(Samples, SamplesIndices, false);
            //throw new NotImplementedException();
        }
    }
}
