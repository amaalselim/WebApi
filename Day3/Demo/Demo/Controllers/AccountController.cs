using Demo.DTO;
using Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]//Resource User
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;

        public AccountController(UserManager<ApplicationUser> userManager,IConfiguration config)
        {
            this.userManager = userManager;
            this.config = config;
        }
        //Create Account new user =>"Registration"=>post
        [HttpPost("register")] //api/account/register
        public async Task<IActionResult> Registration(RegisterUserDTO userDTO) //DTO
        {
            if (ModelState.IsValid)
            {
                //Save
                ApplicationUser user = new ApplicationUser();
                user.UserName = userDTO.UserName;
                user.Email = userDTO.Email;
                IdentityResult result= await userManager.CreateAsync(user,userDTO.Password);
                if (result.Succeeded)
                {
                    return Ok("Account Is Success");
                }
                return BadRequest(result.Errors.FirstOrDefault());
                
            }
            return BadRequest(ModelState);  
        }
        //check Account Valid=>"Login"=>post
        [HttpPost("login")] //api/account/login
        public async Task<IActionResult> Login(LoginUserDTO UserDTO)
        {
            if (ModelState.IsValid==true)
            {
                //check-create Token
             ApplicationUser user= await userManager.FindByNameAsync(UserDTO.UserName);
                if (user != null)
                {
                 bool found= await userManager.CheckPasswordAsync(user,UserDTO.Password);
                    if (found)
                    {
                        //claims Token
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()));

                        //Get Role
                        var roles=await userManager.GetRolesAsync(user);
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }
                        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));
                        //signingCredentials
                        SigningCredentials signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


                        //Create Token
                        JwtSecurityToken Mytoken = new JwtSecurityToken(
                            issuer: config["JWT:ValidIssuer"],//url web api
                            audience: config["JWT:ValidAudience"],//url Consumer Angular
                            claims: claims,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: signingCred
                            );
                        return Ok(new
                        {
                            token= new JwtSecurityTokenHandler().WriteToken(Mytoken),
                            expiration=Mytoken.ValidTo
                        });
                    }
                }
                return Unauthorized();
            }
            return Unauthorized();
        }

    }
}
