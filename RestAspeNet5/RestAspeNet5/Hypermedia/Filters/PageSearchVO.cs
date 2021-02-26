using RestAspeNet5.Hypermedia.Abstrat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspeNet5.Hypermedia.Filters
{
    public class PageSearchVO<T> where T : ISuporteHypermedia
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalResults { get; set; }
        public string SortFieldes { get; set; }
        public string SortDirections { get; set; }
        public Dictionary<string, object> Filters { get; set; }
        public List<T> List { get; set; }

        public PageSearchVO()
        {
        }

        public PageSearchVO(int currentPage, int pageSize, string sortFieldes, string sortDirections)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFieldes = sortFieldes;
            SortDirections = sortDirections;
        }

        public PageSearchVO(int currentPage, int pageSize, string sortFieldes, string sortDirections, Dictionary<string, object> filters) 
        {
            Filters = filters;
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortDirections = sortDirections;
            SortFieldes = sortFieldes;
        }

        public PageSearchVO(int currentPage, string sortFieldes, string sortDirections) 
            : this(currentPage, 10, sortFieldes, sortDirections){ }

        public int GetCurrentPage()
        {
            return this.CurrentPage == 0 ? 2 : CurrentPage;
        }
        public int GetPageSize()
        {
            return this.PageSize == 0 ? 10 : PageSize;
        }
    }
}
