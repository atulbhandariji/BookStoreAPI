using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagemetAPI.Model;

namespace UserManagemetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private IConfiguration _config;

        private readonly EmployeeContext _context;

        TBLUserCredential _tBLUserCredential = new TBLUserCredential();

        public AuthenticateController(EmployeeContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        [HttpGet]
        public IActionResult Login(string userName, string password)
        {
            _tBLUserCredential.UserName = userName;
            _tBLUserCredential.Password = password;
            IActionResult response = Unauthorized();
            bool validUser = AuthenticateUser(_tBLUserCredential);
            if (validUser != null)
            {
              var tokenStr = GenerateJSONWebToken(_tBLUserCredential);
              response = Ok(new { token = tokenStr });
           }
            return response;
        }

        private bool AuthenticateUser(TBLUserCredential tBLUserCredential)
        {
            bool IsValid = false;
            IsValid = _context.TBLUserCredentials.Any(x => x.UserName == tBLUserCredential.UserName && x.Password== tBLUserCredential.Password);
            var tBLUser = _context.TBLUserCredentials.Select(x => x.UserName == tBLUserCredential.UserName && x.Password == tBLUserCredential.Password).FirstOrDefaultAsync();
            return IsValid;
        }
        private string GenerateJSONWebToken(TBLUserCredential tBLUserCredential)
        {
            var userName = tBLUserCredential.UserName;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,userName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };
                      var token = new JwtSecurityToken(
                      issuer: _config["Jwt:Issuer"],
                      audience: _config["Jwt:Issuer"],
                      claims,
                      expires: DateTime.Now.AddMinutes(30),
                      signingCredentials: credentials);

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;

        }
    }
}
