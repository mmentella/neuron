using System;
using System.Collections.Generic;
using System.Text;

namespace mm.ai.ml
{
    public interface ILearningRateFunction
    {
        double InitialLearningRate { get; }
        double FinalLearningRate { get; }
        double GetLearningRate(int currentIteration, int trainingEpochs);
    }
}
