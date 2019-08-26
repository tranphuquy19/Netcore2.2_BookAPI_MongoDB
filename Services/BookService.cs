using System;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using Doraneko.Models;

namespace Doraneko.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;
        private readonly AuthorService _authorService;

        public BookService(IBookstoreDatabaseSettings settings, AuthorService authorService)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
            _authorService = authorService;
        }

        public List<Book> Get() =>
            _books.Find(book => true).ToList();

        public Book Get(string id) =>
            _books.Find<Book>(book => book.Id == id).FirstOrDefault();

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            var author = _authorService.Get(book.AuthorId);
            List<string> books = author.Books;
            var temp = String.Join(",", books);
            Console.WriteLine(temp.ToString());
            if (!books.Contains(book.Id))
            {
                books.Add(book.Id);
            }
            author.Books = books;
            _authorService.Update(author.Id, author);
            return book;
        }

        public void Update(string id, Book bookIn)
        {
            _books.ReplaceOne(book => book.Id == id, bookIn);
        }

        public void Remove(Book bookIn)
        {
            _books.DeleteOne(book => book.Id == bookIn.Id);
        }

        public void Remove(string id) =>
            _books.DeleteOne(book => book.Id == id);
    }
}