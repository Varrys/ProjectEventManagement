using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Entities
{
    public class Feedback
    {
        [Key]
        public Guid FeedbackId { get; set; }
        
        public Guid UserId { get; set; }
        
        public Guid EventId { get; set; }
        
       public DateTime Datetime { get; set; }
       
        public string FeedbackT { get; set; }
        
        public int Value { get; set; }
    }
}