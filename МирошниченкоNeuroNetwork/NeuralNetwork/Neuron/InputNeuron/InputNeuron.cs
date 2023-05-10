using Miroshnichenko.NeuralNetwork.Layer;

namespace Miroshnichenko.NeuralNetwork.Neuron.InputNeuron
{
    public class InputNeuron : Neuron, IInputNeuron
    {
        public new double ActivationValue { get; set; }
        
        public InputNeuron() : base(null, null)
        {
        }

        public new void Compute(ILayer prevLayer)
        {
        }
    }
}