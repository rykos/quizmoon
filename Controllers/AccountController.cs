using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using quizmoon.Models;

namespace quizmoon.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private static string adminKey;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            IActionResult response = await this.CheckIfUserExists(registerModel);
            if (response != null)
                return response;

            ApplicationUser newApplicationUser = new ApplicationUser()
            {
                UserName = registerModel.Username,
                Email = registerModel.Email
            };

            IdentityResult identityResult = await this.userManager.CreateAsync(newApplicationUser, registerModel.Password);
            if (identityResult.Succeeded)
            {
                return Ok(ResponseDTO.Success("Registered successfully"));
            }
            else
            {
                return Ok(identityResult.Errors);
            }
        }

        [HttpPost]
        [Route("register-admin")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminRegisterModel registerModel)
        {
            if (registerModel.Key != adminKey)
            {
                Console.WriteLine(adminKey);
                return Unauthorized(ResponseDTO.Error("Invalid key"));
            }
            IActionResult errorResponse = await this.CheckIfUserExists(registerModel);
            if (errorResponse != null)
                return errorResponse;

            //Create admin role if there is none
            if (await this.roleManager.RoleExistsAsync("admin") == false)
            {
                await this.roleManager.CreateAsync(new IdentityRole("admin"));
            }

            ApplicationUser newApplicationUser = new ApplicationUser()
            {
                UserName = registerModel.Username,
                Email = registerModel.Email
            };

            IdentityResult identityResult = await this.userManager.CreateAsync(newApplicationUser, registerModel.Password);
            if (identityResult.Succeeded)
            {
                await userManager.AddToRoleAsync(newApplicationUser, "admin");
                //Create new admin key
                adminKey = GenerateAdminKey();
                return Ok(ResponseDTO.Success("Registered successfully"));
            }
            else
            {
                return Ok(ResponseDTO.Error("User creation failed, try again later."));
            }
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            ApplicationUser user = await this.userManager.FindByNameAsync(loginModel.Username);
            if (user == default)
                return Unauthorized(ResponseDTO.Error("User does not exist"));
            bool passwordCheckResoult = await this.userManager.CheckPasswordAsync(user, loginModel.Password);

            if (passwordCheckResoult)
            {
                IList<string> userRoles = await this.userManager.GetRolesAsync(user);

                List<Claim> authClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (string role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JWT:Secret"]));

                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: this.configuration["JWT:ValidIssuer"],
                    audience: this.configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(4),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    id = user.Id,
                    username = user.UserName,
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    admin = userRoles.Contains("admin")
                });
            }
            else
            {
                return Unauthorized(ResponseDTO.Error("Wrong username or password"));
            }
        }

        /// <summary>
        /// Checks if user exists in database, returns null if not
        /// </summary>
        private async Task<IActionResult> CheckIfUserExists(RegisterModel registerModel)
        {
            ApplicationUser userByName = await this.userManager.FindByNameAsync(registerModel.Username);
            ApplicationUser userByEmail = await this.userManager.FindByEmailAsync(registerModel.Email);
            if (userByName != default)
                return Ok(ResponseDTO.Error("Username is already taken"));
            if (userByEmail != default)
                return Ok(ResponseDTO.Error("Email is already taken"));
            return null;
        }

        private static string GenerateAdminKey()
        {
            string key;
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[32];
                rng.GetBytes(tokenData);

                key = Convert.ToBase64String(tokenData);
            }
            return key;
        }
    }
}