using App.Config.DTO;
using App.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechnicalAssessmentAPI.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _service;
        public UsersController(IUsersService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var result = await _service.GetAllUsers();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(UsersDTO user)
        {
            try
            {
                var result = await _service.CreateUser(user);
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(UsersDTO user)
        {
            try
            {
                var result = await _service.UpdateAsync(user);
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
