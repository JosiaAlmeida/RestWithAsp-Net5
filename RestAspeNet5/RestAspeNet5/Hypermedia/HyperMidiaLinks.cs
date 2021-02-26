using System.Text;

namespace RestAspeNet5.Hypermedia
{
    public class HyperMidiaLinks
    {
        public string Ref { get; set; }
        private string href;
        public string Href {    
            get {
                object _lock = new object();
                lock (_lock)
                {
                    StringBuilder sb = new StringBuilder(href);
                    return sb.Replace("%2F", "/").ToString();
                }
            }
            set { this.href = value; } }
        public string Actions { get; set; }
        public string Type { get; set; }
    }
}
