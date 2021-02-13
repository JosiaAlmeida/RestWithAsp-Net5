using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspeNet5.Hypermedia.Filters
{
    public class HyperMidiaFilter:ResultFilterAttribute
    {
        private readonly HyperMidiaFilterOptions _hyperMidiaFilterOptions;
        public HyperMidiaFilter(HyperMidiaFilterOptions hyperMidiaFilterOptions) {
            _hyperMidiaFilterOptions = hyperMidiaFilterOptions;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            //Processa um enricher e tenta adicionar os links
            TryEnricherResult(context);
            base.OnResultExecuting(context);
        }
        //Converte os objectos
        private void TryEnricherResult(ResultExecutingContext context)
        {
            if(context.Result is OkObjectResult objectResult)
            {
                var enricher = _hyperMidiaFilterOptions
                    .ContentResponsiveEnricherList
                    .FirstOrDefault(x => x.CanEnrich(context));
                //Converte os objectos
                if (enricher != null) Task.FromResult(enricher.Enrich(context));
            }
            
        }
    }
}
