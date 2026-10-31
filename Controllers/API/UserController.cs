using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] ModelLogin modelLogin)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { error = "Invalid data" });
        }

        var user = await _userManager.FindByEmailAsync(modelLogin.Email);

        if (user == null)
        {
            return BadRequest(new { error = "User not found" });
        }

        var result = await _signInManager.PasswordSignInAsync(
            user.UserName!,
            modelLogin.Password,
            isPersistent: false,
            lockoutOnFailure: false
        );

        if (!result.Succeeded)
        {
            return BadRequest(new { error = "Authentication failed" });
        }

        // Generate JWT token here
        var token = await GenerateJwtToken(user);
        return Ok(
            new
            {
                token = token,
                email = user.Email,
                username = user.UserName

            }
        );
    }

   private async Task<string> GenerateJwtToken(User user)
{
    var user_roles = await _userManager.GetRolesAsync(user);

    var auth_claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Name, user.UserName!),
        new Claim(ClaimTypes.Email, user.Email!)
    };

    foreach (var role in user_roles)
    {
        auth_claims.Add(new Claim(ClaimTypes.Role, role));
    }

    string my_token_password = "K9#mP2$vL8@qR5!xT3&nW7^bC4*zH6!a";

    var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(my_token_password)
    );

    var token = new JwtSecurityToken(
        issuer: "http://localhost:5012",
        audience: "http://localhost:5012",
        expires: DateTime.UtcNow.AddHours(2),
        claims: auth_claims,
        signingCredentials: new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        )
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}
}