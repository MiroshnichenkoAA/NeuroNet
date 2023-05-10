namespace Miroshnichenko.NeuralNetwork.Layer.InputLayer
{
    public interface IInputLayer : ILayer
    {
        void Feed(double[] inputData);
    }
}