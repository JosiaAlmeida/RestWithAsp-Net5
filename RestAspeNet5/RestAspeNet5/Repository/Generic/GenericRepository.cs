using RestAspeNet5.Modals.Base;
using RestAspeNet5.Modals.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspeNet5.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private MySQLContext _context;
        public GenericRepository(MySQLContext context)
        {
            _context = context;
        }
        public List<T> FindAll()
        {
            throw new NotImplementedException();
        }
        public T FindByID(long ID)
        {
            throw new NotImplementedException();
        }
        public T Create(T item)
        {
            throw new NotImplementedException();
        }
        public T Update(T item)
        {
            throw new NotImplementedException();
        }
        public void Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}
