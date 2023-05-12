using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Derivatives: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal FirstDerivative { get; set; }
        public Signal SecondDerivative { get; set; }

        public override void Run()
        {
            List<float> first = new List<float>();
            List<float> second = new List<float>();
            for (int i = 1; i < InputSignal.Samples.Count; i++)
            {
                float tmp = 0;
                tmp=InputSignal.Samples[i]-InputSignal.Samples[i-1];
                first.Add(tmp);
                if (i < (InputSignal.Samples.Count - 1))
                {
                    tmp = InputSignal.Samples[i - 1] + InputSignal.Samples[i + 1] - 2 * InputSignal.Samples[i];
                    second.Add(tmp);
                }
                /*else if(i == (InputSignal.Samples.Count -1))
                {
                    second.Add(0);

                }*/
                
              
            }
            FirstDerivative = new Signal(first, false);
            SecondDerivative = new Signal(second, false);
           //throw new NotImplementedException();
        }
    }
}
