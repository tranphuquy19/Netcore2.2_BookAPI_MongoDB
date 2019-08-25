using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doraneko.Models;
using Doraneko.Services;

namespace Doraneko.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;
        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<List<Book>> Get()
        {
            return _bookService.Get();
        }

        //https://localhost:5001/api/Books/{:id}
        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Book> Get(string id)
        {
            var todoItem = _bookService.Get(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return todoItem;
        }

        [HttpPost]
        public ActionResult<Book> Post(Book item)
        {
            _bookService.Create(item);
            /*
            nameof(GetBook): lấy hàm GetBook(string id) nay đã đổi sang Get(string id)
            return CreatedAtRoute(nameof(GetBook), new { id = item.Id }, item);
             */
            return CreatedAtRoute("GetBook", new { id = item.Id }, item);
        }

        [HttpPut("id:length(24)")]
        public ActionResult<Book> Update(string id, Book bookIn)
        {
            var book = _bookService.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            _bookService.Update(id, bookIn);
            return NoContent();
        }

        [HttpDelete("id:length(24)")]
        public ActionResult<Book> Delete(string id)
        {
            var book = _bookService.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            _bookService.Remove(book.Id);
            return NoContent();
        }
    }
}