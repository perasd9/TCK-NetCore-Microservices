using Identity.API.Core;
using Identity.API.Core.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.API.Application
{
    public class AuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task Register(User user)
        {
            Role? role = await _unitOfWork.RoleRepository.GetByCondition(role => role.RoleName == "User").FirstOrDefaultAsync();
            user.RoleId = role.RoleId;
            user.Password = HashPassword(user.Password);

            await _unitOfWork.UserRepository.Add(user);
            
            await _unitOfWork.SaveChanges();
        }

        public async Task<User?> LoginUser(User user)
        {
            User? userDb = await _unitOfWork.UserRepository.GetByCondition(u => u.Email == user.Email, "Role").SingleOrDefaultAsync();
            if (userDb != null)
            {
                if(CheckPasswordHash(user.Password, userDb.Password))
                {
                    return userDb;
                }
            }

            return null;
        }
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        private bool CheckPasswordHash(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        public string GenerateToken(User user)
        {
            var signinCred = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("JWT")["Key"] ?? "")), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.Now.AddMinutes(double.Parse(_configuration.GetSection("JWT")["Expires"] ?? "")),
                SigningCredentials = signinCred,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.RoleName)
                })
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
