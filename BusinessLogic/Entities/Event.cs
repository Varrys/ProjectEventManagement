namespace BusinessLogic.Entities;

public class Event
{
    public Guid EventId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime Date { get; set; }
    public string Location { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int MaxCapacity { get; set; }
    public Guid? UserId { get; set; }

    public virtual User? User { get; set; }
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    public virtual ICollection<UserEvent> UserEvents { get; set; } = new List<UserEvent>();
}