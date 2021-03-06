namespace mm.ai.ml
{
    public interface ILearningRateFunction
    {
        double InitialLearningRate { get; }
        double FinalLearningRate { get; }
        double GetLearningRate(int currentIteration, int trainingEpochs);
    }
}
