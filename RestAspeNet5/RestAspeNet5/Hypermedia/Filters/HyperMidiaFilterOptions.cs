using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspeNet5.Hypermedia.Filters
{
    public class HyperMidiaFilterOptions
    {
        public List<IResponseEnricher> ContentResponsiveEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
