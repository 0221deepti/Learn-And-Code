using System;
using System.IO;

public class Book
{
    public string Title { get; }
    public string Author { get; }
    private int _currentPage;

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
        _currentPage = 1;
    }

    public void TurnPage()
    {
        _currentPage++;
    }

    public string GetCurrentPageContent()
    {
        return $"Content of page {_currentPage}";
    }
}

public class BookLocation
{
    public string Room { get; }
    public string Shelf { get; }

    public BookLocation(string room, string shelf)
    {
        Room = room;
        Shelf = shelf;
    }

    public string GetLocationDescription()
    {
        return $"Room: {Room}, Shelf: {Shelf}";
    }
}

public class BookRepository
{
    private const string StorageFolder = "Documents";

    public void Save(Book book)
    {
        Directory.CreateDirectory(StorageFolder);

        string filePath = Path.Combine(
            StorageFolder,
            $"{book.Title} - {book.Author}.txt"
        );

        File.WriteAllText(
            filePath,
            $"Title: {book.Title}{Environment.NewLine}Author: {book.Author}"
        );
    }
}

public interface IPrinter
{
    void PrintPage(string pageContent);
}

public class PlainTextPrinter : IPrinter
{
    public void PrintPage(string pageContent)
    {
        Console.WriteLine(pageContent);
    }
}

public class HtmlPrinter : IPrinter
{
    public void PrintPage(string pageContent)
    {
        Console.WriteLine($"<div class=\"single-page\">{pageContent}</div>");
    }
}

class Program
{
    static void Main()
    {
        Book book = new Book("A Great Book", "John Doe");
        book.TurnPage();

        IPrinter printer = new PlainTextPrinter();
        printer.PrintPage(book.GetCurrentPageContent());

        BookLocation location = new BookLocation("Room 1", "Shelf A");
        Console.WriteLine(location.GetLocationDescription());

        BookRepository repository = new BookRepository();
        repository.Save(book);

        Console.WriteLine("Book saved successfully!");
    }
}
