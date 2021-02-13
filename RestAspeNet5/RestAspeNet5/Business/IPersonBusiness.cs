using RestAspeNet5.Data.VO;
using System.Collections.Generic;

namespace RestAspeNet5.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        List<PersonVO> FindAll();
        PersonVO FindByID(long ID);
        PersonVO Update(PersonVO person);
        //Person Delete(Person person);
        void Delete(long id);
    }
}
