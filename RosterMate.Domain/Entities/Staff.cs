using System.ComponentModel.DataAnnotations;

namespace RosterMate.Domain.Entities;

public class Staff
{
    [Key]
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string? MiddleName { get; set; }
    public string LastName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string Gender { get; set; }

    public string Email { get; set; }

    public string MobileNumber { get; set; }   

    public EmploymentDetail EmploymentDetail { get; set; }
    public PayrollDetail PayrollDetail { get; set; }   
}
