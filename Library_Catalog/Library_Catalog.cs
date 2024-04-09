using System.Collections.Concurrent;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

public class Library{
    string Name, Address;
    List <Book> Books;
    List <MediaItem>MediaItems;

    public Library(string Name, string Address, List <Book> Books, List<MediaItem>MediaItems){
        this.Name = Name;
        this.Address = Address;
        this.Books = Books;
        this.MediaItems = MediaItems;
    }
    public void AddBook(Book book){
        Books.Add(book);
    }

    public void RemoveBook(Book book){
        if (Books.Remove(book)){
            Console.WriteLine($"{book.Title} removed");
        }
        else {
            Console.WriteLine("Removal was not successful");
        }
    }

    public void AddMeidaItem(MediaItem item){
        MediaItems.Add(item);
    }

    public void RemoveMediaItem(MediaItem item){
        if (MediaItems.Remove(item)){
            Console.WriteLine($"{item.Title} removed");
        }
        else {
            Console.WriteLine("Removal was not successful");
        }
    }

    public void PrintCatalog(){
        Console.WriteLine("The books are : ");

        foreach(Book book in Books){
            Console.WriteLine($"{book.Title}");
        }

        Console.WriteLine();
        Console.WriteLine("The media items are: ");

        foreach (MediaItem item in MediaItems){
            Console.WriteLine($"{item.Title}" );
        }
    }

    public void Search(string query)
    {
        bool found = false;
        query = query.ToLower(); 

        Console.WriteLine($"Searching for: {query}");

        
        foreach (Book book in Books)
        {
            if (book.Title.ToLower().Contains(query) || book.Author.ToLower().Contains(query) || book.ISBN.Contains(query))
            {
                Console.WriteLine($"Found in Books: {book.Title} by {book.Author}");
                found = true;
            }
        }

        
        foreach (MediaItem item in MediaItems)
        {
            if (item.Title.ToLower().Contains(query) || item.MediaType.ToLower().Contains(query))
            {
                Console.WriteLine($"Found in Media Items: {item.Title}, Type: {item.MediaType}");
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("No match found.");
        }
    }
}



public class Book{
    public string Title, Author, ISBN;
    public int PublicationYear;

    public Book(string Title, string Author,string ISBN, int PublicationYear){
            this.Title = Title;
            this.Author = Author;
            this.ISBN = ISBN;
            this.PublicationYear = PublicationYear;
    }

}


public class MediaItem{
    public string Title, MediaType;
    public int Duration;

    public MediaItem(string Title, string MediaType, int Duration){
        this.Title = Title;
        this.MediaType = MediaType;
        this.Duration = Duration;

    }


}

public class Catalog{

    
    public static void Main(){
        Book book1 = new Book("Genesis", "Moses", "1234", 2020);
        Book book2 = new Book("Romans", "Paul", "1234", 2019);
        Book book3 = new Book("Joshua", "Joshua", "3333", 2017);

        List <Book>books = new List<Book> {book1, book2, book3};

        MediaItem item1 = new MediaItem("Numbers", "CD", 30);
        MediaItem item2 = new MediaItem("Revelation", "DVD", 50);

        List <MediaItem> items = new List<MediaItem>{item1, item2};
        Library Library1 = new Library("Balme", "Legon", books,items );

        Library1.AddBook(book3);
        Library1.PrintCatalog();
        Library1.RemoveBook(book3);
        Library1.PrintCatalog();
        Library1.RemoveMediaItem(item2);
        Library1.PrintCatalog();


        

    }
}