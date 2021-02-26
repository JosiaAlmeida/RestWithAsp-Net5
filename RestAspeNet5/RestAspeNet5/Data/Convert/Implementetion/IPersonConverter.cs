using RestAspeNet5.Data.Convert.Contract;
using RestAspeNet5.Data.VO;
using RestAspeNet5.Modals;
using System.Collections.Generic;
using System.Linq;

namespace RestAspeNet5.Data.Convert.Implementetion
{
    public class IPersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO>
    {
        public Person Parse(PersonVO origin)
        {
            if (origin == null) return null;
            return new Person
            {
                ID = origin.ID,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Adress = origin.Adress,
                Gender = origin.Gender,
                Enable= origin.Enable
            };
        }
        public PersonVO Parse(Person origin)
        {
            if (origin == null) return null;
            return new PersonVO
            {
                ID = origin.ID,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Adress = origin.Adress,
                Gender = origin.Gender,
                Enable = origin.Enable
            };
        }

        public List<Person> Parse(List<PersonVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<PersonVO> Parse(List<Person> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
