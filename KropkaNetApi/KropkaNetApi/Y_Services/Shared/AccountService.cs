using KropkaNetApi.X_Entities.Objects.Shared;
using KropkaNetApi.X_Models.Shared.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KropkaNetApi.X_Entities;
using KropkaNetApi.Exceptions;
using System.Security.Cryptography;
using KropkaNetApi.X_Entities.Objects.CompanySide;
using Microsoft.EntityFrameworkCore;
using KropkaNetApi.X_Entities.Objects.ClientSide;

namespace KropkaNetApi.Y_Services.Shared
{
    public interface IAccountService
    {
        void RegisterEmployee(RegisterEmployeeDto dto);
        void RegisterUser(RegisterUserDto dto);
        LoginResponse LogIn(LoginDto dto);
        LoginResponse Refresh(RefreshTokenModel model);
        string GenerateRefreshToken();
        string GenerateToken(Account account, LoginDto dto, string position);
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



        public void RegisterEmployee(RegisterEmployeeDto dto)
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



            var newEmployee = new Employee()
            {
                Name = dto.Name,
                Surname = dto.Surname,
                PersonalNumber = dto.PersonalNumber,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Note = dto.Note,
                PositionId = dto.PositionId,
                AccountId = newAccount.Id
            };

            _context.Employees.Add(newEmployee);
            _context.SaveChanges();
        }
        public void RegisterUser(RegisterUserDto dto)
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



            var newUser = new User()
            {
                Name = dto.Name,
                Surname = dto.Surname,
                PersonalNumber = dto.PersonalNumber,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Note = dto.Note,
                AccountId = newAccount.Id
            };

            _context.Users.Add(newUser);
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

            string position = "";
            var employee = _context.Employees.Include(e => e.Position).FirstOrDefault(e => e.AccountId == account.Id);
            if (employee != null)
            {
                response.Level = "employee";
                response.PersonId = employee.Id;
                position = employee.Position.Name;
            }
            else
            {
                response.Level = "user";
                var user = _context.Users.FirstOrDefault(u => u.AccountId == account.Id);
                response.PersonId = user.Id;
            }

            response.IsLoggedIn = true;
            response.JwtToken = GenerateToken(account, dto, position);
            response.RefreshToken = GenerateRefreshToken();

            account.RefreshToken = response.RefreshToken;
            account.RefreshTokenExpire = DateTime.Now.AddDays(30);

            _context.SaveChanges();

            return response;
        }
        public LoginResponse Refresh(RefreshTokenModel model)
        {
            var response = new LoginResponse();
            var principal = GetTokenPrincipal(model.JwtToken);
            if (principal.Identity.Name is null)
            {
                return response;
            }

            var account = _context.Accounts.FirstOrDefault(a => a.Login == principal.Identity.Name);

            if (account is null || account.RefreshToken != model.RefreshToken || account.RefreshTokenExpire < DateTime.Now)
            {
                return response;
            }
            
            string position = "";
            var employee = _context.Employees.Include(e => e.Position).FirstOrDefault(e => e.AccountId == account.Id);
            if (employee != null)
            {
                response.Level = "employee";
                response.PersonId = employee.Id;
                position = employee.Position.Name;
            }
            else
            {
                response.Level = "user";
                var user = _context.Users.FirstOrDefault(u => u.AccountId == account.Id);
                response.PersonId = user.Id;
            }

            var loginDto = new LoginDto
            {
                Login = account.Login,
                Password = string.Empty
            };

            response.IsLoggedIn = true;
            response.JwtToken = GenerateToken(account, loginDto, position);
            response.RefreshToken = GenerateRefreshToken();

            account.RefreshToken = response.RefreshToken;
            account.RefreshTokenExpire = DateTime.Now.AddDays(30);

            _context.SaveChanges();

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
        public string GenerateToken(Account account, LoginDto dto, string position)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{account.Login}")
            };
            if (position != "")
            {
                claims.Add(new Claim(ClaimTypes.Role, position));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddSeconds(_authenticationSettings.JwtExpireSeconds);

            var token = new JwtSecurityToken(
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
        private ClaimsPrincipal GetTokenPrincipal(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var validation = new TokenValidationParameters
            {
                IssuerSigningKey = key,
                ValidateLifetime = false,
                ValidateActor = false,
                ValidateIssuer = false,
                ValidateAudience = false,
            };
            return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
        }


    }
}