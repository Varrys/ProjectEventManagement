namespace BusinessLogic.Entities;

public class User
{
    public Guid UserId { get; set; }

    public string Email { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Role { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public bool Enable { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
    public virtual ICollection<UserEvent> UserEvents { get; set; } = new List<UserEvent>();
}