using RestAspeNet5.Modals;
using RestAspeNet5.Modals.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RestAspeNet5.Service.Implementacao
{
    public class PersonImplementationService : IPersonService
    {

        private volatile int count;
        private MySQLContext _context;
        public PersonImplementationService(MySQLContext context)
        {
            _context = context;
        }
        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return person;
        }


        public List<Person> FindAll()
        {
            //Retorna a lista
            return _context.Persons.ToList();
        }

        public Person FindByID(long id)
        {
            return _context.Persons.SingleOrDefault(pers => pers.ID.Equals(id));
        }

        public Person Update(Person person)
        {
            if (!Exist(person.ID)) return new Person();
            var result= _context.Persons.SingleOrDefault(pers => pers.ID.Equals(person.ID));
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
            return person;
        }

        private bool Exist(object id)
        {
            return _context.Persons.Any(pers => pers.ID.Equals(id));
        }
        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }

        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(pers => pers.ID.Equals(id));
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
    }
}
