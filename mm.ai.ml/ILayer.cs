using System.Collections.Generic;

namespace mm.ai.ml
{
    public interface ILayer
    {
        IEnumerable<INeuron> Neurons { get; }
        INeuron this[int index] { get; }
        IEnumerable<IConnector> SourceConnectors { get; }
        IEnumerable<IConnector> TargetConnectors { get; }

        IInitializer Initializer { get; }

        double LearningRate { get; }

        ILearningRateFunction LearningRateFunction { get; }

        void Initialize();

        void SetInput(double[] input);
        double[] GetOutput();

        void Run();
        void Learn(int currentIteration, int trainingEpochs);
    }
}
