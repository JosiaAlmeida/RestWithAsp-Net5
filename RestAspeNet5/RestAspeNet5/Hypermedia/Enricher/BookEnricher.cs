using Microsoft.AspNetCore.Mvc;
using RestAspeNet5.Data.VO;
using RestAspeNet5.Hypermedia.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAspeNet5.Hypermedia.Enricher
{
    public class BookEnricher : ContentResponseInRicher<BookVO>
    {
        private readonly object _lock = new object();
        protected override Task EnrichModel(BookVO content, IUrlHelper urlHelper)
        {
            var path = "api/BooksControll/v1";
            string link = getLink(content.ID, urlHelper, path);
            content.Links.Add(new HyperMidiaLinks()
            {
                Actions = HttpVerbsContract.GET,
                Href= link,
                Ref=RelationType.self,
                Type = ResponseTypeFormatters.DefaultGET
            });
            content.Links.Add(new HyperMidiaLinks()
            {
                Actions = HttpVerbsContract.POST,
                Href = link,
                Ref = RelationType.self,
                Type = ResponseTypeFormatters.DefaultPOST
            });
            content.Links.Add(new HyperMidiaLinks()
            {
                Actions = HttpVerbsContract.PUT,
                Href = link,
                Ref = RelationType.self,
                Type = ResponseTypeFormatters.DefaultPUT
            });
            content.Links.Add(new HyperMidiaLinks()
            {
                Actions = HttpVerbsContract.PATCH,
                Href = link,
                Ref = RelationType.self,
                Type = ResponseTypeFormatters.DefaultPATCH
            });
            content.Links.Add(new HyperMidiaLinks()
            {
                Actions = HttpVerbsContract.DELETE,
                Href = link,
                Ref = RelationType.self,
                Type = "int"
            });
            return null;
        }

        private string getLink(long iD, IUrlHelper urlHelper, string path)
        {
            lock (_lock)
            {
                var url = new { Controller = path, ID = iD };
                return new StringBuilder(urlHelper.Link("Defaultapi", url)).Replace("%2F", "/").ToString();
            };
        }
    }
}
