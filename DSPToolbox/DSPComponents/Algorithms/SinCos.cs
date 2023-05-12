using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class SinCos: Algorithm
    {
        public string type { get; set; }
        public float A { get; set; }
        public float PhaseShift { get; set; }
        public float AnalogFrequency { get; set; }
        public float SamplingFrequency { get; set; }
        public List<float> samples { get; set; }
        public override void Run()
        {
            samples = new List<float>();

            if (type.CompareTo("cos") == 0) { 
                for(int i = 0; i < SamplingFrequency; i++)
                {
                    float temp =(float) (A * Math.Cos(2*Math.PI*AnalogFrequency/ SamplingFrequency * i+PhaseShift));
                   
                    samples.Add(temp);
                }
                 
            }

            if (type.CompareTo("sin") == 0)
            {
                for (int i = 0; i < SamplingFrequency; i++)
                {
                    float temp = (float)(A * Math.Sin(2 * Math.PI * AnalogFrequency / SamplingFrequency * i + PhaseShift));

                    samples.Add(temp);
                }

            }
            // throw new NotImplementedException();
        }
    }
}
