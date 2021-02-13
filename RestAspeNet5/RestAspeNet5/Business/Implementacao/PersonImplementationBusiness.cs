using RestAspeNet5.Data.Convert.Implementetion;
using RestAspeNet5.Data.VO;
using RestAspeNet5.Modals;
using RestAspeNet5.Repository.Generic;
using System.Collections.Generic;

namespace RestAspeNet5.Business.Implementacao
{
    public class PersonImplementationBusiness : IPersonBusiness
    {
        private readonly IRepository<Person> _repository;
        private readonly IPersonConverter _converter;
        public PersonImplementationBusiness(IRepository<Person> repository)
        {
            _repository = repository;
            _converter = new IPersonConverter();
        }
        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public PersonVO FindByID(long ID)
        {
            return _converter.Parse(_repository.FindByID(ID));
        }

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }
        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity= _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }
        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
