using CardApp.BLL.Contracts;
using CardApp.BLL.Services;
using CardApp.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CardApp.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserService _userService;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUserService userService)
        {
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        /// <summary>
        /// Аутенфикация пользователя и выдача токена.
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var token = await _userService.Authenticate(loginModel.Username, loginModel.Password);
            if (token is null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
        /// <summary>
        /// Регистрация пользователя.
        /// </summary>
        /// <param name="registrationModel"></param>
        /// <returns></returns>
        [Route("new")]
        [HttpPost]
        public async Task<IActionResult> Registration([FromBody] RegistrationModel registrationModel)
        {
            await _userService.RegistrationUser(registrationModel);
            return Ok();
        }
    }
}
