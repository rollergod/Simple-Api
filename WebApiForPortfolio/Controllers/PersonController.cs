using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApiForPortfolio.Models;
using WebApiForPortfolio.Models.Abstract;

namespace WebApiForPortfolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository repository;
        public PersonController(IPersonRepository repos)
        {
            repository = repos;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            return Ok(await repository.GetAllPeople());
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await repository.GetById(id));
        }

        [HttpPost()]
        public async Task<IActionResult> AddPerson(Person person)
        {
            bool result = true;
            try
            {
                if (person != null)
                    return Ok(await repository.Add(person));
                else
                    result = false;
              
            }
            catch (Exception)
            {
                result = false;
            }
            
            return result ? Ok(person + "Added") : NotFound("Bad");
        }

        [HttpPut()]
        public async Task<IActionResult> UpdatePerson(Person person)
        {
            bool result = true;
            try
            {
                if (person != null)
                    return Ok(await repository.Update(person));
                else
                    result = false;
            }
            catch (Exception)
            {
                result = false;
            }
            return result ? Ok(person + "Updated") : BadRequest("Not updated");
        }

        [HttpDelete("/{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            bool result = true;
            var model = await repository.GetById(id);
            try
            {
                if (model != null)
                    await repository.Delete(model);
                else
                    result = false;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result ? Ok(result + "Deleted") : BadRequest();
        }
    }
}
