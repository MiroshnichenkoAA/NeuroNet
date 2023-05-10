using Miroshnichenko.NeuralNetwork.Neuron;

namespace Miroshnichenko.NeuralNetwork.Layer
{
    public abstract class AbstractLayer : ILayer
    {
        public INeuron[] Neurons { get; }

        protected AbstractLayer(int units)
        {
            Neurons = new INeuron[units];
        }
    }
}