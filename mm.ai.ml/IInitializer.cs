using System;
using System.Collections.Generic;
using System.Text;

namespace mm.ai.ml
{
    public interface IInitializer
    {
        void Initialize(ILayer layer);
    }
}
