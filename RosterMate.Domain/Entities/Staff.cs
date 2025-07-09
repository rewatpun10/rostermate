using System.ComponentModel.DataAnnotations;

namespace RosterMate.Domain.Entities;

public class Staff
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    public string? MiddleName { get; set; }

    [Required]
    public string LastName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string? Gender { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public string MobileNumber { get; set; }   

    [Required]
    public Role Role { get; set; }

    public int? CompanyId { get; set; } //Null means SuperAdmin

    public Company? Company { get; set; }

    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }

    public EmploymentDetail? EmploymentDetail { get; set; }
    public PayrollDetail? PayrollDetail { get; set; }   
}
