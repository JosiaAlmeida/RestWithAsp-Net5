using RestAspeNet5.Data.Convert.Implementetion;
using RestAspeNet5.Data.VO;
using RestAspeNet5.Hypermedia.Filters;
using RestAspeNet5.Modals;
using RestAspeNet5.Repository;
using RestAspeNet5.Repository.Generic;
using System.Collections.Generic;

namespace RestAspeNet5.Business.Implementacao
{
    public class PersonImplementationBusiness : IPersonBusiness
    {
        private readonly IPersonRepository _repository;
        private readonly IPersonConverter _converter;
        public PersonImplementationBusiness(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new IPersonConverter();
        }
        //Paginação
        public PageSearchVO<PersonVO> FindWithPageSearch(string name, 
            string SortDirection, int PageSize, int Page)
        {
            //Condição das variaveis
            var offset = Page > 0 ? (Page - 1) * PageSize : 0;
            var sort= (!string.IsNullOrWhiteSpace(SortDirection)) && 
                (!SortDirection.Equals("desc")) ?
                "asc" : "desc";
            var size = PageSize < 1 ? 1 : PageSize;
            string CountQuery = "";
            string query = @"select * from person p where 1=1";

            if (!string.IsNullOrWhiteSpace(name))
                query += $" and p.name like '%{name}%'";

            query += $" order by p.firstname {sort} limit {size} offset {offset}";

            CountQuery = @"select count(*) from person p where 1=1";

            if (!string.IsNullOrWhiteSpace(name))
                CountQuery += $" and p.name like '%{name}%'";

            var person = _repository.FindWithPageSearch(query);
            int totalResult = _repository.GetCoutn(CountQuery);
            return new PageSearchVO<PersonVO> {
                CurrentPage = Page,
                List= _converter.Parse(person),
                PageSize= size,
                SortDirections= sort,
                TotalResults= totalResult
            };
        }
        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public PersonVO FindByID(long ID)
        {
            return _converter.Parse(_repository.FindByID(ID));
        }
        public List<PersonVO> FindByName(string firstName, string seconName)
        {
            return _converter.Parse(_repository.FindByName(firstName, seconName));
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
        public PersonVO Disabled(long id)
        {
            var personEntity = _repository.Disable(id);
            return _converter.Parse(personEntity);
        }
        public void Delete(long id)
        {
            _repository.Delete(id);
        }

    }
}
