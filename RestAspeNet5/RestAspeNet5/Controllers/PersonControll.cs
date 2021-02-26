using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestAspeNet5.Business;
using RestAspeNet5.Business.Implementacao;
using RestAspeNet5.Data.VO;
using RestAspeNet5.Hypermedia.Filters;
using System.Collections.Generic;

namespace RestAspeNet5.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
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
        /*
                [HttpGet]
                [ProducesResponseType((200), Type = typeof(List<PersonVO>))]
                [ProducesResponseType((204))]
                [ProducesResponseType((400))]
                [ProducesResponseType((401))]
                [TypeFilter(typeof(HyperMidiaFilter))]
                public IActionResult get()
                {
                    return Ok(_personBusiness.FindAll());
                }*/
        [HttpGet("{SortDirection}/{PageSize}/{Size}")]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMidiaFilter))]
        public IActionResult get([FromQuery] string name, 
            string SortDirection, int PageSize,
            int Size)
        {
            return Ok(_personBusiness.FindWithPageSearch(name, SortDirection,
                PageSize, Size));
        }
        [HttpGet("{id}")]
        [ProducesResponseType((200), Type= typeof(List<PersonVO>))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMidiaFilter))]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindByID(id);
            if (person == null) return NotFound();
            return Ok(person);
        }
        [HttpGet("FindPersonByName")]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMidiaFilter))]
        public IActionResult FindByName(string firstName, string secondName)
        {
            var person = _personBusiness.FindByName(firstName, secondName);
            if (person == null) return NotFound();
            return Ok(person);
        }
        [HttpPost]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMidiaFilter))]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Create(person));
        }

        [HttpPut]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMidiaFilter))]
        public IActionResult Put([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Update(person));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMidiaFilter))]
        public IActionResult Patch(long id)
        {
            var personEntity = _personBusiness.Disabled(id);
            return Ok(personEntity);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        public IActionResult Delete(long id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }
    }
}
