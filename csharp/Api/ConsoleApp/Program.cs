global using BookStore.Models;
global using BookStore.Data;
using System;
using System.Collections.Generic;
using BookStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Configuration;
using BookStore.Services.BookService;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            // Setup DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddDbContext<SqliteContext>()
                .AddSingleton<IBookService, BookService>()
                .AddSingleton<BookStore>();


            Console.WriteLine("Program starts!");
            // Create fake data
            // IEnumerable<Author> authors = CreateFakeAuthors();

            //Create fake data
             IEnumerable<Book> books = CreateFakeBooks();

            // Create Database
            // using var db = CreateDatabase(authors);
            using var db = CreateDatabase(books);

            // Show fake data
            // ShowAuthors(authors);

            // Show data from database
            //authors = db.Authors.Include(a => a.Books); // Eager loading
            //ShowAuthors(authors);

            // Show recent books
            var recentBooks = from b in db.Books where b.PublicationYear > 2010 select b;
            Console.WriteLine("Showing recent books:");
            ShowBooks(recentBooks);

            // Get books from db
            books = DbBooks(db);
            //var books = db.Books;
            //Console.WriteLine(books);
            Console.WriteLine("Showing all books from db:");
            ShowBooks(books);

            var sProvider = serviceProvider.BuildServiceProvider();
            var bookStore = sProvider.GetService<BookStore>();
            var books2 = bookStore.All();
            Console.WriteLine(books2);

            Console.WriteLine("\nGet All Books using DI:");
            ShowBooks(books2);
            Console.WriteLine("\nGet One Book using DI:");
            var book = bookStore.One(1);
            ShowBooks(new List<Book>() { book });
            Console.WriteLine("\nUpdate One Book using DI:");
            var books3 = bookStore.Update(2, new Book() { Title = "A new book 2", PublicationYear = 2024 });
            // ShowBooks(books3);
            Console.WriteLine("\nDelete One Book using DI:");
            var books4 = bookStore.DeleteOne(2);
            ShowBooks(books4);
        }

        static IEnumerable<Book> DbBooks(SqliteContext db)
        {
            var books = db.Books.ToList();
            var service = new BookService(db);
            var _books = service.GetAllBooks();
            return _books;
        }

        static void ShowBooks(IEnumerable<Book> books)
        {
            foreach (var book in books)
            {
               Console.WriteLine(book);
            }
        }

        static IEnumerable<Book> CreateFakeBooks()
        {
            var Books = new List<Book>
            {
                new Book { Title = "Around the world", PublicationYear = 1999 },
                new Book { Title = "Little Sunny", PublicationYear = 2001 },
                new Book { Title = "Mano Aqib", PublicationYear = 2000 }
            };

            return Books;
        }

        static IEnumerable<Author> CreateFakeAuthors()
        {
            var authors = new List<Author>
            {
                new Author
                {
                    Name = "Ali",
                    //Books = new List<Book>
                    //{
                    //    new Book { Title = "Around the world", PublicationYear = 1999 },
                    //    new Book { Title = "Little Sunny", PublicationYear = 2001 },
                    //    new Book { Title = "Mano Aqib", PublicationYear = 2000 }
                    //}
                },
                new Author
                {
                    Name = "Dr. Kan",
                    //Books = new List<Book>
                    //{
                    //    new Book { Title = "Emma", PublicationYear = 2014 },
                    //    new Book { Title = "The park", PublicationYear = 2010 }
                    //}
                }
            };
            return authors;
        }

        static SqliteContext CreateDatabase(IEnumerable<Book> books)
        {
            var options = new DbContextOptionsBuilder<SqliteContext>()
             .UseSqlite(ConfigurationManager.ConnectionStrings["Default"].ConnectionString)
             .Options;
            var db = new SqliteContext(options);
            bool created = db.Database.EnsureCreated();
            if (created)
            {
                Console.WriteLine("Created DB first time and hence inserted fake data");
                db.Books.AddRange(books);
                db.SaveChanges();
            }
            return db;
        }

        static void ShowAuthors(IEnumerable<Author> authors)
        {
            foreach (var author in authors)
            {
                Console.WriteLine("{0} wrote:", author);
                //foreach (var book in author.Books)
                //{
                //    Console.WriteLine("    {0}", book);
                //}
                Console.WriteLine("");
            }
        }
    }
}

