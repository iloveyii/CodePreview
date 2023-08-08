using System;


namespace BookStore.Models
{
	public class Book
	{
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? PublicationYear { get; set; }
        public string? Category { get; set; }
        public string? PdfPath { get; set; }
        public string? ImagePath { get; set; }
        // public Author? Author { get; set; }
        public string? AuthorNames { get; set; }
        public int? Pages { get; set; }

        public override string ToString()
        {
            return $"{Title} - {PublicationYear}";
        }

        public Book update(Book book)
        {
            Title = book.Title;
            PublicationYear = book.PublicationYear;
            // Author = book.Author;
            Category = book.Category;
            PdfPath = book.PdfPath;
            ImagePath = book.ImagePath;
            AuthorNames = book.AuthorNames;
            Pages = book.Pages;

            return this;
        }
    }
}

