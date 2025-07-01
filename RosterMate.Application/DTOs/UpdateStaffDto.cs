namespace RosterMate.Application.DTOs;

public class UpdateStaffDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? Email { get; set; }
    public string? MobileNumber { get; set; }
    public EmploymentDetailDto? EmploymentDetail { get; set; }
    public PayrollDetailDto? PayrollDetail { get; set; }
}