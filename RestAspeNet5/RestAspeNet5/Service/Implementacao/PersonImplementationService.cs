using RestAspeNet5.Modals;
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
        public Person Create(Person person)
        {
            return person;
        }


        public List<Person> FindAll()
        {
            List<Person> persons = new List<Person>();
            for (int i = 0; i < 8; i++)
            {
                Person person = MockPerson(i);
                persons.Add(person);
            }
            return persons;
        }

        public Person FindByID(long ID)
        {
            return new Person
            {
                ID = IncrementAndGet(),
                FirstName = "Josia",
                LastName = "Almeida",
                Adress = "Luanda",
                Gender = "Macho Alfa"
            };
        }

        public Person Update(Person person)
        {
            return person;
        }
        private Person MockPerson(int i)
        {
            return new Person
            {
                ID = IncrementAndGet(),
                FirstName = "Person Name" + i,
                LastName = "Person Last Name" + i,
                Adress = "Some Address" + i,
                Gender = "Male"
            };
        }
        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }

        public void Delete(long id)
        {}
    }
}
