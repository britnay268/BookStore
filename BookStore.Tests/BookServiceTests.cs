using Xunit;
using Moq;
using System.Net;

namespace BookStore.Tests
{
    public class BookServiceTests
    {
        // The BookService class is dependent on the IBookRepository interface.
        // We do not need to mock the Book class because it is a simple class.
        private readonly BookService _bookService;

        // The Mock class is used to create a mock object of the IBookRepository interface.
        // Mock objects are used to simulate the behavior of real objects.
        // Mock objects are used in unit testing to isolate the code under test.
        // We are mocking the IBookRepository interface because we do not want to test the actual implementation of the BookRepository class.
        // Mock just means that we are creating a fake object that simulates the behavior of the real object.
        private readonly Mock<IBookRepository> _mockBookRepository;

        public BookServiceTests()
        {
            _mockBookRepository = new Mock<IBookRepository>();
            _bookService = new BookService(_mockBookRepository.Object);
        }

        // GetBookById
        [Fact]
        public void GetBookDetails_ShouldReturnBook_WhenBookExists()
        {
            // Arrange - means to set up the test
            var bookId = 1;

            // Here, we are creating an instance of the Book class with the Id, Title, and Author properties set.
            // The expectedBook does not have to match the actual book in the repository.
            var expectedBook = new Book { Id = bookId, Title = "Star Wars", Author = "George Lucas" };

            // The Setup method is used to set up the behavior of the mock object.
            // This means that when the GetBookById method is called with the bookId parameter, the mock object should return the expectedBook instance.
            // The expectedBook is the object that we set up to be returned by the mock object.
            _mockBookRepository.Setup(repo => repo.GetBookById(bookId)).Returns(expectedBook);

            // Act - means to run the test
            // The GetBookById method is called with the bookId parameter.
            // The actual book returned by the GetBookById method comes from the mock object.
            var actualBook = _bookService.GetBookById(bookId);

            // Assert - means to check the result
            // The actualBook returned by the GetBookById method should be equal to the expectedBook instance.
            Assert.Equal(expectedBook, actualBook);
        }

        // GetBookById
        [Fact]
        public void GetBookDetails_ShouldReturnNull_WhenBookDoesNotExist()
        {
            // Arrange - means to set up the test
            // The book with Id 99 does not exist in the repository
            // Therefore, the GetBookById method should return null.
            var bookId = 99;

            // The Setup method is used to set up the behavior of the mock object.
            _mockBookRepository.Setup(repo => repo.GetBookById(bookId)).Returns((Book)null);

            // Act - means to run the test
            // The GetBookById method is called with the bookId parameter.
            var actualBook = _bookService.GetBookById(bookId);

            // Assert - means to check the result
            // The actualBook returned by the GetBookById method should be null.
            Assert.Null(actualBook);
        }

        // Test: Should return a list of books
        [Fact]
        public void GetAllBooks_ShouldReturnListofBooks()
        {
            var expectedBooks = new List<Book>
            {
                new Book { Id = 1, Title = "1999", Author = "George Orwell" },
                new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee" },
                new Book { Id = 3, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald" }
            };

            _mockBookRepository.Setup(repo => repo.GetAllBooks()).Returns(expectedBooks);

            var actualBookList = _bookService.GetAllBooks();

            Assert.Equal(expectedBooks, actualBookList);
        }

        // Test: Should return the ID of the newly added book
        [Fact]
        public void AddBook_ShouldReturnBook_WhenItExists()
        {
            // Arrange
            var newBook = new Book { Title = "Pride and Prejudice", Author = "Jane Austen" };

            _mockBookRepository.Setup(repo => repo.AddBook(newBook)).Returns(newBook);

            // Act
            var actualBook = _bookService.AddBook(newBook);

            // Assert
            Assert.Equal(newBook, actualBook);
        }

        // Test: Should return true when update is successful
        [Fact]
        public void UpdateBook_ShouldReturnTrue_WhenUpdateIsSuccessful()
        {
            // Arrange
            var bookToUpdate = new Book { Id = 4, Title = "Lord of the Flies", Author = "William Golding" };

            _mockBookRepository.Setup(repo => repo.UpdateBook(bookToUpdate)).Returns(true);

            // Act
            var result = _bookService.UpdateBook(bookToUpdate);

            // Assert
            Assert.True(result);
        }

        // Test: Should return false when update fails
        [Fact]
        public void UpdateBook_ShouldReturnFalse_WhenUpdateFails()
        {
            // Arrange
            var bookToUpdate = new Book { Id = 4, Title = "Lord of the Flies", Author = "William Golding" };

            _mockBookRepository.Setup(repo => repo.UpdateBook(bookToUpdate)).Returns(false);

            // Act
            var result = _bookService.UpdateBook(bookToUpdate);

            // Assert
            Assert.False(result);
        }

        // Test: Should return true when delete is successful
        [Fact]
        public void DeleteBook_ShouldReturnTrue_WhenDeleteIsSuccessful()
        {
            // Arrange
            var bookId = 1;

            _mockBookRepository.Setup(repo => repo.DeleteBook(bookId)).Returns(true);

            // Act
            var result = _bookService.DeleteBook(bookId);

            // Assert
            Assert.True(result);
        }

        // Test: Should return false when delete fails
        [Fact]
        public void DeleteBook_ShouldReturnFalse_WhenDeleteFails()
        {
            // Arrange
            var bookId = 1;

            _mockBookRepository.Setup(repo => repo.DeleteBook(bookId)).Returns(false);

            // Act
            var result = _bookService.DeleteBook(bookId);

            // Assert
            Assert.False(result);
        }
    }
}
