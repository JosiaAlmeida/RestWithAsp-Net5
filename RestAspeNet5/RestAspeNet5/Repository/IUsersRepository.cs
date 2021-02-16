using RestAspeNet5.Data.VO;
using RestAspeNet5.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspeNet5.Repository
{
    interface IUsersRepository
    {
        Users ValidateCredentials(UserVO user);
    }
}
