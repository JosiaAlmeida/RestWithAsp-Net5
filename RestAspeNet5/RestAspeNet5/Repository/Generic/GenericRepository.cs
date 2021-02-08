using Microsoft.EntityFrameworkCore;
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
        private DbSet<T> dataset;
        public GenericRepository(MySQLContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }
        public List<T> FindAll()
        {
            return dataset.ToList();
        }
        public T FindByID(long id)
        {
            return dataset.SingleOrDefault(pers => pers.ID.Equals(id));
        }
        public T Create(T item)
        {
            //Trocando id de ids iguais
            if (item.ID == item.ID){
                dataset.SingleOrDefault(pers => pers.ID.Equals(item.ID + 1));
                try
                {
                    dataset.Add(item);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
                return item;
            }
            else
            {
                try
                {
                    dataset.Add(item);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
                return item;
            }
            
        }
        public T Update(T item)
        {

            if (!Exist(item.ID)) return null;
            var result = dataset.SingleOrDefault(pers => pers.ID.Equals(item.ID));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item.ID);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
                return null;

            return result;
        }
        public void Delete(long id)
        {
            var result = dataset.SingleOrDefault(pers => pers.ID.Equals(id));
            if (result != null)
            {
                try
                {
                    dataset.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            if (result == result)
            {
                try
                {
                    dataset.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        private bool Exist(long iD)
        {
            return dataset.Any(pers => pers.ID.Equals(iD));
        }
    }
}
