using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoListAPI.Models;

namespace ToDoListAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AuthenticationController:Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    public AuthenticationController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        this._roleManager = roleManager;
        this._userManager = userManager;
        _configuration = configuration;

    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        IdentityUser user = await _userManager.FindByNameAsync(loginModel.Username);
        if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
                {
                     new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
        return Unauthorized();
    }

    [HttpPost]
    [Route("Signup")]
    public async Task<IActionResult> SignUP([FromBody] LoginModel loginModel)
    {
        IdentityUser userExists = await _userManager.FindByNameAsync(loginModel.Username);

        if (userExists != null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,"user already used before");
        };
        userExists = new();
        userExists.UserName = loginModel.Username;
        userExists.Email = loginModel.Username;
        IdentityResult result = await _userManager.CreateAsync(
        userExists, loginModel.Password);
        if (result.Succeeded)
        {
            return StatusCode(StatusCodes.Status200OK,"user created");
        }
        return StatusCode(StatusCodes.Status500InternalServerError, "error occured");


    }

}
