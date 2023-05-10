using Miroshnichenko.NeuralNetwork.Neuron;

namespace Miroshnichenko.NeuralNetwork.Layer
{
    public interface ILayer
    {
        INeuron[] Neurons { get; }
    }
}