using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Section_Website.Data;
using Section_Website.Model;

namespace Section_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _singInManager;
        private readonly ApplicationDbContext _context;
        public AuthenticationController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _singInManager = signInManager;
            _context = context;
        }

        [HttpPost]
        [Route("Registration")]
        public async Task<Object> CustomerRegistration(RegistrationModel model)
        {
            var usr = await _userManager.FindByEmailAsync(model.Email);
            if (usr != null)
            {
                return BadRequest(new { message = "User Already Exists" });
            }
            var applicationUser = new AppUser()
            {
                UserName = model.Email,
                Email = model.Email
            };
            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                await _context.SaveChangesAsync();     
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message="Server Error"});
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            ResponseModel responseModel = new ResponseModel()
            {
                IsSuccess = false
            };
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                responseModel.IsSuccess = true;
                responseModel.responeObject = new { message = "Login success", Email = model.Email };
                return Ok(responseModel);
            }
            else
            {
                responseModel.responeObject = new { message = "Username or password is incorrect." };
                return Ok(responseModel);
            }
                
        }










    }
}
