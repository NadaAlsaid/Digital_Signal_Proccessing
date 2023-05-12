using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class InverseDiscreteFourierTransform : Algorithm
    {
        public Signal InputFreqDomainSignal { get; set; }
        public Signal OutputTimeDomainSignal { get; set; }

        public override void Run()
        {
            //throw new NotImplementedException();

            float angle;
            List<float> samples = new List<float>();
            Complex c1, c2, c3 = new Complex(0, 0);

            for (int n = 0; n < InputFreqDomainSignal.Frequencies.Count(); n++)
            {
                c3 = new Complex(0, 0);
                for (int k = 0; k < InputFreqDomainSignal.Frequencies.Count(); k++)
                {
                   
                    angle = (2 * (float)Math.PI * n * k)/ InputFreqDomainSignal.Frequencies.Count();
                    c2 = new Complex(Math.Cos(angle), Math.Sin(angle));
                    c1 = new Complex(InputFreqDomainSignal.FrequenciesAmplitudes[k] * Math.Cos(InputFreqDomainSignal.FrequenciesPhaseShifts[k]),
                       InputFreqDomainSignal.FrequenciesAmplitudes[k] * Math.Sin(InputFreqDomainSignal.FrequenciesPhaseShifts[k]));
                    
                    c3 +=  c1 * c2;
                    

                }
               
                 float a =(float) c3.Real/ InputFreqDomainSignal.Frequencies.Count; 
                samples.Add(a);


            }
            OutputTimeDomainSignal = new Signal(samples,false);
        }
    }
}
