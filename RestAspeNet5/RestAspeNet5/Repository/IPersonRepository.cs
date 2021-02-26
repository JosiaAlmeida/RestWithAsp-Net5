using RestAspeNet5.Data.VO;
using RestAspeNet5.Modals;
using RestAspeNet5.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspeNet5.Repository
{
    public interface IPersonRepository: IRepository<Person>
    {
        Person Disable(long id);
        List<Person>FindByName(string firstName, string seconName);
    }
}
