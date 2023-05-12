using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FastConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            // throw new NotImplementedException();
            

            List<float> Samples = new List<float>();
            List<int> SamplesIndices = new List<int>();
            int end1 = InputSignal1.Samples.Count + InputSignal2.Samples.Count - 1;

            for (int n = InputSignal1.Samples.Count, i = InputSignal2.Samples.Count; true; n++, i++)
            {
                if (end1 > n)
                {
                    InputSignal1.Samples.Add(0);
                    InputSignal1.SamplesIndices.Add(n);
                }
                if (end1 > i)
                {
                    InputSignal2.Samples.Add(0);
                    InputSignal2.SamplesIndices.Add(i);
                }
                if (i >= end1 && n >= end1)
                    break;
            }
            DiscreteFourierTransform discreteFourierTransform = new DiscreteFourierTransform();
            discreteFourierTransform.InputTimeDomainSignal = InputSignal1;
            discreteFourierTransform.Run();
            InputSignal1 = discreteFourierTransform.OutputFreqDomainSignal;
            discreteFourierTransform.InputTimeDomainSignal = InputSignal2;
            discreteFourierTransform.Run();
            InputSignal2 = discreteFourierTransform.OutputFreqDomainSignal;
            Complex c1, c2, c3;
            List<float> Amplitudes = new List<float>();
            List<float> PhaseShifts = new List<float>();
            List<float> Frequency = new List<float>();
            for (int n = 0; n < end1; n++)
            {

                c1 = Complex.FromPolarCoordinates(InputSignal1.FrequenciesAmplitudes[n], InputSignal1.FrequenciesPhaseShifts[n]);
                c2 = Complex.FromPolarCoordinates(InputSignal2.FrequenciesAmplitudes[n], InputSignal2.FrequenciesPhaseShifts[n]);
                c3 = c1 * c2;

                Amplitudes.Add((float)c3.Magnitude);
                PhaseShifts.Add(
                   (float)c3.Phase
                   );
                Frequency.Add(n);


            }


            OutputConvolvedSignal = new Signal(false, Frequency, Amplitudes, PhaseShifts);
            InverseDiscreteFourierTransform inverseDiscreteFourierTransform = new InverseDiscreteFourierTransform();
            inverseDiscreteFourierTransform.InputFreqDomainSignal = OutputConvolvedSignal;
            inverseDiscreteFourierTransform.Run();
            OutputConvolvedSignal = inverseDiscreteFourierTransform.OutputTimeDomainSignal;

        }
    }
}
