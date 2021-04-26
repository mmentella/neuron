using System;

namespace mm.ai.ml
{
    public class TrainingEpochEventArgs : EventArgs
    {
        public TrainingEpochEventArgs(int trainingIteration, TrainingSet trainingSet)
        {
            this.TrainingSet = trainingSet;
            this.TrainingIteration = trainingIteration;
        }

        public TrainingSet TrainingSet { get; }
        public int TrainingIteration { get; }
    }
}