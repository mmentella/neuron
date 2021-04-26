using System;
using System.Collections.Generic;

namespace mm.ai.ml
{
    public abstract class Layer<TNeuron>
        : ILayer
        where TNeuron : INeuron
    {
        protected readonly TNeuron[] neurons;

        protected Layer(int neuronCount)
        {
            neurons = new TNeuron[neuronCount];
            //LearningRateFunction = new LinearFunction(0.3d, 0.05d);
        }

        public INeuron this[int index] => neurons[index];

        public IEnumerable<INeuron> Neurons
        {
            get
            {
                for (int i = 0; i < neurons.Length; i++)
                {
                    yield return neurons[i];
                }
            }
        }

        public IEnumerable<IConnector> SourceConnectors { get; } = new List<IConnector>();

        public IEnumerable<IConnector> TargetConnectors { get; } = new List<IConnector>();

        public IInitializer Initializer { get; }

        public double LearningRate => LearningRateFunction.InitialLearningRate;

        public ILearningRateFunction LearningRateFunction { get; }

        IEnumerable<INeuron> ILayer.Neurons
        {
            get
            {
                for (int i = 0; i < neurons.Length; i++)
                {
                    yield return neurons[i];
                }
            }
        }

        INeuron ILayer.this[int index]
        {
            get { return neurons[index]; }
        }

        public double[] GetOutput()
        {
            double[] output = new double[neurons.Length];
            for (int i = 0; i < neurons.Length; i++)
            {
                output[i] = neurons[i].Output;
            }
            return output;
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Learn(int currentIteration, int trainingEpochs)
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            throw new NotImplementedException();
        }

        public virtual void SetInput(double[] input)
        {
            if (neurons.Length != input.Length)
            {
                throw new ArgumentException("Length of input array should be same as neuron count", "input");
            }

            // Bind inputs
            for (int i = 0; i < neurons.Length; i++)
            {
                neurons[i].Input = input[i];
            }
        }
    }
}
