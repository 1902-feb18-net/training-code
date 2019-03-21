using CharacterRestDAL;
using CharacterRestService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CharacterRestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public SignInManager<IdentityUser> SignInManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }

        public AccountController(SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            AuthDbContext dbContext)
        {
            SignInManager = signInManager;
            RoleManager = roleManager;

            // we can do code-first "skipping" migrations at runtime
            // the downside is, we can't run migrations on the database that gets generated
            // later.
            dbContext.Database.EnsureCreated();
        }

        [HttpGet("[action]")]
        public AccountDetails Details()
        {
            var details = new AccountDetails
            {
                IsAuthenticated = User.Identity.IsAuthenticated,
                Username = User.Identity.Name,
                Roles = User.Claims.Where(c => c.Type == ClaimTypes.Role)
                                   .Select(c => c.Value)
            };
            return details;
        }

        // POST for create resource, but also for "perform operation"
        // when there is no way to fit the operation into CRUD terms.
        // POST /account/login
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(Login login)
        {
            var result = await SignInManager.PasswordSignInAsync(
                login.Username, login.Password, login.RememberMe, false);

            if (!result.Succeeded)
            {
                return Unauthorized(); // 401 for login failure
            }

            return NoContent();
        }

        // POST /account/logout
        [HttpPost("[action]")]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();

            return NoContent();
        }

        // POST /account
        [HttpPost]
        public async Task<IActionResult> Register(Register register,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            var user = new IdentityUser(register.Username);

            var result = await userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded) // e.g. did not meet password policy
            {
                return BadRequest(result);
            }

            if (register.IsAdmin)
            {
                // make sure admin role exists
                if (!await RoleManager.RoleExistsAsync("admin"))
                {
                    var role = new IdentityRole("admin");
                    var result2 = await RoleManager.CreateAsync(role);
                    if (!result2.Succeeded)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError,
                            "failed to create admin role");
                    }
                }

                // add user to admin role
                var result3 = await userManager.AddToRoleAsync(user, "admin");
                if (!result3.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "failed to add user to admin role");
                }
            }

            await SignInManager.SignInAsync(user, false);

            return NoContent(); // nothing to show the user that he can access
        }
    }
}