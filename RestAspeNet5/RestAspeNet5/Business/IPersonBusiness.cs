using RestAspeNet5.Data.VO;
using System.Collections.Generic;

namespace RestAspeNet5.Business
{
    public interface IPersonBusiness
    {
        IPersonVO Create(IPersonVO person);
        List<IPersonVO> FindAll();
        IPersonVO FindByID(long ID);
        IPersonVO Update(IPersonVO person);
        //Person Delete(Person person);
        void Delete(long id);
    }
}
