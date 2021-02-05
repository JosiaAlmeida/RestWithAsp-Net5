using RestAspeNet5.Modals;
using RestAspeNet5.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspeNet5.Business.Implementacao
{
    public class PersonImplementationBusiness : IPersonBusiness
    {
        private readonly IPersonService _person;
        public PersonImplementationBusiness(IPersonService person)
        {
            _person = person;
        }
        public List<Person> FindAll()
        {
            return _person.FindAll();
        }

        public Person FindByID(long ID)
        {
            return _person.FindByID(ID);
        }

        public Person Create(Person person)
        {
            return _person.Create(person);
        }
        public Person Update(Person person)
        {
            return _person.Update(person);
        }
        public void Delete(long id)
        {
            _person.Delete(id);
        }
    }
}
