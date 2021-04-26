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
            throw new NotImplementedException();
        }

        public void Learn(TrainingSample trainingSample, int currentIteration, int trainingEpochs)
        {
            throw new NotImplementedException();
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
