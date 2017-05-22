using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

            DataPrep DataPreppp = new DataPrep();
            DataPreppp.Initialize();
            MLP MultiLayerPerceptron;
            StringBuilder str = new StringBuilder();

            for (int i = 1; i < 11; i++)
            {
                MultiLayerPerceptron = new MLP();
                
                MultiLayerPerceptron.Train(DataPreppp.ForestFireTrainSet,i);
                MultiLayerPerceptron.Test(DataPreppp.ForestFireTestSet);

                //str.AppendLine(MultiLayerPerceptron.Error.Min() +  " " + MultiLayerPerceptron.Error.Max() + " " + MultiLayerPerceptron.Error.Average());
                //str.AppendLine(MultiLayerPerceptron.ChangeOnGlobalError.Min() + " " + MultiLayerPerceptron.ChangeOnGlobalError.Max() + " " + MultiLayerPerceptron.ChangeOnGlobalError.Average());

                str.AppendLine(String.Format("Multilayer Perceptron with {0} Momentum Rate, {1} Epoch, {2} Training Coefficient, {3} Input Neurons, {4} Hidden Neurons", MultiLayerPerceptron.Momentum, MultiLayerPerceptron.iterationLimit, MultiLayerPerceptron.trainingCoefficient, MultiLayerPerceptron.AttributeCount,i));
                str.AppendLine(MultiLayerPerceptron.CorrectCount + " " + MultiLayerPerceptron.FalseCount + " ");

                str.AppendLine("");
                str.AppendLine("MeanError : " + MultiLayerPerceptron.MeanError + " ");
                str.AppendLine("SumOfSquareError : " + MultiLayerPerceptron.SumOfSquareError + " ");
                str.AppendLine("SumOfSquareErrorMean : " + MultiLayerPerceptron.SumOfSquareErrorMean + " ");
                str.AppendLine("SumOfError : " + MultiLayerPerceptron.SumOfError + " ");
                str.AppendLine("ErrorStdDev : " + MultiLayerPerceptron.ErrorStdDev + " ");
                str.AppendLine("");
                str.AppendLine("MeanValue : " + MultiLayerPerceptron.MeanValue + " ");
                str.AppendLine("SumOfSquareValue : " + MultiLayerPerceptron.SumOfSquareValue + " ");
                str.AppendLine("SumOfSquareValueMean : " + MultiLayerPerceptron.SumOfSquareValueMean + " ");
                str.AppendLine("SumOfValue : " + MultiLayerPerceptron.SumOfValue + " ");
                str.AppendLine("ValueStdDev : " + MultiLayerPerceptron.ValueStdDev + " ");
                str.AppendLine("");
                str.AppendLine("MeanOutput : " + MultiLayerPerceptron.MeanOutput + " ");
                str.AppendLine("SumOfSquareOutput : " + MultiLayerPerceptron.SumOfSquareOutput + " ");
                str.AppendLine("SumOfSquareOutputMean : " + MultiLayerPerceptron.SumOfSquareOutputMean + " ");
                str.AppendLine("SumOfOutput : " + MultiLayerPerceptron.SumOfOutput + " ");
                str.AppendLine("OutputStdDev : " + MultiLayerPerceptron.OutputStdDev + " ");
                str.AppendLine("");
                str.AppendLine("");
                str.AppendLine("");
                textBox1.Text = str.ToString();
            }
            

            //for (int i = 0; i < MultiLayerPerceptron.DesiredOutput.Count; i++)
            //{
            //    str.AppendLine(MultiLayerPerceptron.DesiredOutput.ElementAt(i) + " " +
            //    MultiLayerPerceptron.RealOutput.ElementAt(i) + " " +
            //    MultiLayerPerceptron.Error.ElementAt(i) + " ");
            //}
            
            //for (int j = 0; j < 30; j++)
            //{
            //    for (int i = 0; i < 7; i++)
            //    {
            //        str.Append(MultiLayerPerceptron.initial_weights[j, i] + " ");
            //    }
            //    str.AppendLine();
            //}

            //chart1.Series[0].Points.DataBindXY(MultiLayerPerceptron.Iteration, MultiLayerPerceptron.Error);
            //chart2.Series[0].Points.DataBindXY(MultiLayerPerceptron.Iteration, MultiLayerPerceptron.ChangeOnGlobalError);


            //for (int i = 0; i < 30; i++)
            //{
            //    str.Append((i+1).ToString() + ". Row\t");
            //    for (int j = 0; j < 6; j++)
            //    {
            //        str.Append(MultiLayerPerceptron.initial_weights[i, j] + "\t");
            //    }
            //    str.AppendLine();

            //}

            //str.AppendLine();

            //for (int j = 0; j < 6; j++)
            //{
            //    str.Append((j+1).ToString() + ". Row\t");
            //    str.Append(MultiLayerPerceptron.hidden_weights[j] );
            //    str.AppendLine();
            //}

           


        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
