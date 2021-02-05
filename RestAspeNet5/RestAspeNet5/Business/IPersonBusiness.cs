using RestAspeNet5.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspeNet5.Business
{
    interface IPersonBusiness
    {
        Person Create(Person person);
        List<Person> FindAll();
        Person FindByID(long ID);
        Person Update(Person person);
        //Person Delete(Person person);
        void Delete(long id);
    }
}
