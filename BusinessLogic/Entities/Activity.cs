namespace BusinessLogic.Entities;
public class Activity
{
    
        public Guid ActivityId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Datetime { get; set; }
        public string Description { get; set; } = null!;
        public Guid? EventId { get; set; }
        public bool IsRegistered { get; set; }

        public virtual Event? Event { get; set; }
    
}