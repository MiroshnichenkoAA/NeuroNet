using Miroshnichenko.NeuralNetwork.Layer.HiddenLayer;

namespace Miroshnichenko.NeuralNetwork.Layer.OutputLayer
{
    public interface IOutputLayer : IHiddenLayer
    {
        double[] Result { get; }
        void ComputeDelta(double[] expectedData);
    }
}