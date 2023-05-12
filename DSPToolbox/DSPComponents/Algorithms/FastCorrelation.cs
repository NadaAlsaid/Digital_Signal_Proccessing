using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FastCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        public override void Run()
        {
            // throw new NotImplementedException();
            Signal OutputCorrelationSignal;
            Complex c1, c2, c3;
            List<float> Amplitudes = new List<float>();
            List<float> PhaseShifts = new List<float>();
            List<float> Frequency = new List<float>();
            DiscreteFourierTransform discreteFourierTransform = new DiscreteFourierTransform();
            float squar1 = 0, squar2 = 0;
            if (InputSignal2 != null)
            {
                int end1 = Math.Max(InputSignal1.Samples.Count, InputSignal2.Samples.Count);

                for (int n = 0; n < end1; n++)
                {
                    if (InputSignal1.Samples.Count > InputSignal2.Samples.Count)
                    {
                        InputSignal1.Samples.Add(0);
                        InputSignal1.SamplesIndices.Add(InputSignal1.Samples.Count + n);
                    }
                    else if (InputSignal2.Samples.Count > InputSignal1.Samples.Count)
                    {
                        InputSignal2.Samples.Add(0);
                        InputSignal2.SamplesIndices.Add(InputSignal2.Samples.Count + n);
                    }

                }
                for (int n = 0; n < InputSignal1.Samples.Count; n++)
                {
                    squar1 += (InputSignal1.Samples[n] * InputSignal1.Samples[n]);
                    squar2 += (InputSignal2.Samples[n] * InputSignal2.Samples[n]);
                }
                squar1 *= squar2;
                discreteFourierTransform.InputTimeDomainSignal = InputSignal1;
                discreteFourierTransform.Run();
                InputSignal1 = discreteFourierTransform.OutputFreqDomainSignal;
                discreteFourierTransform.InputTimeDomainSignal = InputSignal2;
                discreteFourierTransform.Run();
                InputSignal2 = discreteFourierTransform.OutputFreqDomainSignal;
                for (int n = 0; n < end1; n++)
                {

                    c1 = Complex.FromPolarCoordinates(InputSignal1.FrequenciesAmplitudes[n], InputSignal1.FrequenciesPhaseShifts[n]);
                    c2 = Complex.FromPolarCoordinates(InputSignal2.FrequenciesAmplitudes[n], InputSignal2.FrequenciesPhaseShifts[n]);
                    c3 = Complex.Conjugate(c1) * c2 / InputSignal1.Frequencies.Count;

                    Amplitudes.Add((float)c3.Magnitude);
                    PhaseShifts.Add(
                       (float)c3.Phase
                       );
                    Frequency.Add(n);

                }

            }
            else
            {
                for (int n = 0; n < InputSignal1.Samples.Count; n++)
                {
                    squar1 += (InputSignal1.Samples[n] * InputSignal1.Samples[n]);
                }
                squar1 *= squar1;
                discreteFourierTransform.InputTimeDomainSignal = InputSignal1;
                discreteFourierTransform.Run();
                InputSignal1 = discreteFourierTransform.OutputFreqDomainSignal;

                for (int n = 0; n < InputSignal1.Frequencies.Count; n++)
                {

                    c1 = Complex.FromPolarCoordinates(InputSignal1.FrequenciesAmplitudes[n], InputSignal1.FrequenciesPhaseShifts[n]);
                    c3 = Complex.Conjugate(c1) * c1 / InputSignal1.Frequencies.Count;

                    Amplitudes.Add((float)c3.Magnitude);
                    PhaseShifts.Add(
                       (float)c3.Phase
                       );
                    Frequency.Add(n);

                }
            }
            OutputCorrelationSignal = new Signal(false, Frequency, Amplitudes, PhaseShifts);
            InverseDiscreteFourierTransform inverseDiscreteFourierTransform = new InverseDiscreteFourierTransform();
            inverseDiscreteFourierTransform.InputFreqDomainSignal = OutputCorrelationSignal;
            inverseDiscreteFourierTransform.Run();
            OutputCorrelationSignal = inverseDiscreteFourierTransform.OutputTimeDomainSignal;
            OutputNonNormalizedCorrelation = OutputCorrelationSignal.Samples;
            OutputNormalizedCorrelation = new List<float>();
            for (int n = 0; n < OutputNonNormalizedCorrelation.Count; n++)
            {
                OutputNormalizedCorrelation.Add(OutputNonNormalizedCorrelation[n] * OutputNonNormalizedCorrelation.Count / (float)Math.Sqrt(squar1));
            }
        }
    }
}