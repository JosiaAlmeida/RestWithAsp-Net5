using RestAspeNet5.Modals;
using RestAspeNet5.Modals.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RestAspeNet5.Service.Implementacao
{
    public class BooksImplementationService : IBooksService
    {
        private MySQLContext _context;
        public BooksImplementationService(MySQLContext context)
        {
            _context = context;
        }
        public List<Books> FindAll()
        {
            //Retorna a lista
            return _context.books.ToList();
        }

        public Books FindByID(long id)
        {
            return _context.books.SingleOrDefault(pers => pers.ID.Equals(id));
        }

        public Books Create(Books book)
        {
            try
            {
                _context.Add(book);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return book;
        }
        public Books Update(Books book)
        {
            if (!Exist(book.ID)) return null;
            var result= _context.books.SingleOrDefault(pers => pers.ID.Equals(book.ID));
            if (result != null)
            {
                try
                {
                    _context.Entry(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return book;
        }
        public void Delete(long id)
        {
            var result = _context.books.SingleOrDefault(pers => pers.ID.Equals(id));
            if (result != null)
            {
                try
                {
                    _context.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        private bool Exist(long id)
        {
            return _context.books.Any(pers => pers.ID.Equals(id));
        }
       
    }
}
