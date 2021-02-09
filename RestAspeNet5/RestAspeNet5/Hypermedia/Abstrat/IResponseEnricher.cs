using Microsoft.AspNetCore.Mvc.Filters;

namespace RestAspeNet5.Hypermedia
{
    public interface IResponseEnricher
    {
        bool CanEnrich(ResultExecutedContext context);
        bool Enrich(ResultExecutedContext context);
    }
}
