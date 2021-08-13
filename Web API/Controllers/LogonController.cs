using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using API.Models;
using Model.DTO;
using Persistence.APIHandler;

namespace API.Controllers
{

    public class Token
    {
        public string token;

        public Token(string token)
        {
            this.token = token;
        }
    }
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly StaffContext context;
        private readonly TokenService tokenService;
        private readonly IConfiguration _config;

        public AccountController(StaffContext context, TokenService tokenService, IConfiguration config)
        {
            this._config = config;
            this.tokenService = tokenService;
            this.context = context;
        }

        [HttpPost("logon")]
        public async Task<ActionResult<string>> Login(LoginDTO logon)
        {
            SQLProcedure pro = new SQLProcedure(this._config);
            var user = await pro.Login(logon);
            if (user == null)
            {
                return BadRequest("Wrong username or password");
            }

            else
            {
                var tokenObj = new TokenDTO
                {
                    UserName = logon.UserName,
                    Type = user.Type
                };
                return tokenService.CreateToken(tokenObj);
            }
        }



    }
}