using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspeNet5.Data.VO
{
    public class TokenVO
    {

        public TokenVO(bool aunthenticated, string created, string expired, string acessToken, string refreshToken)
        {
            Aunthenticated = aunthenticated;
            Created = created;
            Expired = expired;
            AcessToken = acessToken;
            RefreshToken = refreshToken;
        }

        public bool Aunthenticated { get; set; }
        public string Created { get; set; }
        public string Expired { get; set; }
        public string AcessToken { get; set; }
        public string RefreshToken { get; set; }

    }
}
