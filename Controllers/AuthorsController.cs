using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Doraneko.Services;
using Doraneko.Models;

namespace Doraneko.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorService _authorService;

        public AuthorsController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public ActionResult<List<Author>> Get()
        {
            return _authorService.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<Author> GetAuthor(string id)
        {
            var todoItem = _authorService.Get(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return todoItem;
        }

        [HttpPost]
        public ActionResult<Author> Post(Author author)
        {
            _authorService.Create(author);
            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        [HttpPut("id:length(24)")]
        public ActionResult<Author> Update(string id, Author authorIn)
        {
            var author = _authorService.Get(id);
            if (author == null)
            {
                return NotFound();
            }
            _authorService.Update(id, authorIn);
            return NoContent();
        }

        [HttpDelete("id:length(24)")]
        public ActionResult<Author> Delete(string id)
        {
            var author = _authorService.Get(id);
            if (author == null)
            {
                return NotFound();
            }
            _authorService.Remove(author.Id);
            return NoContent();
        }

    }
}