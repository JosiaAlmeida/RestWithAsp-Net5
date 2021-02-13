using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestAspeNet5.Business;
using RestAspeNet5.Business.Implementacao;
using RestAspeNet5.Data.VO;
using RestAspeNet5.Hypermedia.Filters;

namespace RestAspeNet5.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonControll : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<PersonControll> _logger;
        private IPersonBusiness _personBusiness;

        public PersonControll(ILogger<PersonControll> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }

        [HttpGet]
        [TypeFilter(typeof(HyperMidiaFilter))]
        public IActionResult get()
        {
            return Ok(_personBusiness.FindAll());
        }
        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMidiaFilter))]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindByID(id);
            if (person == null) return NotFound();
            return Ok(person);
        }
        [HttpPost]
        [TypeFilter(typeof(HyperMidiaFilter))]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Create(person));
        }

        [HttpPut]
        [TypeFilter(typeof(HyperMidiaFilter))]
        public IActionResult Put([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Update(person));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }
    }
}
