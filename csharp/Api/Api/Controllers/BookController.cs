using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.Data;
using BookStore.Services.BookService;
using System.Configuration;
using static System.Reflection.Metadata.BlobBuilder;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/v1/book")]
    public class BookController : Controller
    {
        private readonly IBookService bookService;

        public BookController(IBookService service)
        {
            bookService = service;
        }

        // GET: api/v1/book
        [HttpGet]
        public List<Book> Get()
        {
            var books = bookService.GetAllBooks();
            return books;
        }

        // GET api/v1/book/5
        [HttpGet("{id}")]
        public Book Get(int id)
        {
            var book = bookService.GetBook(id);
            if (book is null)
                return new Book();
            return book;
        }

        // POST api/v1/book
        [HttpPost]
        public Book Post([FromBody] Book book)
        {
            var b = bookService.CreateBook(book);
            return b;
        }

        // PUT api/v1/book/5
        [HttpPut("{id}")]
        public Book Put(int id, [FromBody]Book request)
        {
            var book = bookService.UpdateBook(id, request);
            if (book is null)
                return new Book();

            return book;
        }

        // DELETE api/v1/book/5
        [HttpDelete("{id}")]
        public List<Book> Delete(int id)
        {
            var books = bookService.DeleteBook(id);
            return books;
        }
    }
}

