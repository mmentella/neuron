using System;
using System.Collections.Generic;

namespace mm.ai.ml
{
    public sealed class TrainingSet
    {
        public TrainingSet(int inputVectorLength, int outputVectorLength)
        {
            // Initialize instance variables
            InputVectorLength = inputVectorLength;
            OutputVectorLength = outputVectorLength;
            TrainingSamples = new List<TrainingSample>();
        }

        public int InputVectorLength { get; }
        public int OutputVectorLength { get; }
        public IList<TrainingSample> TrainingSamples { get; }

        public void Add(TrainingSample sample)
        {
            // Validation
            if (sample.InputVector.Length != InputVectorLength)
            {
                throw new ArgumentException
                    ("Input vector must be of size " + InputVectorLength, "sample");
            }
            if (sample.OutputVector.Length != OutputVectorLength)
            {
                throw new ArgumentException
                    ("Output vector must be of size " + OutputVectorLength, "sample");
            }

            // Note that the reference is being added. (Sample is immutable)
            TrainingSamples.Add(sample);
        }

        public bool Remove(double[] inputVector)
        {
            return Remove(new TrainingSample(inputVector));
        }

        public bool Remove(TrainingSample sample)
        {
            return TrainingSamples.Remove(sample);
        }

        public bool Contains(double[] inputVector)
        {
            return Contains(new TrainingSample(inputVector));
        }

        public bool Contains(TrainingSample sample)
        {
            return TrainingSamples.Contains(sample);
        }

        public void Clear()
        {
            TrainingSamples.Clear();
        }

    }

}
