using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;



namespace DSPAlgorithms.Algorithms
{
    public class DiscreteFourierTransform : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public float InputSamplingFrequency { get; set; }
        public Signal OutputFreqDomainSignal { get; set; }



        public override void Run()
        {
            //throw new NotImplementedException();
            double angle;
            Complex c1;
            List<float> Amplitudes = new List<float>();
            List<float> PhaseShifts = new List<float>();
            List<float> Frequency = new List<float>();
            double amplitudes, phaseShifts;
            for (int k = 0; k < InputTimeDomainSignal.Samples.Count; k++)
            {
                c1 = new Complex(0, 0);
                for (int n = 0; n < InputTimeDomainSignal.Samples.Count; n++)
                {

                    angle = (-2 *Math.PI * n * k)
                        / InputTimeDomainSignal.Samples.Count;
                    if (angle == Math.Abs(angle))
                        c1 += new Complex(InputTimeDomainSignal.Samples[n] * Math.Cos(angle),
                            InputTimeDomainSignal.Samples[n] * Math.Sin(angle));
                    else
                        c1 += new Complex(InputTimeDomainSignal.Samples[n] * Math.Cos(Math.Abs(angle)),
                            -1 * InputTimeDomainSignal.Samples[n] * Math.Sin(Math.Abs(angle)));
                }

                amplitudes = (Math.Sqrt ( Math.Pow(c1.Real, 2) + Math.Pow(c1.Imaginary, 2) ));
                Amplitudes.Add((float)amplitudes);

                /*Amplitudes.Add((float)c1.Magnitude); */
                phaseShifts = Math.Atan2(c1.Imaginary, c1.Real);
                PhaseShifts.Add( (float)phaseShifts );
                /*PhaseShifts.Add(
                   (float)c1.Phase
                   ) ;*/

                Frequency.Add((float)Decimal.Round((Decimal)(2 * Math.PI * k * InputSamplingFrequency) / InputTimeDomainSignal.Samples.Count, 1));
            }
            OutputFreqDomainSignal = new Signal(false, Frequency, Amplitudes, PhaseShifts);
        }
    }
}