using RosterMate.Domain.Enums;
using RosterMate.Domain.Entities;

public class EmploymentDetail
{
    public int Id { get; set; }
    public string Position { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public EmploymentType EmploymentType { get; set; } // e.g., Full-time, Part-time, Casual

    // Navigation property

    public int StaffId { get; set; }

    public Staff? Staff { get; set; }
}