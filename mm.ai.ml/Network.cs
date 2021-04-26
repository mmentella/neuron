using System;
using System.Collections.Generic;
using System.Linq;

namespace mm.ai.ml
{
    public abstract class Network
        : INetwork
    {
        private bool isStopping;

        protected Network(ILayer inputLayer, ILayer outputLayer, TrainingMethod trainingMethod)
        {
            InputLayer = inputLayer;
            OutputLayer = outputLayer;
            TrainingMethod = trainingMethod;

            JitterEpoch = 73;
            JitterNoiseLimit = 0.03d;

            Layers = new List<ILayer>();
            Connectors = new List<IConnector>();

            if (inputLayer == outputLayer)
            {
                throw new ArgumentException("Input and Output Layers must not be the same");
            }

            if(inputLayer.SourceConnectors!=null && inputLayer.SourceConnectors.Any())
            {
                throw new ArgumentException("Input Layer should not have any Input Connector");
            }

            if (outputLayer.TargetConnectors != null && outputLayer.TargetConnectors.Any())
            {
                throw new ArgumentException("Output Layer should not have any Output Connector");
            }

            Initialize();
        }

        public ILayer InputLayer { get; }

        public ILayer OutputLayer { get; }
        public TrainingMethod TrainingMethod { get; }

        public int LayerCount => Layers.Count();

        public IEnumerable<ILayer> Layers { get; }

        public int ConnectorCount => Connectors.Count();

        public IEnumerable<IConnector> Connectors { get; }

        public double JitterNoiseLimit { get; set; }
        public int JitterEpoch { get; set; }

        public event TrainingSampleEventHandler BeginSampleEvent;
        public event TrainingSampleEventHandler EndSampleEvent;
        public event TrainingEpochEventHandler EndEpochEvent;
        public event TrainingEpochEventHandler BeginEpochEvent;

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Learn(TrainingSet trainingSet, int trainingEpochs)
        {
            if ((trainingSet.InputVectorLength != InputLayer.Neurons.Count())
                || (TrainingMethod == TrainingMethod.Supervised && trainingSet.OutputVectorLength != OutputLayer.Neurons.Count())
                || (TrainingMethod == TrainingMethod.Unsupervised && trainingSet.OutputVectorLength != 0))
            {
                throw new ArgumentException("Invalid training set");
            }

            // Reset isStopping
            isStopping = false;

            // Re-Initialize the network
            Initialize();
            for (int currentIteration = 0; currentIteration < trainingEpochs; currentIteration++)
            {
                // Beginning a new training epoch
                OnBeginEpoch(currentIteration, trainingSet);

                // Check for Jitter Epoch
                if (JitterEpoch > 0 && currentIteration % JitterEpoch == 0)
                {
                    foreach (var connector in Connectors)
                    {
                        connector.Jitter(JitterNoiseLimit);
                    }
                }

                foreach(var sample in trainingSet.TrainingSamples.OrderBy(ts => Guid.NewGuid()))
                {
                    // Learn a random training sample
                    OnBeginSample(currentIteration, sample);
                    LearnSample(sample, currentIteration, trainingEpochs);
                    OnEndSample(currentIteration, sample);

                    // Check if we need to stop
                    if (isStopping) { isStopping = false; return; }
                }

                // Training Epoch successfully complete
                OnEndEpoch(currentIteration, trainingSet);

                // Check if we need to stop
                if (isStopping) { isStopping = false; return; }
            }
        }

        public void Learn(TrainingSample trainingSample, int currentIteration, int trainingEpochs)
        {
            OnBeginSample(currentIteration, trainingSample);
            LearnSample(trainingSample, currentIteration, trainingEpochs);
            OnEndSample(currentIteration, trainingSample);
        }

        public double[] Run(double[] input)
        {
            throw new NotImplementedException();
        }

        public void StopLearning()
        {
            isStopping = true;
        }

        protected virtual void OnBeginEpoch(int currentIteration, TrainingSet trainingSet)
        {
            BeginEpochEvent?.Invoke(this, new TrainingEpochEventArgs(currentIteration, trainingSet));
        }

        protected virtual void OnEndEpoch(int currentIteration, TrainingSet trainingSet)
        {
            if (EndEpochEvent != null)
            {
                EndEpochEvent(this, new TrainingEpochEventArgs(currentIteration, trainingSet));
            }
        }

        protected virtual void OnBeginSample(int currentIteration, TrainingSample currentSample)
        {
            BeginSampleEvent?.Invoke(this, new TrainingSampleEventArgs(currentIteration, currentSample));
        }

        protected virtual void OnEndSample(int currentIteration, TrainingSample currentSample)
        {
            EndSampleEvent?.Invoke(this, new TrainingSampleEventArgs(currentIteration, currentSample));
        }

        protected abstract void LearnSample(TrainingSample trainingSample, int currentIteration, int trainingEpochs);
    }
}
