using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CharacterRestDAL;
using CharacterRestService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CharacterRestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public SignInManager<IdentityUser> SignInManager { get; }

        public AccountController(SignInManager<IdentityUser> signInManager,
            AuthDbContext dbContext)
        {
            // we can do code-first "skipping" migrations at runtime
            // the downside is, we can't run migrations on the database that gets generated
            // later.
            dbContext.Database.EnsureCreated();
            SignInManager = signInManager;
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

            await SignInManager.SignInAsync(user, false);

            return NoContent(); // nothing to show the user that he can access
        }
    }
}