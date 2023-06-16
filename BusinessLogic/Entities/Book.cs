namespace BusinessLogic.Entities;

public class Book
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public int PublicationYear { get; set; }

    public Guid? AuthorId { get; set; }

    public string Status { get; set; } = null!;

    public virtual Author? Author { get; set; }
}