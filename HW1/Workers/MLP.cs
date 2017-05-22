using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1
{
    public class MLP
    {
        public double trainingCoefficient = 0.01;
        public double Momentum = 0.99;
        private double DefaultWeightLimitMin = -0.02;
        private double DefaultWeightLimitMax = 0.02;
        private double Bias = 1;
        public int    AttributeCount = (29 + 1);

        public int iterationLimit = 1000;
        int localErrorLimit = 3;

        int numInput = (29 + 1);
        int numHidden = (6 + 1);
        int numOutput = 1; // number of classes for Y
        int numRows = 300;
        int seed = 1;

        private Random rnd;

        public double[,] initial_weights;
        public double[] initial_values;
        public double[] hidden_weights;
        public double[] hidden_values;
        public List<double> DesiredOutput;
        public List<double> RealOutput;
        public List<double> Error;
        public List<double> ChangeOnGlobalError;
        public List<int> Iteration;
        public int CorrectCount = 0;
        public int FalseCount = 0;
        public void Train(List<ForestFire> TrainSet, int hiddenCount)
        {
            numHidden = hiddenCount + 1;
            initial_weights = new double[AttributeCount, numHidden];
            initial_values = new double[AttributeCount];
            hidden_weights = new double[numHidden];
            hidden_values = new double[numHidden];

            double temp_weight_sum_norm = 0;
            double output = 0;
            double delta_v_hidden = 0;
            double delta_w_initial = 0;
            double temp_sigmoid = 0;
            #region Give Random Weights

            //Random random = new Random();
            //for (int i = 1; i < numHidden; i++)
            //{
            //    for (int j = 0; j < numInput; j++)
            //    {
            //        initial_weights[j, i] = (double)random.Next(-200000, 200000) / 10000000.0; //GetRandomWeight();
            //    }

            //}

            //for (int i = 0; i < numHidden; i++)
            //{
            //    hidden_weights[i] = (double)random.Next(-200000, 200000) / 10000000.0;
            //}

            InitializeWeights();


            #endregion

            for (int x = 0; x < iterationLimit; x++)
            {

                foreach (ForestFire ForestFireTrain in TrainSet)
                {
                    #region Put inputs into an array

                    initial_values[0] = 1.0;
                    initial_values[1] = ForestFireTrain.X;
                    initial_values[2] = ForestFireTrain.Y;
                    initial_values[3] = ForestFireTrain.FFMC;
                    initial_values[4] = ForestFireTrain.DMC;
                    initial_values[5] = ForestFireTrain.DC;
                    initial_values[6] = ForestFireTrain.ISI;
                    initial_values[7] = ForestFireTrain.temp;
                    initial_values[8] = ForestFireTrain.RH;
                    initial_values[9] = ForestFireTrain.wind;
                    initial_values[10] = ForestFireTrain.rain;
                    initial_values[11] = ForestFireTrain.CD_Month_jan;
                    initial_values[12] = ForestFireTrain.CD_Month_feb;
                    initial_values[13] = ForestFireTrain.CD_Month_mar;
                    initial_values[14] = ForestFireTrain.CD_Month_apr;
                    initial_values[15] = ForestFireTrain.CD_Month_may;
                    initial_values[16] = ForestFireTrain.CD_Month_jun;
                    initial_values[17] = ForestFireTrain.CD_Month_jul;
                    initial_values[18] = ForestFireTrain.CD_Month_aug;
                    initial_values[19] = ForestFireTrain.CD_Month_sep;
                    initial_values[20] = ForestFireTrain.CD_Month_oct;
                    initial_values[21] = ForestFireTrain.CD_Month_nov;
                    initial_values[22] = ForestFireTrain.CD_Month_dec;
                    initial_values[23] = ForestFireTrain.CD_WeekDay_mon;
                    initial_values[24] = ForestFireTrain.CD_WeekDay_tue;
                    initial_values[25] = ForestFireTrain.CD_WeekDay_wed;
                    initial_values[26] = ForestFireTrain.CD_WeekDay_thu;
                    initial_values[27] = ForestFireTrain.CD_WeekDay_fri;
                    initial_values[28] = ForestFireTrain.CD_WeekDay_sat;
                    initial_values[29] = ForestFireTrain.CD_WeekDay_sun;




                    #endregion

                    #region Calculate Hidden Neuron Values

                    
                    for (int i = 0; i < numHidden; i++)
                    {
                        if (i == 0) hidden_values[i] = 1;
                        else
                        {
                            for (int j = 0; j < numInput; j++)
                            {
                                temp_sigmoid += initial_values[j] * initial_weights[j, i];
                            }
                            hidden_values[i] = Sigmoid.Output(temp_sigmoid);
                        }
                    
                    }
                    #endregion

                    #region Calculate Output

                    
                    for (int i = 0; i < numHidden; i++)
                    {
                        output += hidden_values[i] * hidden_weights[i];
                    }

                    #endregion

                    #region Calculate Delta Hidden Weight

                    
                    for (int i = 0; i < numHidden; i++)
                    {
                        //delta_v_hidden += trainingCoefficient * (ForestFireTrain.area - output) * hidden_values[i];
                        delta_v_hidden += trainingCoefficient * (ForestFireTrain.area - output) * hidden_values[i] * (1.0 - hidden_values[i]);
                    }


                    #endregion

                    #region Calculate Delta Initial Weights

                    
                    for (int i = 0; i < numHidden; i++)
                    {
                        for (int j = 0; j < numInput; j++)
                        {
                            //delta_w_initial += trainingCoefficient * (ForestFireTrain.area - output) * hidden_weights[i] * hidden_values[i] * (1.0 - hidden_values[i]) * initial_values[j];
                            delta_w_initial += trainingCoefficient * (ForestFireTrain.area - output) * hidden_weights[i] * hidden_values[i] * (1.0 - hidden_values[i]) * initial_values[j];
                        }
                    }

                    #endregion

                    #region Assign New Hidden Weights

                    for (int i = 0; i < numHidden; i++)
                    {
                        hidden_weights[i] -= delta_v_hidden;
                    }

                    #endregion

                    #region Assign New Initial Weights

                    for (int i = 0; i < numHidden; i++)
                    {
                        for (int j = 0; j < numInput; j++)
                        {
                            initial_weights[j, i] -= delta_w_initial;
                        }
                    }

                    #endregion

                    #region Information Collection

                    output = 0;
                    delta_v_hidden = 0;
                    delta_w_initial = 0;
                    temp_sigmoid = 0;
                    #endregion

                }

                #region Normalize Weights

                for (int i = 0; i < numHidden; i++)
                {
                    for (int j = 0; j < numInput; j++)
                    {
                        temp_weight_sum_norm += (initial_weights[j, i]);
                    }

                    for (int j = 0; j < numInput; j++)
                    {
                         initial_weights[j, i] = initial_weights[j, i] / temp_weight_sum_norm;
                    }
                }
                temp_weight_sum_norm = 0;
                for (int i = 0; i < numHidden; i++)
                {
                    temp_weight_sum_norm += (hidden_weights[i]);
                }
                for (int i = 0; i < numHidden; i++)
                {
                    hidden_weights[i] = hidden_weights[i] / temp_weight_sum_norm;
                }
                temp_weight_sum_norm = 0;

                #endregion

                trainingCoefficient = Momentum * trainingCoefficient;
            }





        }
        /*
        public double[] TrainCopy(double[][] trainData)
        {
            // train using back-prop
            // back-prop specific arrays
            double[][] hoGrads = MakeMatrix(numHidden, numOutput, 0.0); // hidden-to-output weight gradients
            double[] obGrads = new double[numOutput];                   // output bias gradients

            double[][] ihGrads = MakeMatrix(numInput, numHidden, 0.0);  // input-to-hidden weight gradients
            double[] hbGrads = new double[numHidden];                   // hidden bias gradients

            double[] oSignals = new double[numOutput];                  // local gradient output signals - gradients w/o associated input terms
            double[] hSignals = new double[numHidden];                  // local gradient hidden node signals

            // back-prop momentum specific arrays 
            double[][] ihPrevWeightsDelta = MakeMatrix(numInput, numHidden, 0.0);
            double[] hPrevBiasesDelta = new double[numHidden];
            double[][] hoPrevWeightsDelta = MakeMatrix(numHidden, numOutput, 0.0);
            double[] oPrevBiasesDelta = new double[numOutput];

            int epoch = 0;
            double[] xValues = new double[numInput]; // inputs
            double[] tValues = new double[numOutput]; // target values
            double derivative = 0.0;
            double errorSignal = 0.0;

            int[] sequence = new int[trainData.Length];
            for (int i = 0; i < sequence.Length; ++i)
                sequence[i] = i;

            int errInterval = iterationLimit / 10; // interval to check error
            while (epoch < iterationLimit)
            {
                ++epoch;

                if (epoch % errInterval == 0 && epoch < iterationLimit)
                {
                    double trainErr = Error(trainData);
                    Console.WriteLine("epoch = " + epoch + "  error = " +
                      trainErr.ToString("F4"));
                    //Console.ReadLine();
                }

                Shuffle(sequence); // visit each training data in random order
                for (int ii = 0; ii < trainData.Length; ++ii)
                {
                    int idx = sequence[ii];
                    Array.Copy(trainData[idx], xValues, numInput);
                    Array.Copy(trainData[idx], numInput, tValues, 0, numOutput);
                    ComputeOutputs(xValues); // copy xValues in, compute outputs 

                    // indices: i = inputs, j = hiddens, k = outputs

                    // 1. compute output node signals (assumes softmax)
                    for (int k = 0; k < numOutput; ++k)
                    {
                        errorSignal = tValues[k] - outputs[k];  // Wikipedia uses (o-t)
                        derivative = (1 - outputs[k]) * outputs[k]; // for softmax
                        oSignals[k] = errorSignal * derivative;
                    }

                    // 2. compute hidden-to-output weight gradients using output signals
                    for (int j = 0; j < numHidden; ++j)
                        for (int k = 0; k < numOutput; ++k)
                            hoGrads[j][k] = oSignals[k] * hOutputs[j];

                    // 2b. compute output bias gradients using output signals
                    for (int k = 0; k < numOutput; ++k)
                        obGrads[k] = oSignals[k] * 1.0; // dummy assoc. input value

                    // 3. compute hidden node signals
                    for (int j = 0; j < numHidden; ++j)
                    {
                        derivative = (1 + hOutputs[j]) * (1 - hOutputs[j]); // for tanh
                        double sum = 0.0; // need sums of output signals times hidden-to-output weights
                        for (int k = 0; k < numOutput; ++k)
                        {
                            sum += oSignals[k] * hoWeights[j][k]; // represents error signal
                        }
                        hSignals[j] = derivative * sum;
                    }

                    // 4. compute input-hidden weight gradients
                    for (int i = 0; i < numInput; ++i)
                        for (int j = 0; j < numHidden; ++j)
                            ihGrads[i][j] = hSignals[j] * inputs[i];

                    // 4b. compute hidden node bias gradients
                    for (int j = 0; j < numHidden; ++j)
                        hbGrads[j] = hSignals[j] * 1.0; // dummy 1.0 input

                    // == update weights and biases

                    // update input-to-hidden weights
                    for (int i = 0; i < numInput; ++i)
                    {
                        for (int j = 0; j < numHidden; ++j)
                        {
                            double delta = ihGrads[i][j] * trainingCoefficient;
                            ihWeights[i][j] += delta; // would be -= if (o-t)
                            ihWeights[i][j] += ihPrevWeightsDelta[i][j] * Momentum;
                            ihPrevWeightsDelta[i][j] = delta; // save for next time
                        }
                    }

                    // update hidden biases
                    for (int j = 0; j < numHidden; ++j)
                    {
                        double delta = hbGrads[j] * trainingCoefficient;
                        hBiases[j] += delta;
                        hBiases[j] += hPrevBiasesDelta[j] * Momentum;
                        hPrevBiasesDelta[j] = delta;
                    }

                    // update hidden-to-output weights
                    for (int j = 0; j < numHidden; ++j)
                    {
                        for (int k = 0; k < numOutput; ++k)
                        {
                            double delta = hoGrads[j][k] * trainingCoefficient;
                            hoWeights[j][k] += delta;
                            hoWeights[j][k] += hoPrevWeightsDelta[j][k] * Momentum;
                            hoPrevWeightsDelta[j][k] = delta;
                        }
                    }

                    // update output node biases
                    for (int k = 0; k < numOutput; ++k)
                    {
                        double delta = obGrads[k] * trainingCoefficient;
                        oBiases[k] += delta;
                        oBiases[k] += oPrevBiasesDelta[k] * Momentum;
                        oPrevBiasesDelta[k] = delta;
                    }

                } // each training item

            } // while
            double[] bestWts = GetWeights();
            return bestWts;
        } // Train



        public void Train2(List<ForestFire> TrainSet)
        {
            this.rnd = new Random(0);
            int epoch = 0;

            #region Give Random Weights

            InitializeWeights();

            #endregion



        }
        */



        public double MeanError = 0;
        public double SumOfSquareError = 0;
        public double SumOfSquareErrorMean = 0;
        public double SumOfError = 0;
        public double ErrorStdDev = 0;

        public double MeanValue = 0;
        public double SumOfSquareValue = 0;
        public double SumOfSquareValueMean = 0;
        public double SumOfValue = 0;
        public double ValueStdDev = 0;

        public double MeanOutput = 0;
        public double SumOfSquareOutput = 0;
        public double SumOfSquareOutputMean = 0;
        public double SumOfOutput = 0;
        public double OutputStdDev = 0;
        public void Test(List<ForestFire> TestSet)
        {
            double LocalError = 0;
            DesiredOutput = new List<double>();
            RealOutput = new List<double>();
            Error = new List<double>();
            ChangeOnGlobalError = new List<double>();
            Iteration = new List<int>();

            CorrectCount = 0;
            FalseCount = 0;
            
            foreach (ForestFire ForestFireTrain in TestSet)
            {
                #region Put inputs into an array

                initial_values[0] = 1.0;
                initial_values[1] = ForestFireTrain.X;
                initial_values[2] = ForestFireTrain.Y;
                initial_values[3] = ForestFireTrain.FFMC;
                initial_values[4] = ForestFireTrain.DMC;
                initial_values[5] = ForestFireTrain.DC;
                initial_values[6] = ForestFireTrain.ISI;
                initial_values[7] = ForestFireTrain.temp;
                initial_values[8] = ForestFireTrain.RH;
                initial_values[9] = ForestFireTrain.wind;
                initial_values[10] = ForestFireTrain.rain;
                initial_values[11] = ForestFireTrain.CD_Month_jan;
                initial_values[12] = ForestFireTrain.CD_Month_feb;
                initial_values[13] = ForestFireTrain.CD_Month_mar;
                initial_values[14] = ForestFireTrain.CD_Month_apr;
                initial_values[15] = ForestFireTrain.CD_Month_may;
                initial_values[16] = ForestFireTrain.CD_Month_jun;
                initial_values[17] = ForestFireTrain.CD_Month_jul;
                initial_values[18] = ForestFireTrain.CD_Month_aug;
                initial_values[19] = ForestFireTrain.CD_Month_sep;
                initial_values[20] = ForestFireTrain.CD_Month_oct;
                initial_values[21] = ForestFireTrain.CD_Month_nov;
                initial_values[22] = ForestFireTrain.CD_Month_dec;
                initial_values[23] = ForestFireTrain.CD_WeekDay_mon;
                initial_values[24] = ForestFireTrain.CD_WeekDay_tue;
                initial_values[25] = ForestFireTrain.CD_WeekDay_wed;
                initial_values[26] = ForestFireTrain.CD_WeekDay_thu;
                initial_values[27] = ForestFireTrain.CD_WeekDay_fri;
                initial_values[28] = ForestFireTrain.CD_WeekDay_sat;
                initial_values[29] = ForestFireTrain.CD_WeekDay_sun;




                #endregion

                #region Calculate Hidden Neuron Values

                double temp_sigmoid = 0;
                for (int i = 0; i < numHidden; i++)
                {
                    if (i == 0) hidden_values[i] = 1;
                    else
                    {
                        for (int j = 0; j < numInput; j++)
                        {
                            temp_sigmoid += initial_values[j] * initial_weights[j, i];
                        }
                        hidden_values[i] = Sigmoid.Output(temp_sigmoid);
                    }

                }
                #endregion

                #region Calculate Output

                double output = 0;
                for (int i = 0; i < numHidden; i++)
                {
                    output += hidden_values[i] * hidden_weights[i];
                }

                


                #endregion

                #region Information Collection

                LocalError = ForestFireTrain.area - output;

                if (Math.Abs(output) <= Math.Abs(ForestFireTrain.area) * 1.2 && Math.Abs(output) >= Math.Abs(ForestFireTrain.area) * 0.8)
                    CorrectCount++;
                else
                    FalseCount++;

                //DesiredOutput.Add(output);
                //RealOutput.Add(ForestFireTrain.area);
                //Error.Add(LocalError);
                SumOfError += LocalError;
                SumOfSquareError += LocalError * LocalError;

                SumOfOutput += output;
                SumOfSquareOutput += output * output;

                SumOfValue += ForestFireTrain.area;
                SumOfSquareValue += ForestFireTrain.area * ForestFireTrain.area;

                #endregion

            }

            SumOfSquareErrorMean = SumOfSquareError / TestSet.Count;
            MeanError = SumOfError / TestSet.Count;
            ErrorStdDev = Math.Sqrt((SumOfSquareError - (MeanError * MeanError * TestSet.Count)) / TestSet.Count);

            SumOfSquareValueMean = SumOfSquareValue / TestSet.Count;
            MeanValue = SumOfValue / TestSet.Count;
            ValueStdDev = Math.Sqrt((SumOfSquareValue - (MeanValue * MeanValue * TestSet.Count)) / TestSet.Count);

            SumOfSquareOutputMean = SumOfSquareOutput / TestSet.Count;
            MeanOutput = SumOfOutput / TestSet.Count;
            OutputStdDev = Math.Sqrt((SumOfSquareOutput - (MeanOutput * MeanOutput * TestSet.Count)) / TestSet.Count);
        }


        private void InitializeWeights() // helper for ctor
        {
            rnd = new Random();
            // initialize weights and biases to small random values
            for (int i = 1; i < numHidden; i++)
            {
                for (int j = 0; j < numInput; j++)
                {
                    initial_weights[j, i] = (0.001 - 0.0001) * rnd.NextDouble() + 0.0001;
                }

            }

            for (int i = 0; i < numHidden; i++)
            {
                hidden_weights[i] = (0.001 - 0.0001) * rnd.NextDouble() + 0.0001;
            }
        }
        /*
        private double Error(double[][] trainData)
        {
            // average squared error per training item
            double sumSquaredError = 0.0;
            double[] xValues = new double[numInput]; // first numInput values in trainData
            double[] tValues = new double[numOutput]; // last numOutput values

            // walk thru each training case. looks like (6.9 3.2 5.7 2.3) (0 0 1)
            for (int i = 0; i < trainData.Length; ++i)
            {
                Array.Copy(trainData[i], xValues, numInput);
                Array.Copy(trainData[i], numInput, tValues, 0, numOutput); // get target values
                double[] yValues = this.ComputeOutputs(xValues); // outputs using current weights
                for (int j = 0; j < numOutput; ++j)
                {
                    double err = tValues[j] - yValues[j];
                    sumSquaredError += err * err;
                }
            }
            return sumSquaredError / trainData.Length;
        } // MeanSquaredError

        public double Accuracy(double[][] testData)
        {
            // percentage correct using winner-takes all
            int numCorrect = 0;
            int numWrong = 0;
            double[] xValues = new double[numInput]; // inputs
            double[] tValues = new double[numOutput]; // targets
            double[] yValues; // computed Y

            for (int i = 0; i < testData.Length; ++i)
            {
                Array.Copy(testData[i], xValues, numInput); // get x-values
                Array.Copy(testData[i], numInput, tValues, 0, numOutput); // get t-values
                yValues = this.ComputeOutputs(xValues);
                int maxIndex = MaxIndex(yValues); // which cell in yValues has largest value?
                int tMaxIndex = MaxIndex(tValues);

                if (maxIndex == tMaxIndex)
                    ++numCorrect;
                else
                    ++numWrong;
            }
            return (numCorrect * 1.0) / (numCorrect + numWrong);
        }

        private static double[][] MakeMatrix(int rows, int cols, double v) // helper for ctor, Train
        {
            double[][] result = new double[rows][];
            for (int r = 0; r < result.Length; ++r)
                result[r] = new double[cols];
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < cols; ++j)
                    result[i][j] = v;
            return result;
        }

        public double[] ComputeOutputs(double[] xValues)
        {
            double[] hSums = new double[numHidden]; // hidden nodes sums scratch array
            double[] oSums = new double[numOutput]; // output nodes sums

            for (int i = 0; i < xValues.Length; ++i) // copy x-values to inputs
                this.inputs[i] = xValues[i];
            // note: no need to copy x-values unless you implement a ToString.
            // more efficient is to simply use the xValues[] directly.

            for (int j = 0; j < numHidden; ++j)  // compute i-h sum of weights * inputs
                for (int i = 0; i < numInput; ++i)
                    hSums[j] += this.inputs[i] * this.ihWeights[i][j]; // note +=

            for (int i = 0; i < numHidden; ++i)  // add biases to hidden sums
                hSums[i] += this.hBiases[i];

            for (int i = 0; i < numHidden; ++i)   // apply activation
                this.hOutputs[i] = Sigmoid.HyperTan(hSums[i]); // hard-coded

            for (int j = 0; j < numOutput; ++j)   // compute h-o sum of weights * hOutputs
                for (int i = 0; i < numHidden; ++i)
                    oSums[j] += hOutputs[i] * hoWeights[i][j];

            for (int i = 0; i < numOutput; ++i)  // add biases to output sums
                oSums[i] += oBiases[i];

            double[] softOut = Softmax(oSums); // all outputs at once for efficiency
            Array.Copy(softOut, outputs, softOut.Length);

            double[] retResult = new double[numOutput]; // could define a GetOutputs 
            Array.Copy(this.outputs, retResult, retResult.Length);
            return retResult;
        }
        */
    }
}
