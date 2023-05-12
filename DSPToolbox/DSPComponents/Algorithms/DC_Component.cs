using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DC_Component: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            List<float> temp = new List<float>();
            float mean = InputSignal.Samples.Average();
            for(int i = 0; i < InputSignal.Samples.Count; i++)
            {
                float tmp=InputSignal.Samples[i]-mean;
                temp.Add(tmp);

            }

            OutputSignal = new Signal(temp, false);
            //throw new NotImplementedException();

        }
    }
}
