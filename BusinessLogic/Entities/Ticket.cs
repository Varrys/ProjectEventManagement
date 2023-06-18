namespace BusinessLogic.Entities;

public class Ticket
{
    public Guid TicketId { get; set; }
    public decimal Price { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Quantity { get; set; }
    public Guid EventId { get; set; }
}