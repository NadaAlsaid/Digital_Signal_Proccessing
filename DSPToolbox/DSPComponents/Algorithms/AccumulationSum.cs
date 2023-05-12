using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;


namespace DSPAlgorithms.Algorithms
{
    public class AccumulationSum : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            List<float> Samples = new List<float>();
            bool Periodic = false;
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                float result = 0;
                for (int j = 0; j <= i; j++)
                    result += InputSignal.Samples[j];
                Samples.Add(result);

            }
            Periodic = InputSignal.Periodic;

            OutputSignal = new Signal(Samples, Periodic);

            //throw new NotImplementedException();
        }
    }
}
