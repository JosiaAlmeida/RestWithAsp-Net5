using RestAspeNet5.Data.VO;
using RestAspeNet5.Hypermedia.Filters;
using System.Collections.Generic;

namespace RestAspeNet5.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        List<PersonVO> FindAll();
        PersonVO FindByID(long ID);
        PersonVO Update(PersonVO person);
        PersonVO Disabled(long id);
        List<PersonVO> FindByName(string firstName, string seconName);
        //Person Delete(Person person);
        void Delete(long id);
        PageSearchVO<PersonVO> FindWithPageSearch(string name, 
            string SortDirection, int PageSize, int Page);
    }
}
