using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestAspeNet5.Business;
using RestAspeNet5.Data.VO;
using RestAspeNet5.Hypermedia.Filters;
using RestAspeNet5.Modals;
using RestAspeNet5.Repository.Generic;
using System.Collections.Generic;

namespace RestAspeNet5.Controllers
{
    [ApiVersion("1")]
    [Authorize("Bearer")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BooksControll: ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<BooksControll> _logger;
        private IBooksBusiness _booksBusiness;

        public BooksControll(ILogger<BooksControll> logger, IBooksBusiness books)
        {
            _logger = logger;
            _booksBusiness = books;
        }

        [HttpGet("{SortDirection}/{PageSize}/{Size}")]
        [ProducesResponseType((200), Type = typeof(List<BookVO>))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMidiaFilter))]
        public IActionResult get(string name, string SortDirection, int PageSize, int Size)
        {
            return Ok(_booksBusiness.FindWithPageSearch(name, SortDirection, PageSize, Size));
        }
        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(List<BookVO>))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMidiaFilter))]
        public IActionResult Get(long id)
        {
            var book = _booksBusiness.FindByID(id);
            if (book == null) return NotFound();
            return Ok(book);
        }
        [HttpPost]
        [ProducesResponseType((200), Type = typeof(List<BookVO>))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMidiaFilter))]
        public IActionResult Post([FromBody] BookVO books)
        {
            if (books == null) return BadRequest();
            return Ok(_booksBusiness.Create(books));
        }

        [HttpPut]
        [ProducesResponseType((200), Type = typeof(List<BookVO>))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMidiaFilter))]
        public IActionResult Put([FromBody] BookVO books)
        {
            if (books == null) return BadRequest();
            return Ok(_booksBusiness.Update(books));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType((200), Type = typeof(List<BookVO>))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMidiaFilter))]
        public IActionResult patch(long id)
        {
             var bookEntity = _booksBusiness.Disable(id);
            if (bookEntity == null) return BadRequest("Id invalid");
            return Ok(bookEntity);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((200), Type = typeof(List<BookVO>))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        public IActionResult Delete(long id)
        {
            _booksBusiness.Delete(id);
            return NoContent();
        }
    }
}
