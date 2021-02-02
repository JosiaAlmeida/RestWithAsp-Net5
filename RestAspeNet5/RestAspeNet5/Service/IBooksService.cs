using RestAspeNet5.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspeNet5.Service
{
    public interface IBooksService
    {
        Books Create(Books personbooks);
        List<Books> FindAll();
        Books FindByID(long ID);
        Books Update(Books personbooks);
        //Person Delete(Person person);
        void Delete(long id);
    }
}
