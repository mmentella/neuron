using System;
using System.Collections.Generic;
using System.Text;

namespace mm.ai.ml
{
    public interface INeuron
    {
        double Input { get; set; }
        double Output { get; }

        void AddSourceSynapse(ISynapse synapse);
        void AddTargetSynapse(ISynapse synapse);

        ILayer Parent { get; }

        void Run();

        void Learn(double learningRate);
    }
}
