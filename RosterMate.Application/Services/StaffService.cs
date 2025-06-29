using RosterMate.Application.Interfaces;
using RosterMate.Domain.Entities;
using RosterMate.Domain.Interfaces;

namespace RossteraMate.Application.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task<IEnumerable<Staff>> GetAllAsync()
        {
            return await _staffRepository.GetAllAsync();
        }
        public async Task<Staff?> GetByIdAsync(int id)
        {
            return await _staffRepository.GetByIdAsync(id);
        }

        public async Task<Staff> AddAsync(Staff staff)
        {
            if (staff == null) throw new ArgumentNullException(nameof(staff));
            return await _staffRepository.AddAsync(staff);
        }

        public async Task UpdateAsync(Staff staff)
        {
            if (staff == null) throw new ArgumentNullException(nameof(staff));
            await _staffRepository.UpdateAsync(staff);
        }

        public async Task DeleteAsync(int id)
        {
            var staff = await _staffRepository.GetByIdAsync(id);
            if (staff == null) throw new KeyNotFoundException($"Staff with ID {id} not found.");
            await _staffRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _staffRepository.ExistsAsync(id);
        }
    }
}