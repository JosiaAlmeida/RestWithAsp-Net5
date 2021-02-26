using RestAspeNet5.Modals;
using RestAspeNet5.Modals.Context;
using RestAspeNet5.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspeNet5.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySQLContext Context): base(Context) { }
        public Person Disable(long id)
        {
            if (!_context.Persons.Any(p=> p.ID.Equals(id))) return null;
            var user = _context.Persons.SingleOrDefault(p => p.ID.Equals(id));
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
        public List<Person> FindByName(string firstName, string seconName)
        {
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(seconName))
            {
                return _context.Persons.Where(p => p.FirstName.Contains(firstName)
                    && p.LastName.Contains(seconName)).ToList();
            }
            else if (!string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(seconName))
            {
                return _context.Persons.Where(p => p.FirstName.Contains(firstName)).ToList();
            }
            else if (string.IsNullOrEmpty(firstName)
                && !string.IsNullOrEmpty(seconName))
                return _context.Persons.Where(p => p.LastName.Contains(seconName)).ToList();
            else
                // return _context.Persons.ToList();
                return null;
        }
    }
}
