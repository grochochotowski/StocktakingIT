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

namespace KropkaNetApi.Y_Services.Shared
{
    public interface IAccountService
    {
        void Register(RegisterDto dto);
        string GenerateToken(LoginDto dto, HttpContext httpContext);
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

        public string GenerateToken(LoginDto dto, HttpContext httpContext)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.Login == dto.Login);

            if (account is null)
            {
                throw new BadRequestException("Login or password is incorrect");
            }

            var hashedPassword = _passwordHasher.VerifyHashedPassword(account, account.HashedPassword, dto.Password);
            if (hashedPassword == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Login or password is incorrect");
            }

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

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenString = tokenHandler.WriteToken(token);

            httpContext.Response.Cookies.Append("jwtToken", tokenString, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            return tokenString;
        }
    }
}