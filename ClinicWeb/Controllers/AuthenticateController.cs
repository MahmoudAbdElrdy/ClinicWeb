using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DataAccessLayer;
using ApplicationCore;
using DataAccessLayer.Models;
using ApplicationCore.IRepository;

namespace ClinicWeb.Controllers
{
  [Route("api/[controller]")]
    [ApiController]
  public class AuthenticateController : ControllerBase
  {
    readonly IIdentityRepository<ApplicationUser> _identityUserRepository;
    readonly IIdentityRepository<ApplicationRole> _identityRoleRepository;
    readonly IIdentityRepository<ApplicationUserRole> _identityUserRoleRepository;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IConfiguration _configuration;
    private readonly IGenericRepositry<ApplicationUser> genericRepository;
    
    public AuthenticateController(IGenericRepositry<ApplicationUser> _genericRepository,
      UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IIdentityRepository<ApplicationUser> identityUserRepository,
      IIdentityRepository<ApplicationRole> identityRoleRepository, IIdentityRepository<ApplicationUserRole> identityUserRoleRepository
      )
    {
      _identityUserRepository = identityUserRepository;
      _identityRoleRepository = identityRoleRepository;
      _identityUserRoleRepository = identityUserRoleRepository;
      genericRepository = _genericRepository;
      this.userManager = userManager;
      this.roleManager = roleManager;
      _configuration = configuration;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
      var user = await userManager.FindByNameAsync(model.Username);
      if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
      {
        var userRoles = await userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
                {
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
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
      var userExists = await userManager.FindByNameAsync(model.Username);
      if (userExists != null)
        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
      ApplicationUser user = new ApplicationUser()
      {
        PhoneNumber = model.PhoneNumber,
        IsActive = true,
        CreatedDate = DateTime.Now,
        Address = model.Address,
        Email = model.Email,
        SecurityStamp = Guid.NewGuid().ToString(),
        UserName = model.Username
      };
      user.PasswordHash = userManager.PasswordHasher.HashPassword(user, model.Password);
      var result = await userManager.CreateAsync(user);
      if (!result.Succeeded)
        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

      return Ok(new Response { Status = "Success", Message = "User created successfully!" });
    }

    [HttpPost]
    [Route("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
    {
      var userExists = await userManager.FindByNameAsync(model.Username);
      if (userExists != null)
        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

      ApplicationUser user = new ApplicationUser()
      {
        Email = model.Email,
        SecurityStamp = Guid.NewGuid().ToString(),
        UserName = model.Username
      }; 
      user.PasswordHash = userManager.PasswordHasher.HashPassword(user, model.Password);
      var result = await userManager.CreateAsync(user);
      if (!result.Succeeded)
        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

      if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
        await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
      if (!await roleManager.RoleExistsAsync(UserRoles.User))
        await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

      if (await roleManager.RoleExistsAsync(UserRoles.Admin))
      {
        await userManager.AddToRoleAsync(user, UserRoles.Admin);
      }

      return Ok(new Response { Status = "Success", Message = "User created successfully!" });
    } 

    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetAllAsync()
    {
      return Ok(await _identityUserRepository.ListAllAsync());
    }
    [HttpGet("GetUser/{UserId}")]
    public async Task<ActionResult> GetUserByIdAsync(string UserId)
    {
      ApplicationUser model = await _identityUserRepository.GetByIdAsync(UserId);
      return Ok(model);
    }

    [HttpPut("EditUser")]
    public async Task<ActionResult> EditUserAsync([FromBody]ApplicationUser appUser)
    {

      if (ModelState.IsValid)
      {
        string id = appUser.Id;
        ApplicationUser originalUser = await _identityUserRepository.GetByIdAsync(id);
        if (originalUser != null)
        {
          originalUser.UserName = appUser.UserName;
          originalUser.NormalizedUserName = appUser.UserName.Normalize();
          originalUser.UserName = appUser.UserName;
          originalUser.Email = string.IsNullOrEmpty(appUser.Email) ? originalUser.Email : appUser.Email;
          originalUser.PhoneNumber = appUser.PhoneNumber;
          await _identityUserRepository.UpdateAsync(originalUser);
          return Ok(originalUser);
        }
        else
        {
          return BadRequest("User Is not Found");
        }
      }

      else
      {
        return BadRequest();
      }
    }
    [HttpDelete("Delete/{UserId}")]
    public async Task<ActionResult> DeleteAsync(string UserId)
    {
      try
      {

        ApplicationUser originalUser = await _identityUserRepository.GetByIdAsync(UserId);
        await _identityUserRepository.DeleteAsync(originalUser);
        return Ok(true);
      }
      catch
      {
        return BadRequest(false);
      }
    }
  }
}  
