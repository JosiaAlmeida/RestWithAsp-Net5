using RestAspeNet5.Data.Convert.Contract;
using RestAspeNet5.Data.VO;
using RestAspeNet5.Modals;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestAspeNet5.Data.Convert.Implementetion
{
    public class BooksConverter : IParser<BookVO, Books>, IParser<Books, BookVO>
    {
        public Books Parse(BookVO origin)
        {
            if (origin == null) return null;
            return new Books
            {
                ID = origin.ID,
                Title = origin.Title,
                Descricao = origin.Descricao,
                Autor = origin.Autor,
                Enable= origin.Enable
            };
        }
        public BookVO Parse(Books origin)
        {
            if (origin == null) return null;
            return new BookVO
            {
                ID = origin.ID,
                Title = origin.Title,
                Descricao = origin.Descricao,
                Autor = origin.Autor,
                Enable = origin.Enable
            };
        }
        public List<Books> Parse(List<BookVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
        public List<BookVO> Parse(List<Books> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
