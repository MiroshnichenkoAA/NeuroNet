namespace Miroshnichenko.NeuralNetwork.WeightsInitializer
{
    public interface IWeightsInitializer
    {
        public double[][] Initialize(int prevLayerUnits, int units, int layerIndex);
    }
}