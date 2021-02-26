using RestAspeNet5.Data.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspeNet5.Business
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredential(UserVO user);
        TokenVO ValidateCredential(TokenVO token);
        bool RevokeToken(string userName);
    }
}
