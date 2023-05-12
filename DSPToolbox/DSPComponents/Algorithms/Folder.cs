using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Folder : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputFoldedSignal { get; set; }

        public override void Run()
        {
            //throw new NotImplementedException();

            List<float> Samples = new List<float>();
            List<int> SamplesIndeces = new List<int>();
            bool Periodic = false;
            for (int i = InputSignal.Samples.Count - 1; i >= 0; i--)
            {
                Samples.Add(InputSignal.Samples[i]);
                SamplesIndeces.Add(-1 * InputSignal.SamplesIndices[i]);
            }
            Periodic = InputSignal.Periodic;

            OutputFoldedSignal = new Signal(Samples, SamplesIndeces, !Periodic);
        }
    }
}
