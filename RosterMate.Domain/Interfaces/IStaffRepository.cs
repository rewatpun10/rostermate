using RosterMate.Domain.Entities;

namespace RosterMate.Domain.Interfaces
{
    public interface IStaffRepository
    {
        Task<IEnumerable<Staff>> GetAllAsync();
        Task<Staff> GetByIdAsync(int id);
        Task<Staff> AddAsync(Staff staff);
        Task UpdateAsync(Staff staff);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        
        Task<Staff?> GetByEmailAsync(string email);
    }
}