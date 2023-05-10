namespace Miroshnichenko.NeuralNetwork.LossFunction
{
    public interface ILossFunction
    {
        public double ComputeLoss(double[] currentOutput, double[] expectedOutput);
    }
}