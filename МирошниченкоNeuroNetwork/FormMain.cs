using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using Miroshnichenko.NeuralNetwork.ActivationFunction;
using Miroshnichenko.NeuralNetwork.ActivationFunction.SigmoidActivationFunction;
using Miroshnichenko.NeuralNetwork.ActivationFunction.TanhActivationFunction;
using Miroshnichenko.NeuralNetwork.LossFunction.EuclideanDistanceLoss;
using Miroshnichenko.NeuralNetwork.WeightsInitializer.DefaultWeightsInitializer;
using static Miroshnichenko.NeuralNetwork.NeuralNetwork;

namespace Miroshnichenko
{
    public partial class FormMain : Form
    {
        private readonly double[] _inputArray = new double[15];

        public static Label LabelNeurons;
        public static Label LabelCount;
        public static Label LabelAnswer;
        public static Label LabelEp;
        public static TextBox TextTrain;
        public static NumericUpDown Count;
        public static Chart Chart1;
        public static Series ser;
        private NeuralNetwork.NeuralNetwork _nn;
        private string addTrainFile = null;

        private double[][] _inputData =
        {
            new[]
            {
                1.0, 1.0, 1.0,
                1.0, 0.0, 1.0,
                1.0, 0.0, 1.0,
                1.0, 0.0, 1.0,
                1.0, 1.0, 1.0
            },
            new[]
            {
                0.0, 0.0, 1.0,
                0.0, 0.0, 1.0,
                0.0, 0.0, 1.0,
                0.0, 0.0, 1.0,
                0.0, 0.0, 1.0
            },
            new[]
            {
                1.0, 1.0, 1.0,
                0.0, 0.0, 1.0,
                1.0, 1.0, 1.0,
                1.0, 0.0, 0.0,
                1.0, 1.0, 1.0
            },
            new[]
            {
                1.0, 1.0, 1.0,
                0.0, 0.0, 1.0,
                1.0, 1.0, 1.0,
                0.0, 0.0, 1.0,
                1.0, 1.0, 1.0
            },
            new[]
            {
                1.0, 0.0, 1.0,
                1.0, 0.0, 1.0,
                1.0, 1.0, 1.0,
                0.0, 0.0, 1.0,
                0.0, 0.0, 1.0
            },
            new[]
            {
                1.0, 1.0, 1.0,
                1.0, 0.0, 0.0,
                1.0, 1.0, 1.0,
                0.0, 0.0, 1.0,
                1.0, 1.0, 1.0
            },
            new[]
            {
                1.0, 1.0, 1.0,
                1.0, 0.0, 0.0,
                1.0, 1.0, 1.0,
                1.0, 0.0, 1.0,
                1.0, 1.0, 1.0
            },
            new[]
            {
                1.0, 1.0, 1.0,
                0.0, 0.0, 1.0,
                0.0, 1.0, 1.0,
                0.0, 0.0, 1.0,
                0.0, 0.0, 1.0
            },
            new[]
            {
                1.0, 1.0, 1.0,
                1.0, 0.0, 1.0,
                1.0, 1.0, 1.0,
                1.0, 0.0, 1.0,
                1.0, 1.0, 1.0
            },
            new[]
            {
                1.0, 1.0, 1.0,
                1.0, 0.0, 1.0,
                1.0, 1.0, 1.0,
                0.0, 0.0, 1.0,
                1.0, 1.0, 1.0
            },
            new[]
            {
                0.0, 0.0, 0.0,
                0.0, 0.0, 0.0,
                0.0, 0.0, 0.0,
                0.0, 0.0, 0.0,
                0.0, 0.0, 0.0
            }
        };

        private double[][] _outputData =
        {
            new[] {1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0},
            new[] {0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0},
            new[] {0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0},
            new[] {0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0},
            new[] {0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0},
            new[] {0.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0},
            new[] {0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0},
            new[] {0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0},
            new[] {0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0},
            new[] {0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0},
            new[] {0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0},
        };


        public FormMain()
        {
            InitializeComponent();
            LabelNeurons = labelEvaluationValue;
            Count = numericUpDown1;
            LabelEp = label2;
            LabelAnswer = label3;
            Chart1 = chart1;
            CreateNeuralNetwork(NeuralNetwork.NeuralNetwork.InitType.SETLOCAL);
        }
        private void CreateNeuralNetwork(InitType type)
        {
            _nn = Builder()
                .InputLayer(15)
                .HiddenLayer(71, new LeakyReLUActivationFunction())
                .HiddenLayer(32, new LeakyReLUActivationFunction())
                .OutputLayer(10, new SigmoidActivationFunction())
                .LossFunction(new EuclideanDistanceLoss())
                .Build();
            _nn.Initialize(type, new DefaultWeightsInitializer());
            Predict();
            Evaluate();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var thread2 = new Thread(Fit);
            thread2.Start();
        }

