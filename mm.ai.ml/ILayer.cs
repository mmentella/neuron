using System;
using System.Collections.Generic;
using System.Text;

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
        void SetLearningRate(double learningRate);
        void SetLearningRate(double initialLearningRate, double finalLearningRate);
        void SetLearningRate(ILearningRateFunction learningRateFunction);

        void SetInput(double[] input);
        double[] GetOutput();

        void Run();
        void Learn(int currentIteration, int trainingEpochs);
    }
}
