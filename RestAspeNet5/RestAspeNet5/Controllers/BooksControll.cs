using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestAspeNet5.Modals;
using RestAspeNet5.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspeNet5.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BooksControll: ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<BooksControll> _logger;
        private IBooksService _books;

        public BooksControll(ILogger<BooksControll> logger, IBooksService books)
        {
            _logger = logger;
            _books = books;
        }

        [HttpGet]
        public IActionResult get()
        {
            return Ok(_books.FindAll());
        }
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var book = _books.FindByID(id);
            if (book == null) return NotFound();
            return Ok(book);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Books books)
        {
            if (books == null) return BadRequest();
            return Ok(_books.Create(books));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Books books)
        {
            if (books == null) return BadRequest();
            return Ok(_books.Update(books));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _books.Delete(id);
            return NoContent();
        }
    }
}
