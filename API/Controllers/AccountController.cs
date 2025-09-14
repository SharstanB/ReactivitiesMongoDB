using Application.UsersAccounts.Command;
using Application.DataTransferObjects.UsersAccounts;
using Domain.CoreServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Application.Command_Queries.UsersAccounts.Command;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : BaseAppController
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<OperationResult<Guid>>> SignUp(SignupDTO signupDTO)
        {
            var signoutResult = await Mediator.Send(new SignUpCommand.Command()
            { Signup = signupDTO });
            return signoutResult;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<OperationResult<string>>> SignIn(SigninDTO loginDTO)
        {
            var loginUser = await Mediator.Send(new SignInCommand.Command()
            { Login = loginDTO });
            return loginUser;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<OperationResult<bool>>> SignOut()
        {
            var logoutResult = await Mediator.Send(new SignOutCommand.Command() { 
                RefreshToken = HttpContext.Request.Cookies["refresh_token"]!.ToString() });
            return logoutResult;
        }



        //private string GenerateJwtToken(string username)
        //{
        //    var claims = new[]
        //    {
        //    new Claim(JwtRegisteredClaimNames.Sub, username),
        //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //};

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_super_secret_key"));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        issuer: "yourdomain.com",
        //        audience: "yourdomain.com",
        //        claims: claims,
        //        expires: DateTime.Now.AddMinutes(30),
        //        signingCredentials: creds);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

    }
}
