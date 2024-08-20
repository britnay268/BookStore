using System.Collections.Generic;

namespace BookStore
{
    // This is the BookRepository class
    // The BookRepository class is responsible for getting the book data from the data store
    // In this case, the data store is an in-memory dictionary
    // The data store could be a database, a web service, or any other source of data
    public class BookRepository : IBookRepository // if you your interface hasn't been implemented, you can press ctrl + . and click on implement interface
    {
        // The dictionary that stores the book data
        // It will not actually be a dictionary in a real-world application
        // It will be a database, a web service, or any other source of data
        // For the sake of simplicity, we are using a dictionary here
        // Your unit tests will not be able to test the actual data store
        // Because the data store will be external to the application (e.g., a database)
        // Your unit tests will not be accessing the data in this dictionary

        private readonly Dictionary<int, Book> books = new()
        {
            { 1, new Book { Id = 1, Title = "1999", Author = "George Orwell" } },
            { 2, new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee" } },
            { 3, new Book { Id = 3, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald" } },
            { 4, new Book { Id = 4, Title = "Lord of the Flies", Author = "William Golding" } },
            { 5, new Book { Id = 5, Title = "Pride and Prejudice", Author = "Jane Austen" } }
        };

        public Book GetBookById(int id)
        {
            books.TryGetValue(id, out var book);
            return book;
        }

        public List<Book> GetAllBooks()
        {
            return books.Values.ToList();
        }

        public Book AddBook(Book book)
        {
            int id = books.Keys.Max() + 1;
            book.Id = id;
            books.Add(id, book);
            return book;
        }

        public bool UpdateBook(Book book)
        {
            if (books.ContainsKey(book.Id))
            {
                books[book.Id] = book;
                return true;
            }
            return false;
        }

        public bool DeleteBook(int id)
        {
            books.TryGetValue(id, out var book);
            return books.Remove(id);
        }
    }
}
