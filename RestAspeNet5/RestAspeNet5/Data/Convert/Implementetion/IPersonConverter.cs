using RestAspeNet5.Data.Convert.Contract;
using RestAspeNet5.Data.VO;
using RestAspeNet5.Modals;
using System.Collections.Generic;
using System.Linq;

namespace RestAspeNet5.Data.Convert.Implementetion
{
    public class IPersonConverter : IParser<IPersonVO, Person>, IParser<Person, IPersonVO>
    {
        public Person Parse(IPersonVO origin)
        {
            if (origin == null) return null;
            return new Person
            {
                ID = origin.ID,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Adress = origin.Adress,
                Gender = origin.Gender
            };
        }
        public IPersonVO Parse(Person origin)
        {
            if (origin == null) return null;
            return new IPersonVO
            {
                ID = origin.ID,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Adress = origin.Adress,
                Gender = origin.Gender
            };
        }

        public List<Person> Parse(List<IPersonVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<IPersonVO> Parse(List<Person> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
