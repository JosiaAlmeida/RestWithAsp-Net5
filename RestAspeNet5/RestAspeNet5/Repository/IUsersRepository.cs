using RestAspeNet5.Data.VO;
using RestAspeNet5.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspeNet5.Repository
{
    public interface IUsersRepository
    {
        Users ValidateCredentials(UserVO user);
        Users ValidateCredentials(string userName);
        bool RevokeToken(string userName);
        Users RefreshUserInfo(Users user);
    }
}