        private void Fit()
        {
            string[] seriesArray = { "chart" };
            if (ser == null)
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        ser = Chart1.Series.Add(seriesArray[0]);
                        Chart1.Series["chart"].LegendText = "График ошибки";
                        Chart1.Series["chart"].ChartType = SeriesChartType.Spline;
                        Chart1.Series["chart"].BorderWidth = 3;
                        Chart1.Palette = ChartColorPalette.Pastel;
                        Chart1.Series[0].IsVisibleInLegend = false;
                        Chart1.Series["chart"].IsVisibleInLegend = true;
                    }));
                }
                else
                {
                    ser = Chart1.Series.Add(seriesArray[0]);
                    Chart1.Series["chart"].LegendText = "График ошибки";
                    Chart1.Series["chart"].ChartType = SeriesChartType.Spline;
                    Chart1.Series["chart"].BorderWidth = 3;
                    Chart1.Palette = ChartColorPalette.Pastel;
                    Chart1.Series[0].IsVisibleInLegend = false;
                    Chart1.Series["chart"].IsVisibleInLegend = true;
                }
            }
            _nn.Fit(_inputData, _outputData, Convert.ToInt32(Count.Value), 0.05);
            Predict();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            CreateNeuralNetwork(NeuralNetwork.NeuralNetwork.InitType.SETLOCAL);
        }

        private void Evaluate()
        {
            labelEvaluationValue.Text = "Ошибка: 0" ;
        }
    
        private void Predict()
        {
            var prediction = _nn.Predict(_inputArray);
            var s = "";
            for (var i = 0; i < 10; i++)
            {
                s += i + ": " + prediction[i] + "\n";
            }
            string ans = "N";
            if (prediction.Max() > 0.05) ans = Array.IndexOf(prediction, prediction.Max()).ToString();
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    LabelAnswer.Text = ans;
                }));
            }
            else
            {
                LabelAnswer.Text = ans;
            }
        }

        private void btn_click(Button btn)
        {
            var buttonId = btn.TabIndex;
            btn.BackColor = _inputArray[buttonId] == 0.0 ? Color.IndianRed : Color.Empty;
            _inputArray[buttonId] = Math.Abs(1.0 - _inputArray[buttonId]);
            Predict();
        }
        
        private void button0_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }
        
        private void button6_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            btn_click(sender as Button);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            _nn.Initialize(NeuralNetwork.NeuralNetwork.InitType.WORKSET, new DefaultWeightsInitializer());
        }

        private void button18_Click(object sender, EventArgs e)
        {
            button18.Text = LabelAnswer.Text;
            button18.Font = new Font(button18.Font.Name, 30, button18.Font.Style);
        }


        private void button20_Click(object sender, EventArgs e)
        {
            //CreateNeuralNetwork(NeuralNetwork.NeuralNetwork.InitType.GET);
            _nn.Initialize(NeuralNetwork.NeuralNetwork.InitType.GET, new DefaultWeightsInitializer());
            Predict();
            Evaluate();
        }



        private void setTrainData(string name) {
            XmlDocument memory_doc = new XmlDocument();
            if (!File.Exists(System.IO.Path.Combine("Resources", name)))
            {
                XmlElement element1 = memory_doc.CreateElement("", "sets", "");
                memory_doc.AppendChild(element1);
                memory_doc.Save(System.IO.Path.Combine("Resources", name));
                memory_doc.Load(System.IO.Path.Combine("Resources", name));
            }
            else
            {
                memory_doc.Load(System.IO.Path.Combine("Resources", name));
            }
            var count = memory_doc.SelectNodes("sets/set").Count;
            _inputData = new double[count][];
            Array.Clear(_inputData, 0, count);
            _outputData = new double[count][];
            Array.Clear(_outputData, 0, count);
            XmlElement memory_el = memory_doc.DocumentElement;
            for (int i = 0; i < count; i++)
            {
                _outputData[i] = memory_el.ChildNodes.Item(i).LastChild.InnerText.Split(' ').Select(double.Parse).ToArray();
                _inputData[i] = memory_el.ChildNodes.Item(i).FirstChild.InnerText.Split(' ').Select(double.Parse).ToArray();
            }
            LabelCount.Text = count.ToString();

        }

   
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void labelEvaluationValue_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}