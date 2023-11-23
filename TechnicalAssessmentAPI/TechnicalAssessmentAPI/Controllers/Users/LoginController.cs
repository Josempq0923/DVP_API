using App.Config.DTO;
using App.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PaisGourmetQRControlAPI.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsersService _service;
        public LoginController(IUsersService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Login(LoginDTO login)
        {
            try
            {
                var result = _service.LoginUser(login);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
