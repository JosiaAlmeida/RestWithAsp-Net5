using RestAspeNet5.Hypermedia;
using RestAspeNet5.Hypermedia.Abstrat;
using RestAspeNet5.Hypermedia.Filters;
using System.Collections.Generic;

namespace RestAspeNet5.Data.VO
{
    public class BookVO: ISuporteHypermedia
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Descricao { get; set; }
        public string Autor { get; set; }
        public bool Enable { get; set; }
        public List<HyperMidiaLinks> Links { get; set; } = new List<HyperMidiaLinks>();
    }
}
