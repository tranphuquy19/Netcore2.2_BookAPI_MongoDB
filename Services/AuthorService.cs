using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using Doraneko.Models;

namespace Doraneko.Services
{
    public class AuthorService
    {
        private readonly IMongoCollection<Author> _author;

        public AuthorService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _author = database.GetCollection<Author>(settings.AuthorsCollectionName);
        }

        public List<Author> Get() =>
            _author.Find(author => true).ToList();

        public Author Get(string id) =>
            _author.Find<Author>(author => author.Id == id).FirstOrDefault();

        public Author Create(Author author)
        {
            _author.InsertOne(author);
            return author;
        }
        public void Update(string id, Author authorIn)
        {
            _author.ReplaceOne(author => author.Id == id, authorIn);
        }

        public void Remove(Author authorIn)
        {
            _author.DeleteOne(author => author.Id == authorIn.Id);
        }

        public void Remove(string id)
        {
            _author.DeleteOne(author => author.Id == id);
        }
    }
}