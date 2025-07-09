using System.ComponentModel.DataAnnotations;

namespace RosterMate.Domain.Entities
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Industry { get; set; }

        public string? CompanyCode { get; set; }

        public string? Address { get; set; }

        public string? ContactEmail { get; set; }

        public string? ContactPhone { get; set; }

        public string? LogoUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Staff> Staffs { get; set; }
    }
}