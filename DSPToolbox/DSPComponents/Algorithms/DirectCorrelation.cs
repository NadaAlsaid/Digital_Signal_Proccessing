using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        public override void Run()
        {
            //throw new NotImplementedException();

            /*List<float> nonNormalizedSignal = new List<float>();
            List<float> NormalizedSignal = new List<float>();
            List<float> temp1 = new List<float>();

            double signal1squared = 0;
                float normalized;
            for(int i = 0;i<InputSignal1.Samples.Count;i++)
                signal1squared+=(InputSignal1.Samples[i] * InputSignal1.Samples[i]);

            if (InputSignal2 == null)//auto
            {
                List<float> temp2 = InputSignal1.Samples;
                for (int j = 0; j < InputSignal1.Samples.Count; j++)
                {
                    double signal2squared = 0;

                    for (int k = 0; k < InputSignal1.Samples.Count; k++)
                        temp1.Add(InputSignal1.Samples[k]);


                    float res = 0, tmp;
                    for (int i = 0, l = 0; i < InputSignal1.Samples.Count; i++)
                    {
                        if (i < (InputSignal1.Samples.Count -j ))
                            temp1[i] = InputSignal1.Samples[i + j];
                        else
                        {
                            if (InputSignal1.Periodic == true)
                                temp1[i] = temp2[l];
                            else
                                temp1[i] = 0;
                            l++;
                        }
                        signal2squared += (temp1[i]* temp1[i]);
                    }

                    for (int n = 0; n < InputSignal1.Samples.Count; n++)
                    {
                        tmp = InputSignal1.Samples[n] * temp1[n];
                        res += tmp;

                    }
                    res /= InputSignal1.Samples.Count;
                    nonNormalizedSignal.Add(res);
                    
                    normalized = (res * InputSignal1.Samples.Count) / (float)Math.Sqrt(signal1squared*signal2squared);
                    NormalizedSignal.Add(normalized);
                }
                OutputNonNormalizedCorrelation = new List<float>(nonNormalizedSignal);
                OutputNormalizedCorrelation=new List<float> (NormalizedSignal);


            }
            else
            {
                List<float> temp2 = InputSignal2.Samples;
                for (int j = 0; j < InputSignal1.Samples.Count; j++)
                {
                    float signal2squared = 0;
                    for (int k = 0; k < InputSignal2.Samples.Count; k++)
                        temp1.Add(InputSignal2.Samples[k]);

                    
                    float res = 0, tmp;
                    for (int i = 0, l = 0; i < InputSignal2.Samples.Count; i++)
                    {
                        if (i < (InputSignal2.Samples.Count - j))
                            temp1[i] = InputSignal2.Samples[i + j];
                        else
                        {
                            if (InputSignal2.Periodic == true)
                                temp1[i] = temp2[l];
                            else
                                temp1[i] = 0;
                            l++;
                        }
                        signal2squared += (temp1[i] * temp1[i]);
                    }

                    for (int n = 0; n < InputSignal1.Samples.Count; n++)
                    {
                        tmp = InputSignal1.Samples[n] * temp1[n];
                        res += tmp;

                    }
                    res /= InputSignal1.Samples.Count;
                    nonNormalizedSignal.Add(res);
                    normalized = (res * InputSignal1.Samples.Count) / (float)Math.Sqrt(signal1squared * signal2squared);
                    NormalizedSignal.Add(normalized);
                }
                OutputNonNormalizedCorrelation = new List<float>(nonNormalizedSignal);
                OutputNormalizedCorrelation = new List<float>(NormalizedSignal);*/

            List<float> nonNormalizedSignal = new List<float>();
            List<float> NormalizedSignal = new List<float>();
            List<float> temp1 = new List<float>();

            float signal1squared = 0, normalized;
            for (int i = 0; i < InputSignal1.Samples.Count; i++)
                signal1squared += (InputSignal1.Samples[i] * InputSignal1.Samples[i]);

            if (InputSignal2 == null)
            {
                List<float> temp2 = InputSignal1.Samples;
                for (int j = 0; j < InputSignal1.Samples.Count; j++)
                {
                    float signal2squared = 0;

                    for (int k = 0; k < InputSignal1.Samples.Count; k++)
                        temp1.Add(InputSignal1.Samples[k]);


                    float res = 0, tmp, res1 = 0;
                    for (int i = 0, l = 0; i < InputSignal1.Samples.Count; i++)
                    {

                        if (i < (InputSignal1.Samples.Count - j))
                            temp1[i] = InputSignal1.Samples[i + j];
                        else
                        {
                            if (InputSignal1.Periodic == true)
                                temp1[i] = temp2[l];
                            else
                                temp1[i] = 0;
                            l++;
                        }
                        signal2squared += (temp1[i] * temp1[i]);
                    }

                    for (int n = 0; n < InputSignal1.Samples.Count; n++)
                    {
                        tmp = InputSignal1.Samples[n] * temp1[n];
                        res += tmp;

                    }
                    res1 = res;
                    res /= InputSignal1.Samples.Count;
                    nonNormalizedSignal.Add((float)res);
                    normalized = (((res1) / signal1squared));
                    NormalizedSignal.Add(normalized);
                }
                OutputNonNormalizedCorrelation = new List<float>(nonNormalizedSignal);
                OutputNormalizedCorrelation = new List<float>(NormalizedSignal);


            }
            else
            {
                List<float> temp2 = InputSignal2.Samples;
                for (int j = 0; j < InputSignal1.Samples.Count; j++)
                {
                    float signal2squared = 0;
                    for (int k = 0; k < InputSignal2.Samples.Count; k++)
                        temp1.Add(InputSignal2.Samples[k]);


                    float res = 0, tmp, res1 = 0;
                    for (int i = 0, l = 0; i < InputSignal2.Samples.Count; i++)
                    {
                        if (i < (InputSignal2.Samples.Count - j))
                            temp1[i] = InputSignal2.Samples[i + j];
                        else
                        {
                            if (InputSignal2.Periodic == true)
                                temp1[i] = temp2[l];
                            else
                                temp1[i] = 0;
                            l++;
                        }
                        signal2squared += (temp1[i] * temp1[i]);
                    }

                    for (int n = 0; n < InputSignal1.Samples.Count; n++)
                    {
                        tmp = InputSignal1.Samples[n] * temp1[n];
                        res += tmp;

                    }
                    res1 = res;
                    res /= InputSignal1.Samples.Count;
                    nonNormalizedSignal.Add((float)res);
                    normalized = ((res1) / (float)Math.Sqrt(signal1squared * signal2squared));
                    NormalizedSignal.Add(normalized);
                }
                OutputNonNormalizedCorrelation = new List<float>(nonNormalizedSignal);
                OutputNormalizedCorrelation = new List<float>(NormalizedSignal);

            }
        }
    }
}