
using RestAspeNet5.Configuration;
using RestAspeNet5.Data.VO;
using RestAspeNet5.Repository;
using RestAspeNet5.Service;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RestAspeNet5.Business.Implementacao
{
    public class LoginBusinessImplementation : ILoginBusiness
    {
        private const string DATE_FORMAT = "yy-MM-dd HH-mm-ss";
        private  TokenConfiguration _tokenConfiguration;
        private  IUsersRepository _repository;
        private readonly ITokenService _tokenService;
        public LoginBusinessImplementation(TokenConfiguration tokenConfiguration, IUsersRepository repository, ITokenService tokenService)
        {
            _tokenConfiguration = tokenConfiguration;
            _repository = repository;
            _tokenService = tokenService;
        }

        public TokenVO ValidateCredential(UserVO userCredential)
        {
            //Pega as credenciais e valida no banco se existe retorna user
            var user = _repository.ValidateCredentials(userCredential);
            //Se não retorna null
            if (user == null) return null;
            //Se não null, valida os Claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };
            // Valida os tokens
            var acessToken = _tokenService.GenerateAcessToken(claims);
            var RefreshToken = _tokenService.GenerateRefreshToken();
            //Coloca os valores no usuario
            user.RefreshToken = RefreshToken;
            user.RefreshTokenExpired = DateTime.Now.AddDays(_tokenConfiguration.DayToExpiry);
            
            DateTime CreatedDay = DateTime.Now;
            //Quando expira
            DateTime ExpirationDate = CreatedDay.AddMinutes(_tokenConfiguration.Minutes);
            //Atualiza as informações
            _repository.RefreshUserInfo(user);
            //Seta tudo a Token
            return new TokenVO(
                true,
                CreatedDay.ToString(DATE_FORMAT),
                ExpirationDate.ToString(DATE_FORMAT),
                acessToken,
                RefreshToken
                );
        }

        public TokenVO ValidateCredential(TokenVO token)
        {
            var acessToken = token.AcessToken;
            var refreshToken = token.RefreshToken;
            var principal = _tokenService.GetPrincipalFromExpiredToken(acessToken);
            var userName = principal.Identity.Name;
            var user = _repository.ValidateCredentials(userName);
            if (user == null || 
                user.RefreshToken != refreshToken ||
                user.RefreshTokenExpired <= DateTime.Now) return null;

            acessToken = _tokenService.GenerateAcessToken(principal.Claims);
            refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;

            DateTime CreatedDay = DateTime.Now;
            //Quando expira
            DateTime ExpirationDate = CreatedDay.AddMinutes(_tokenConfiguration.Minutes);
            //Atualiza as informações

            _repository.RefreshUserInfo(user);
            //Seta tudo a Token
            return new TokenVO(
                true,
                CreatedDay.ToString(DATE_FORMAT),
                ExpirationDate.ToString(DATE_FORMAT),
                acessToken,
                refreshToken
                );
        }

        public bool RevokeToken(string userName)
        {
            return _repository.RevokeToken(userName);
        }
    }
}
