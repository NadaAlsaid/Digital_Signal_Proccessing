using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class QuantizationAndEncoding : Algorithm
    {
        // You will have only one of (InputLevel or InputNumBits), the other property will take a negative value
        // If InputNumBits is given, you need to calculate and set InputLevel value and vice versa
        public int InputLevel { get; set; }
        public int InputNumBits { get; set; }
        public Signal InputSignal { get; set; }
        public Signal OutputQuantizedSignal { get; set; }
        public List<int> OutputIntervalIndices { get; set; }
        public List<string> OutputEncodedSignal { get; set; }
        public List<float> OutputSamplesError { get; set; }

        public override void Run()
        {
            List<float> midPoint = new List<float>();
            List<float> OutputQuantized = new List<float>();
            List<float> Interval = new List<float>();
            OutputIntervalIndices = new List<int>();
            OutputSamplesError = new List<float>();
            OutputEncodedSignal = new List<string>();
            float delta;
            if (InputLevel == 0)
                InputLevel = (int)Math.Pow(2, InputNumBits);
            if (InputNumBits == 0)
                InputNumBits = (int)Math.Log(InputLevel, 2);
            float minAmplitute = InputSignal.Samples.Min();
            float maxAmplitute = InputSignal.Samples.Max();
            delta = (maxAmplitute - minAmplitute)
                / InputLevel;
            Interval.Add(minAmplitute + delta);
            midPoint.Add((Interval[0] + minAmplitute) / 2);
            for (int i = 1; i < InputLevel; i++)
            {
                Interval.Add(Interval[i - 1] + delta);
                midPoint.Add((Interval[i] + Interval[i - 1]) / 2);
            }
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                bool flag = false;
                for (int j = 0; j < Interval.Count; j++)
                {
                    if (InputSignal.Samples[i] <= Interval[j])
                    {
                        OutputIntervalIndices.Add(j + 1);
                        OutputQuantized.Add(midPoint[j]);
                        OutputSamplesError.Add(midPoint[j] - InputSignal.Samples[i]);
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    OutputIntervalIndices.Add(InputLevel);
                    OutputQuantized.Add(midPoint[InputLevel - 1]);
                    OutputSamplesError.Add(midPoint[InputLevel - 1] - InputSignal.Samples[i]);
                }
            }
            OutputQuantizedSignal = new Signal(OutputQuantized, false);
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                String ConvertedBinary = Convert.ToString((OutputIntervalIndices[i] - 1), 2);
                ConvertedBinary = ConvertedBinary.PadLeft(InputNumBits, '0');
                OutputEncodedSignal.Add(ConvertedBinary);
            }
        
        //throw new NotImplementedException();
        }
    }
}
