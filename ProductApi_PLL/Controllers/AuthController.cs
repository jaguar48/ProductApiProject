
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductAPI_BLL.Interface;
using ProductAPI_Data.Dtos.Response;
using ProductAPI_Data.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace ProductApi_PLL.Controllers
{
    [ApiController]
    [Route("api/productapp")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authentication;
        private readonly UserManager<User> _userManager;

        public AuthController(IAuthService authentication, UserManager<User> userManager)
        {
            _authentication = authentication;
            _userManager = userManager;
        }


        [HttpPost("login")]

        [SwaggerOperation(Summary = "Authenticate user and create token", Description = "Authenticate user and create token.")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Token created successfully.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid user credentials.")]
        public async Task<IActionResult> Authenticate([FromBody] UserAuthenticationResponse user)
        {
            var response = await _authentication.ValidateUser(user);



            if (!response.Success)
                return BadRequest(response);

            return Ok(new { Token = await _authentication.CreateToken(), response.Role });

        }
    }
}
