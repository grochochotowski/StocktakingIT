using KropkaNetApi.X_Entities.Objects.Shared;
using KropkaNetApi.X_Models.Shared.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using KropkaNetApi.X_Entities;
using KropkaNetApi.Exceptions;
using KropkaNet.Migrations;
using System.Security.Cryptography;

namespace KropkaNetApi.Y_Services.Shared
{
    public interface IAccountService
    {
        void Register(RegisterDto dto);
        LoginResponse LogIn(LoginDto dto);
        string GenerateRefreshToken();
        string GenerateToken(Account account);
    }

    public class AccountService : IAccountService
    {
        private readonly StocktakingContext _context;
        private readonly IPasswordHasher<Account> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(StocktakingContext context, IPasswordHasher<Account> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }



        public void Register(RegisterDto dto)
        {
            var newAccount = new Account()
            {
                Login = dto.Login
            };

            if (dto.Password != dto.ConfirmPassword)
            {
                throw new BadRequestException("Passwords do not match");
            }

            var hashedPassword = _passwordHasher.HashPassword(newAccount, dto.Password);

            newAccount.HashedPassword = hashedPassword;

            _context.Accounts.Add(newAccount);
            _context.SaveChanges();
        }

        public LoginResponse LogIn(LoginDto dto)
        {
            var response = new LoginResponse();
            var account = _context.Accounts.FirstOrDefault(a => a.Login == dto.Login);

            if (account is null)
            {
                return response;
            }

            var hashedPassword = _passwordHasher.VerifyHashedPassword(account, account.HashedPassword, dto.Password);
            if (hashedPassword == PasswordVerificationResult.Failed)
            {
                return response;
            }

            response.IsLoggedIn = true;
            response.JwtToken = GenerateToken(account);
            response.RefreshToken = GenerateRefreshToken();

            return response;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];

            using (var numberGenerator = RandomNumberGenerator.Create())
            {
                numberGenerator.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }

        public string GenerateToken(Account account)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{account.Login}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}