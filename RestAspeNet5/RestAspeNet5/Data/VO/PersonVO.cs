using RestAspeNet5.Hypermedia;
using RestAspeNet5.Hypermedia.Abstrat;
using System.Collections.Generic;

namespace RestAspeNet5.Data.VO
{
    public class PersonVO: ISuporteHypermedia
    {
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Adress { get; set; }
        public List<HyperMidiaLinks> Links { get; set; } = new List<HyperMidiaLinks>();
    }
}
