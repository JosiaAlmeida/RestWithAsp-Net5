using RestAspeNet5.Data.Convert.Implementetion;
using RestAspeNet5.Data.VO;
using RestAspeNet5.Modals;
using RestAspeNet5.Repository.Generic;
using System.Collections.Generic;

namespace RestAspeNet5.Business.Implementacao
{
    public class BookImplementationBusiness : IBooksBusiness
    {
        private readonly IRepository<Books> _repository;
        private readonly BooksConverter _booksConverter;
        public BookImplementationBusiness(IRepository<Books> repository)
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
        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
