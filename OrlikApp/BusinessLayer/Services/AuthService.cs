using BusinessLayer.Contexts;
using BusinessLayer.Entities;
using BusinessLayer.Helpers;
using BusinessLayer.Models.Auth;
using BusinessLayer.Models.Enums;
using BusinessLayer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class AuthService : IAuthService
    {
        private readonly OrlikAppContext _context;
        private readonly TokenSettings _tokenSettings;
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            OrlikAppContext context, 
            IOptions<TokenSettings> tokenSettings,
            IUserRepository userRepository,
            IHashService hashService,
            ILogger<AuthService> logger)
        {
            _context = context;
            _tokenSettings = tokenSettings.Value;
            _userRepository = userRepository;
            _hashService = hashService;
            _logger = logger;
        }

        #region Authenticate()
        public async Task<AuthResponse> Authenticate(string login, string password)
        {
            try
            {
                var user = await _context.Users.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Login == login);

                if (user == null)
                {
                    throw new BusinessLogicException("Nieprawidłowa nazwa użytkownika",
                        (int)AuthError.InvalidLogin);
                }

                if (!_hashService.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    throw new BusinessLogicException("Nieprawidłowe hasło",
                        (int)AuthError.InvalidPassword);
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_tokenSettings.SecretKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(6),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return new AuthResponse()
                {
                    UserId = user.Id,
                    Token = tokenString
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion

        #region RegisterUser()
        public async Task<User> RegisterUser(string login, string password, string email)
        {
            try
            {
                await _userRepository.CheckUniqueFields(login, email);
                var user = new User
                {
                    Login = login,
                    Email = email,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    RoleId = (long)RoleIds.User
                };

                _hashService.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                _context.Users.Add(user);
                _context.SaveChanges();

                return user;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion
    }
}
