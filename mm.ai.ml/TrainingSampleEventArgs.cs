using System;
using System.Collections.Generic;
using System.Text;

namespace mm.ai.ml
{
    public class TrainingSampleEventArgs:EventArgs
    {
        public TrainingSampleEventArgs(int trainingIteration, TrainingSample trainingSample)
        {
            TrainingIteration = trainingIteration;
            TrainingSample = trainingSample;
        }

        public int TrainingIteration { get; }
        public TrainingSample TrainingSample { get; }
    }
}
