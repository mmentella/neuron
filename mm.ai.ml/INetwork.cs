using System.Collections.Generic;

namespace mm.ai.ml
{
    public interface INetwork
    {
        ILayer InputLayer { get; }
        ILayer OutputLayer { get; }

        int LayerCount { get; }
        IEnumerable<ILayer> Layers { get; }

        int ConnectorCount { get; }
        IEnumerable<IConnector> Connectors { get; }
        double JitterNoiseLimit { get; set; }
        int JitterEpoch { get; set; }

        event TrainingSampleEventHandler BeginSampleEvent;
        event TrainingSampleEventHandler EndSampleEvent;
        event TrainingEpochEventHandler BeginEpochEvent;
        event TrainingEpochEventHandler EndEpochEvent;

        void Initialize();

        double[] Run(double[] input);

        void Learn(TrainingSet trainingSet, int trainingEpochs);
        void Learn(TrainingSample trainingSample, int currentIteration, int trainingEpochs);

        void StopLearning();
    }
}
