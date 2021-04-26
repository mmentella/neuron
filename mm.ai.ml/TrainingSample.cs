using System;
using System.Linq;

namespace mm.ai.ml
{
    public class TrainingSample
    {
        public TrainingSample(double[] vector)
            : this(vector, new double[0])
        {
        }


        public TrainingSample(double[] inputVector, double[] outputVector)
        {
            // Clone and initialize
            InputVector = (double[])inputVector.Clone();
            OutputVector = (double[])outputVector.Clone();

            // Some neural networks require inputs in normalized form.
            // As an optimization measure, we normalize and store training samples
            NormalizedInputVector = Standardize(inputVector);
            NormalizedOutputVector = Standardize(outputVector);
        }

        public double[] InputVector { get; }
        public double[] OutputVector { get; }
        public double[] NormalizedInputVector { get; }
        public double[] NormalizedOutputVector { get; }

        private double[] Standardize(double[] vector)
        {
            var mean = vector.Sum() / vector.Length;
            var std = Math.Sqrt(vector.Select(v => Math.Pow(v - mean, 2))
                                      .Sum() / vector.Length);
            return vector.Select(v => (v - mean) / std).ToArray();
        }
    }
}
