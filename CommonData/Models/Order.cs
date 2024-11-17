using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required, MaxLength(32)]
        public string Name { get; set; }

        [Required, MaxLength(64)]
        public string Email { get; set; }
        public string Message { get; set; }
        public int StatusId { get; set; }
    }
}
