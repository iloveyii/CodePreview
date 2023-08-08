using System;
using BookStore.Models;


namespace BookStore.Services.BookService
{
	public interface IBookService
	{
        List<Book> GetAllBooks();
        Book? GetBook(int id);
        Book CreateBook(Book book);
        List<Book> CreateBooks(List<Book> books);
        Book? UpdateBook(int id, Book request);
        List<Book> DeleteBook(int id);
        List<Book> SearchByTitle(string Title);
        List<string> GetAllCategories();
    }
}

