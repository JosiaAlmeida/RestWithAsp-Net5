using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestAspeNet5.Business;
using RestAspeNet5.Data.VO;

namespace RestAspeNet5.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class AuthControllers : ControllerBase
    {
        private ILoginBusiness _loginBusiness;

        public AuthControllers(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }
        [HttpPost]
        [Route("signin")]
        public IActionResult signin([FromBody] UserVO user)
        {
            if (user == null) return BadRequest("Invalide Client Requeste");
            var Token = _loginBusiness.ValidateCredential(user);
            if (Token == null) return Unauthorized();
            return Ok(Token);
        }
        [HttpPost]
        [Route("refresh")]
        public IActionResult refresh([FromBody] TokenVO tokenVO)
        {
            if (tokenVO == null) return BadRequest("Invalide Client Requeste");
            var Token = _loginBusiness.ValidateCredential(tokenVO);
            if (Token == null) return BadRequest("Invalide Client Requeste");
            return Ok(Token);
        }
        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult revoke()
        {
            var userName = User.Identity.Name;
            var result = _loginBusiness.RevokeToken(userName);
            if (!result) return BadRequest("Invalid Client Request");
            return NoContent();
        }
    }
}
