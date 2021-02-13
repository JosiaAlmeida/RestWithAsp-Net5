using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using RestAspeNet5.Hypermedia.Abstrat;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspeNet5.Hypermedia
{
    public abstract class ContentResponseInRicher<T> : IResponseEnricher where T : ISuporteHypermedia
    {
        public ContentResponseInRicher() { }
        public bool CanEnrich(Type Contentype)
        {
            //Pegando Objectos Simples
            return Contentype == typeof(T) || Contentype == typeof(List<T>);
        }
        protected abstract Task EnrichModel(T content, IUrlHelper urlHelper);
        bool IResponseEnricher.CanEnrich(ResultExecutingContext response)
        {
            if (response.Result is OkObjectResult okObjectResult)
            {
                return CanEnrich(okObjectResult.Value.GetType());
            }
            return false;
        }

        public async Task Enrich(ResultExecutingContext response)
        {
            var urlHelper = new UrlHelperFactory().GetUrlHelper(response);
            if (response.Result is OkObjectResult okObjectResult)
            {
                if (okObjectResult is T model)
                {
                    await EnrichModel(model, urlHelper);
                }

                else if (okObjectResult.Value is List<T> Collection)
                {
                    ConcurrentBag<T> Bag = new ConcurrentBag<T>(Collection);
                    Parallel.ForEach(Bag, (element) =>
                    {
                        EnrichModel(element, urlHelper);
                    });
                }
            }
            await Task.FromResult<object>(null);
        }
    }
}
