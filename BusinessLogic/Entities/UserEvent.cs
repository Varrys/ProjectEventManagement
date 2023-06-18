namespace BusinessLogic.Entities;

public class UserEvent
{
    public Guid EventId { get; set; }
    public Guid UserId { get; set; }
    public string Feedback { get; set; } = null!;
   
}