using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace RestAspeNet5.Hypermedia
{
    public interface IResponseEnricher
    {
        //Em casos de bug utiliza o ResultExecutingContext em CanEnrich
        bool CanEnrich(ResultExecutingContext context);
        Task Enrich(ResultExecutingContext context);
        //bool CanEnrich(ResultExecutingContext response);
    }
}
