using RosterMate.Application.DTOs;

public class StaffDto
{
    public int Id { get; set; }

    // Personal Info
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }

    // Contact
    public string Email { get; set; }
    public string MobileNumber { get; set; }

    // Related Info (flattened to avoid circular refs)
    public EmploymentDetailDto EmploymentDetail { get; set; }
    public PayrollDetailDto PayrollDetail { get; set; }
}
