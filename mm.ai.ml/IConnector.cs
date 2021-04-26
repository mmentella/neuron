using System.Collections.Generic;

namespace mm.ai.ml
{
    public interface IConnector
    {
        ILayer SourceLayer { get; }
        ILayer TargetLayer { get; }

        int SynapseCount { get; }

        IEnumerable<ISynapse> Synapses { get; }

        ConnectionMode ConnectionMode { get; }
        IInitializer Initializer { get; }

        void Initialize();
        void Jitter(double jitterNoiseLimit);
    }
}
