using System.ComponentModel.DataAnnotations;

namespace csharp_reference.Models
{
    public class Property
    {
        [Key]
        public int Id { get; set; }
        public string PropertyType { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public int SquareMeters { get; set;  }
        public int NumberOfRooms { get; set; }

    }
}
