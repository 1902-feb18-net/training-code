using CharacterRestDAL;
using CharacterRestService.ApiModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace CharacterRestService.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        //private readonly ILogger<AccountController> _logger;

        public AccountController(SignInManager<IdentityUser> signInManager/*,
            ILogger<AccountController> logger*/)
        {
            SignInManager = signInManager;
            //_logger = logger;

            // we can do code-first "skipping" migrations at runtime
            // the downside is, we can't run migrations on the database that gets generated
            // later.
            //dbContext.Database.EnsureCreated()
            // i'm gonna do migrations
            // when you do migrations and there's two dbcontexts, you have to specify
            // (it'll prompt you)
        }

        public SignInManager<IdentityUser> SignInManager { get; }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public ApiAccountDetails Details()
        {
            // if we want to know which user is logged in or which roles he has
            // apart from [Authorize] attribute...
            // we have User.Identity.IsAuthenticated
            // User.IsInRole("admin")
            // User.Identity.Name
            if (!User.Identity.IsAuthenticated)
            {
                //_logger.LogInformation("");
                return null;
            }
            var details = new ApiAccountDetails
            {
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
        [AllowAnonymous]
        public async Task<IActionResult> Login(ApiLogin login)
        {
            SignInResult result = await SignInManager.PasswordSignInAsync(
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
        [AllowAnonymous]
        public async Task<IActionResult> Register(ApiRegister register,
            [FromServices] RoleManager<IdentityRole> roleManager,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            var user = new IdentityUser(register.Username);

            IdentityResult createUserResult = await userManager.CreateAsync(user,
                register.Password);

            if (!createUserResult.Succeeded) // e.g. did not meet password policy
            {
                return BadRequest(createUserResult);
            }

            if (register.IsAdmin)
            {
                // make sure admin role exists
                if (!await roleManager.RoleExistsAsync("admin"))
                {
                    var role = new IdentityRole("admin");
                    IdentityResult createRoleResult = await roleManager.CreateAsync(role);
                    if (!createRoleResult.Succeeded)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError,
                            "failed to create admin role");
                    }
                }

                // add user to admin role
                IdentityResult addRoleResult = await userManager.AddToRoleAsync(user, "admin");
                if (!addRoleResult.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "failed to add user to admin role");
                }
            }

            await SignInManager.SignInAsync(user, false);

            return NoContent(); // nothing to show the user that he can access
        }


        // POST /account/seed
        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Seed(
            [FromServices] RoleManager<IdentityRole> roleManager,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            var nick = "nick.escalona@revature.com";
            if (await userManager.FindByNameAsync(nick) is null)
            {
                var nickUser = new IdentityUser(nick);

                IdentityResult result = await userManager.CreateAsync(nickUser, "password123");

                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "failed to seed");
                }
            }

            var fred = "fred@revature.com";
            if (await userManager.FindByNameAsync(fred) is null)
            {
                var fredUser = new IdentityUser(fred);

                IdentityResult result = await userManager.CreateAsync(fredUser, "password123");

                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "failed to seed");
                }

                // make sure admin role exists
                if (!await roleManager.RoleExistsAsync("admin"))
                {
                    var role = new IdentityRole("admin");
                    IdentityResult createRoleResult = await roleManager.CreateAsync(role);
                    if (!createRoleResult.Succeeded)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError,
                            "failed to seed");
                    }
                }

                // add fred to admin role
                IdentityResult addRoleResult = await userManager.AddToRoleAsync(fredUser, "admin");
                if (!addRoleResult.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "failed to add user to admin role");
                }
            }

            return NoContent();
        }
    }
}