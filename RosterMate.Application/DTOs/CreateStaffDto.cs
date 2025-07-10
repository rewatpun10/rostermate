namespace RosterMate.Application.DTOs;

public class CreateStaffDto
{
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string Email { get; set; }
    public string MobileNumber { get; set; }
    public string Password { get; set; } 

    public Role Role { get; set; }
    public int? CompanyId { get; set; } // Null means SuperAdmin
    public EmploymentDetailDto EmploymentDetail { get; set; }
    public PayrollDetailDto PayrollDetail { get; set; }
}   