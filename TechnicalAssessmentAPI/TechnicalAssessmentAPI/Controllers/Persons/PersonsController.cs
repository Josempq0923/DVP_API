using App.Config.DTO;
using App.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechnicalAssessmentAPI.Controllers.Persons
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonsController : Controller
    {
        private readonly IPersonsService _service;

        public PersonsController(IPersonsService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetAllPersons")]
        public async Task<IActionResult> GetAllPersons()
        {
            try
            {
                var result = await _service.GetAllPersonsByStoreProcedure();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SavePerson(PersonsDTO personsDTO)
        {
            try
            {
                var result = await _service.CreateAsync(personsDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePerson(PersonsDTO personsDTO)
        {
            try
            {
                var result = await _service.UpdateAsync(personsDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePerson(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
