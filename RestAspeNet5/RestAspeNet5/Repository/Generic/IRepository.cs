using RestAspeNet5.Modals;
using RestAspeNet5.Modals.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspeNet5.Repository.Generic
{
    public interface IRepository<T> where T: BaseEntity 
    {
        T Create(T item);
        List<T> FindAll();
        T FindByID(long ID);
        T Update(T item);
        //T Delete(T item);
        void Delete(long id);
        List<T> FindWithPageSearch(string query);
        int GetCoutn(string query);
    }
}
