using RestAspeNet5.Data.VO;
using RestAspeNet5.Hypermedia.Filters;
using RestAspeNet5.Modals;
using RestAspeNet5.Repository.Generic;
using System.Collections.Generic;

namespace RestAspeNet5.Business
{
    public interface IBooksBusiness
    {
        BookVO Create(BookVO book);
        List<BookVO> FindAll();
        BookVO FindByID(long ID);
        BookVO Update(BookVO book);
        BookVO Disable(long id);
        void Delete(long id);
        PageSearchVO<BookVO> FindWithPageSearch(string name,
            string SortDirection, int PageSize, int Page);
    }
}
