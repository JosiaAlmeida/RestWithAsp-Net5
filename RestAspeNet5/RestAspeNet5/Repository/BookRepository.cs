using RestAspeNet5.Modals;
using RestAspeNet5.Modals.Context;
using RestAspeNet5.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspeNet5.Repository
{
    public class BookRepository : GenericRepository<Books>, IBookRepository
    {
        public BookRepository(MySQLContext Context): base(Context) { }
        public Books Disable(long id)
        {
            if (!_context.books.Any(b=> b.ID.Equals(id))) return null;
            var user = _context.books.SingleOrDefault(b => b.ID.Equals(id));
            if (user != null)
            {
                user.Enable = false;
                try
                {
                    _context.Entry(user).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return user;

        }
    }
}
