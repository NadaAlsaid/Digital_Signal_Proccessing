using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FIR : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public FILTER_TYPES InputFilterType { get; set; }
        public float InputFS { get; set; }
        public float? InputCutOffFrequency { get; set; }
        public float? InputF1 { get; set; }
        public float? InputF2 { get; set; }
        public float InputStopBandAttenuation { get; set; }
        public float InputTransitionBand { get; set; }
        public Signal OutputHn { get; set; }
        public Signal OutputYn { get; set; }

        public override void Run()
        {
            // throw new NotImplementedException();
            double n = 0,
            transitionWidth = InputTransitionBand / InputFS;
            List<float> sample = new List<float>();
            List<double> w = new List<double>();
            List<float> h;
            List<int> sampleIndecis = new List<int>();
            double Wn = 0;
            float Hn = 0;
            if (InputStopBandAttenuation <= 21)
            {
                n = 0.9 / transitionWidth;
                n = Math.Ceiling(n);
                if (n % 2 == 0) n++;
                for (int i = (int)(-(n - 1) / 2); i <= (n - 1) / 2; i++)
                    w.Add(1);

            }
            else if (InputStopBandAttenuation <= 44)
            {
                n = 3.1 / transitionWidth;
                n = Math.Ceiling(n);
                if (n % 2 == 0) n++;
                for (int i = (int)(-(n - 1) / 2); i <= (n - 1) / 2; i++)
                {
                    Wn = 0.5 + (0.5 * Math.Cos((2 * Math.PI * i) / n));
                    w.Add(Wn);
                }
            }
            else if (InputStopBandAttenuation <= 53)
            {

                n = 3.3 / transitionWidth;
                n = Math.Ceiling(n);
                if (n % 2 == 0) n++;
                for (int i = (int)(-(n - 1) / 2); i <= (n - 1) / 2; i++)
                {
                    Wn = 0.54 + (0.46 * Math.Cos(2 * Math.PI * i / n));
                    w.Add(Wn);
                }

            }
            else
            {
                n = 5.5 / transitionWidth;
                n = Math.Ceiling(n);
                if (n % 2 == 0) n++;

                for (int i = (int)(-(n - 1) / 2); i <= (n - 1) / 2; i++)
                {
                    Wn = 0.42 + (0.5 * Math.Cos((2 * Math.PI * i) / (n - 1)) + (0.08 * Math.Cos((4 * Math.PI * i) / (n - 1))));
                    w.Add(Wn);
                }

            }
            h = new List<float>();
            if (InputFilterType == FILTER_TYPES.LOW)
            {
                InputCutOffFrequency += (float)(InputTransitionBand / 2.0);

                InputCutOffFrequency /= InputFS;
                int j = 0;
                for (int i = (int)(-(n - 1) / 2); i <= (n - 1) / 2; i++)
                {
                    sampleIndecis.Add(i);
                    if (i == 0)
                    {
                        h.Add((float)(InputCutOffFrequency * 2 * w[j++]));
                        continue;

                    }
                    float seta = ((float)(Math.Sin(2 * i * (float)InputCutOffFrequency * Math.PI) / (i * Math.PI)));
                    h.Add((float)(seta * w[j++]));

                }


            }
            else if (InputFilterType == FILTER_TYPES.HIGH)
            {
                InputCutOffFrequency -= (float)(InputTransitionBand / 2.0);

                InputCutOffFrequency /= InputFS;
                int j = 0;
                for (int i = (int)(-(n - 1) / 2); i <= (n - 1) / 2; i++)
                {
                    sampleIndecis.Add(i);
                    if (i == 0)
                    {
                        h.Add((float)(1 - InputCutOffFrequency * 2 * w[j++]));
                        continue;

                    }
                    float seta = ((float)(Math.Sin(2 * i * (float)InputCutOffFrequency * Math.PI) / (i * Math.PI)));
                    h.Add((float)(-1 * seta * w[j++]));

                }
            }
            else if (InputFilterType == FILTER_TYPES.BAND_PASS)
            {
                InputF1 -= (float)(InputTransitionBand / 2.0);
                InputF1 /= InputFS;
                InputF2 += (float)(InputTransitionBand / 2.0);
                InputF2 /= InputFS;
                int j = 0;
                for (int i = (int)(-(n - 1) / 2); i <= (n - 1) / 2; i++)
                {
                    sampleIndecis.Add(i);
                    if (i == 0)
                    {
                        h.Add((float)(2 * (InputF2 - InputF1) * w[j++]));
                        continue;

                    }
                    float seta1 = ((float)(Math.Sin(2 * i * (float)InputF1 * Math.PI) / (2 * i * InputF1 * Math.PI)));
                    float seta2 = ((float)(Math.Sin(2 * i * (float)InputF2 * Math.PI) / (2 * i * InputF2 * Math.PI)));
                    Hn = (float)((InputF2 * 2 * seta2) - (InputF1 * 2 * seta1));
                    h.Add((float)(Hn * w[j++]));

                }
            }
            else
            {
                InputF1 += (float)(InputTransitionBand / 2.0);
                InputF1 /= InputFS;
                InputF2 -= (float)(InputTransitionBand / 2.0);
                InputF2 /= InputFS;
                int j = 0;
                for (int i = (int)(-(n - 1) / 2); i <= (n - 1) / 2; i++)
                {
                    sampleIndecis.Add(i);
                    if (i == 0)
                    {
                        h.Add((float)(1 - 2 * (InputF2 - InputF1) * w[j++]));
                        continue;

                    }
                    float seta1 = ((float)(Math.Sin(2 * i * (float)InputF1 * Math.PI) / (2 * i * InputF1 * Math.PI)));
                    float seta2 = ((float)(Math.Sin(2 * i * (float)InputF2 * Math.PI) / (2 * i * InputF2 * Math.PI)));
                    Hn = (float)((InputF1 * 2 * seta1) - (InputF2 * 2 * seta2));
                    h.Add((float)(Hn * w[j++]));
                }
            }
            OutputHn = new Signal(h, sampleIndecis, false);
            DirectConvolution directConvolution = new DirectConvolution();
            directConvolution.InputSignal1 = OutputHn;
            directConvolution.InputSignal2 = InputTimeDomainSignal;
            directConvolution.Run();
            OutputYn = directConvolution.OutputConvolvedSignal;
        }
    }
}
