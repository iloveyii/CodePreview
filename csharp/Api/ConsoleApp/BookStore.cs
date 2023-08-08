using BookStore.Services.BookService;
using BookStore.Models;


namespace ConsoleApp
{
	public class BookStore
	{
        private readonly IBookService _bookService;

		public BookStore(IBookService bookService)
		{
            _bookService = bookService;
		}

        public List<Book> All()
        {
            var _books = _bookService.GetAllBooks();
            return _books;
        }

        public Book One(int id)
        {
            var book = _bookService.GetBook(id);
            return book;
        }

        public Book Update(int id, Book request)
        {
            var books = _bookService.UpdateBook(id, request);
            return books;
        }

        public List<Book> DeleteOne(int id)
        {
            var books = _bookService.DeleteBook(id);
            return books;
        }

        public Book CreateOne()
        {
            var book = new Book { Title = "Console book", PublicationYear= 2001 };
            var b = _bookService.CreateBook(book);
            return b;
        }

        public List<Book> Search(string Title)
        {
            var books = _bookService.SearchByTitle(Title);
            return books;
        }
    }
}

