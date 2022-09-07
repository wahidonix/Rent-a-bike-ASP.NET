using JWT.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        public static Bike bikes = new Bike();

        private readonly IConfiguration _configuration;

        private readonly DataContext dataContext;

        private readonly IRepository<Bike,int> bikeRepository;

        public AuthController(DataContext dataContext, IConfiguration configuration, IRepository<Bike, int> bikeRepository)
        {
            this.dataContext = dataContext;
            this.bikeRepository = bikeRepository;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            return Ok(await dataContext.Users.ToListAsync());
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDTO request)
        {
            CreatePasswordHash(request.Password,out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User();

            user.Username = request.Username;
            user.Email = request.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            if (!IsValid(request.Email)) 
            {
                return BadRequest("Email format is incorrect.");
            }
            if (request.Password.Length < 8)
            {
                return BadRequest("Password needs to be 8 characters long");
            }
            var dbUser = dataContext.Users.Where(b => b.Email == request.Email)
                    .FirstOrDefault();
            if (dbUser == null) 
            { 
                dataContext.Users.Add(user);
                await dataContext.SaveChangesAsync();
                return Ok(await dataContext.Users.ToListAsync());
            }
            else
            {
                return BadRequest("Email has already been registered");
            }

        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDTO request)
        {
            //var dbUser = await dataContext.Users.FindAsync(request.Username);
            var dbUser = dataContext.Users.Where(b => b.Email == request.Email)
                    .FirstOrDefault();
            if (dbUser == null)
            {
                return BadRequest("Email has not been registered.");
            }
            else
            {
                user = dbUser;
            }
            if (!VerifyPassowordHash(request.Password, dbUser.PasswordHash, dbUser.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }
            if (user.Verified == false)
            {
                return BadRequest("Account is not verified by the admin!");
            }
            string token = CreateToken(dbUser);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(7),
            };
            Response.Cookies.Append("jwt", token, cookieOptions);
            return Ok(token);
        }

        [HttpPost("getUser")]
        public async Task<ActionResult<List<User>>> getUser(UserDTO request)
        {
            var dbUser = dataContext.Users.Where(b => b.Email == request.Email)
                    .FirstOrDefault();
            return Ok(dbUser);
        }
        [HttpPut("updateUser")]
        public async Task<ActionResult<List<User>>> UpdateHero(UserDTO request)
        {
            var dbUser = await dataContext.Users.FindAsync(request.Id);
            dbUser.Username=request.Username;
            dbUser.Credits = request.Credits;
            if(request.Password != "") 
            {
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                dbUser.PasswordHash = passwordHash;
                dbUser.PasswordSalt = passwordSalt;
            }
            
            await dataContext.SaveChangesAsync();
            return Ok(await dataContext.Users.ToListAsync());
        }
        private static bool IsValid(string email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("userEmail:",user.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password,out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassowordHash(string password, byte[] passwordHash,byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(user.PasswordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }
    }
}
