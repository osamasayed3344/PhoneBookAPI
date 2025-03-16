using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Serivces.Abstraction;
using Sharded.DTO;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactSerivce _contactSerivce;
        public ContactController(IContactSerivce contactSerivce){
            _contactSerivce=contactSerivce;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]ContactRequestDTO request){
            try{
                return Ok(await _contactSerivce.Search(request));
            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactRequestDTO request){
            try{
                await _contactSerivce.Add(request);
                return Created("successfull",request);
            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id){
            try{
                await _contactSerivce.Delete(id);
                return NoContent();
            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }
    }
}
