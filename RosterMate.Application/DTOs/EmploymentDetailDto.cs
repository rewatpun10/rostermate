using RosterMate.Domain.Enums;
namespace RosterMate.Application.DTOs;

public class EmploymentDetailDto
{
        public string Position { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public EmploymentType EmploymentType { get; set; }
}