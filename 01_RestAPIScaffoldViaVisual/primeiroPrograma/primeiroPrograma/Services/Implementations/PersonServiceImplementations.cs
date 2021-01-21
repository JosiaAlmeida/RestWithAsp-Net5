using primeiroPrograma.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace primeiroPrograma.Services.Implementations
{
    public class PersonServiceImplementations : IPersonServices
    {
        private volatile int count;

        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {
            return;
        }

        public List<Person> FindAll()
        {
            List<Person> persons = new List<Person>();
            for(int i =0; i<8; i++)
            {
                Person person = MockPerson(i);
                persons.Add(person);
            }
            return persons;
        }

        
        public Person FindById(long id)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Josia",
                LastName = "Almeida",
                Address = "Luanda",
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
                Id = IncrementAndGet(),
                FirstName = "Person Name" +i,
                LastName = "Person Last Name" +i,
                Address = "Some Address" +i,
                Gender = "Male"
            };
        }
        //Troca de Id
        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }

    }
}
