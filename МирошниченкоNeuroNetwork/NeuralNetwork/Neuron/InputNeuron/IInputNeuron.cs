namespace Miroshnichenko.NeuralNetwork.Neuron.InputNeuron
{
    public interface IInputNeuron : INeuron
    {
        new double ActivationValue { get; set; }
    }
}