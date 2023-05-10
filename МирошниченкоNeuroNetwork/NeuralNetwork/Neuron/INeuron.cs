using Miroshnichenko.NeuralNetwork.Layer;

namespace Miroshnichenko.NeuralNetwork.Neuron
{
    public interface INeuron
    {
        double ActivationValue { get; }
        double NeuronValue { get; }
        double[] Weights { get; }
        double Delta { get; set; }
        void Compute(ILayer prevLayer);
        void Correct(double learningRate, ILayer prevLayer);
    }
}