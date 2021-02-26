using RestAspeNet5.Data.Convert.Implementetion;
using RestAspeNet5.Data.VO;
using RestAspeNet5.Hypermedia.Filters;
using RestAspeNet5.Modals;
using RestAspeNet5.Repository;
using RestAspeNet5.Repository.Generic;
using System.Collections.Generic;

namespace RestAspeNet5.Business.Implementacao
{
    public class BookImplementationBusiness : IBooksBusiness
    {
        private readonly IBookRepository _repository;
        private readonly BooksConverter _booksConverter;
        public BookImplementationBusiness(IBookRepository repository)
        {
            _repository = repository;
            _booksConverter = new BooksConverter();
        }
        List<BookVO> IBooksBusiness.FindAll()
        {
            return _booksConverter.Parse(_repository.FindAll());
        }
        BookVO IBooksBusiness.FindByID(long ID)
        {
            return _booksConverter.Parse(_repository.FindByID(ID));
        }
        public BookVO Create(BookVO book)
        {
            var bookEntity = _booksConverter.Parse(book);
            bookEntity = _repository.Create(bookEntity);
            return _booksConverter.Parse(bookEntity);
        }
        public BookVO Update(BookVO book)
        {
            var bookEntity = _booksConverter.Parse(book);
            bookEntity = _repository.Update(bookEntity);
            return _booksConverter.Parse(bookEntity);
        }
        public BookVO Disable(long id)
        {
            var bookEntity = _repository.Disable(id);
            return _booksConverter.Parse(bookEntity);
        }
        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public PageSearchVO<BookVO> FindWithPageSearch(string name, string SortDirection, int PageSize, int Page)
        {
            var offset = Page > 0 ? (Page - 1) * Page : 0;
            var sort = (!string.IsNullOrWhiteSpace(SortDirection)) &&
                (!SortDirection.Equals("desc")) ? "asc" : "desc";
            var Size = PageSize < 1 ? 1 : PageSize;
            string query = @"select * from Book b where 1=1";
            if (!string.IsNullOrWhiteSpace(name)) 
                query += $"and b.name like '%{name}%'";
            query += $" order by  b.name {sort} limit {Size} offset {offset}";

            string CountQuerys = @"select count(*) from Book b where 1=1";
            if (!string.IsNullOrWhiteSpace(name)) 
                CountQuerys += $"and b.name like '%{name}%'";

            var book = _repository.FindWithPageSearch(query);
            int totalResul = _repository.GetCoutn(CountQuerys);
            return new PageSearchVO<BookVO>
            {
                CurrentPage = Page,
                List = _booksConverter.Parse(book),
                PageSize = Size,
                SortDirections = sort,
                TotalResults = totalResul
            };
        }
    }
}
