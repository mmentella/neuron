using System;
using System.Collections.Generic;
using System.Text;

namespace mm.ai.ml
{
    public interface ISynapse
    {
        double Weigth { get; }

        IConnector Parent { get; }

        INeuron SourceNeuron { get; }
        INeuron TargetNeuron { get; }

        void Propagate();
        void OptimizeWeigth(double learningRate);
        void Jitter(double jitterNoiseLimit);
    }
}
