using RosterMate.Application.DTOs;
using RosterMate.Domain.Entities;

namespace RosterMate.Application.Interfaces
{
    public interface IStaffService
    {
        Task<IEnumerable<StaffDto>> GetAllAsync();
        Task<StaffDto?> GetByIdAsync(int id);
        Task<Staff> AddAsync(CreateStaffDto staff);
        Task UpdateAsync(UpdateStaffDto staff);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);

        Task<Staff?> GetByEmailAsync(string email);
    }
}