using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Shifter : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int ShiftingValue { get; set; }
        public Signal OutputShiftedSignal { get; set; }

        public override void Run()
        {
            // throw new NotImplementedException();

            List<float> Samples = new List<float>();
            List<int> SamplesIndeces = new List<int>();
            bool Periodic = false;
            if (InputSignal.Periodic != true)

                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {
                    Samples.Add(InputSignal.Samples[i]);
                    SamplesIndeces.Add(InputSignal.SamplesIndices[i] - ShiftingValue);

                }
            else
                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {
                    Samples.Add(InputSignal.Samples[i]);
                    SamplesIndeces.Add(InputSignal.SamplesIndices[i] + ShiftingValue);

                }

            Periodic = InputSignal.Periodic;

            OutputShiftedSignal = new Signal(Samples, SamplesIndeces, Periodic);
        }
    }
}
