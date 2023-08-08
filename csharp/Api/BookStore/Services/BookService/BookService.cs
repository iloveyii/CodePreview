using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;


namespace BookStore.Services.BookService
{
	public class BookService :  IBookService
	{
        private readonly SqliteContext _context;

		public BookService(SqliteContext context)
		{
            _context = context;
		}

        public Book CreateBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();

            return book;
        }

        public List<Book> DeleteBook(int id)
        {
            var book = GetBook(id);
            if(book is null)
            {
                return null;
            }
            _context.Books.Remove(book);
            _context.SaveChanges();
            return GetAllBooks();
        }

        public Book? GetBook(int id)
        {
            return _context.Books.Find(id);
        }

        public List<Book> GetAllBooks()
        {
            var books = _context.Books.ToList();

            return books;
        }

        public Book? UpdateBook(int id, Book request)
        {
            var book = GetBook(id);
            if (book is not null)
            {
                book.update(request);
                _context.SaveChanges();
            }
            return book;
        }

        public List<Book> SearchByTitle(string Title)
        {
            // var books = _context.Books.Where(b => b.Title.ToLower().Contains(Title.ToLower())).ToList();
            var title = Title.Replace(' ', '%');
            var books = _context.Books.Where(b => EF.Functions.Like(b.Title, "%"+title+"%") ).ToList();
            return books;
        }

        public List<string> GetAllCategories()
        {
            var cats = _context.Books.Select( x => x.Category ).Distinct();
            return cats.ToList();
        }

        public List<Book> CreateBooks(List<Book> books)
        {
            _context.BulkInsert(books, options => options.BatchSize = books.Count);
            return books;
        }
    }
}

