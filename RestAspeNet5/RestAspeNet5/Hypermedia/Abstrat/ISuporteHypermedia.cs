using System.Collections.Generic;

namespace RestAspeNet5.Hypermedia.Abstrat
{
    public interface ISuporteHypermedia
    {
        List<HyperMidiaLinks> Links { get; set; }
    }
}
