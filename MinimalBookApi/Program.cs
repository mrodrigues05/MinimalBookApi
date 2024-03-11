var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var books = new List<Book>
{
    new Book { Id = 1, Title = "1984", Author = "George Orwell"},
    new Book { Id = 2, Title = "O Pequeno Príncipe", Author = "Anthoine de Saint-Exupéry" },
    new Book { Id = 3, Title = "Sapiens", Author = "Yuval Harari"},
    new Book { Id = 4, Title = "Crime e Castigo", Author = "Fiódor Dostoiévski"},
};

//GET/book
app.MapGet("/book", () =>
{
    return books;
});


//GET/book/{id}
app.MapGet("/book/{id}", (int id) =>
{
    var book = books.Find(b => b.Id == id);
    if (book is null)
        return Results.NotFound("Desculpe, esse livro não existe.");

    return Results.Ok(book);
});

//POST/book
app.MapPost("/book", (Book book) =>
{
    books.Add(book);
    return books;
});

//PUT/book/{id}
app.MapPut("/book/{id}", (Book updateBook, int id) =>
{
    var book = books.Find(b => b.Id == id);
    if (book is null)
        return Results.NotFound("Desculpe, esse livro não existe.");

    book.Title= updateBook.Title;
    book.Author= updateBook.Author;

    return Results.Ok(book);
});

//PUT/book/{id}
app.MapDelete("/book/{id}", (int id) =>
{
    var book = books.Find(b => b.Id == id);
    if (book is null)
        return Results.NotFound("Desculpe, esse livro não existe.");

    books.Remove(book);

    return Results.Ok(book);
});

app.Run();

class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }

}