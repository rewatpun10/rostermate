using RosterMate.Domain.Entities;

namespace RosterMate.Application.Interfaces
{
    public interface IStaffService
    {
        Task<IEnumerable<Staff>> GetAllAsync();
        Task<Staff?> GetByIdAsync(int id);
        Task<Staff> AddAsync(Staff staff);
        Task UpdateAsync(Staff staff);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}