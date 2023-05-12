using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DCT: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            //throw new NotImplementedException();
            double sum;
            List<float> Samples = new List<float>();
            for (int k = 0; k < InputSignal.Samples.Count; k++)
            {
                sum = 0;
                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {
                    sum += InputSignal.Samples[i] *
                        Math.Cos(
                            (Math.PI / (4 * InputSignal.Samples.Count)) * (2 * i - 1) * (2 * k - 1)
                            );

                }

                Samples.Add((float)(sum * Math.Sqrt(2.0 / InputSignal.Samples.Count)));
            }
            OutputSignal = new Signal(Samples, InputSignal.Periodic);
        }
    }
}
