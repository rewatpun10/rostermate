using Microsoft.EntityFrameworkCore;
using RosterMate.Domain.Entities;
using RosterMate.Domain.Interfaces;
using RosterMate.Infrastructure.Data;

namespace RosterMate.Infrastructure.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly ApplicationDbContext _context;

        public StaffRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Staff>> GetAllAsync()
        {
            return await _context.Staff
                   .Include(s => s.EmploymentDetail)
                   .Include(s => s.PayrollDetail)
                   .ToListAsync();
        }

        public async Task<Staff?> GetByIdAsync(int id)
        {
            return await _context.Staff
                .Include(s => s.EmploymentDetail)
                .Include(s => s.PayrollDetail)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Staff> AddAsync(Staff staff)
        {
            await _context.Staff.AddAsync(staff);
            await _context.SaveChangesAsync();
            return staff;
        }

        public async Task UpdateAsync(Staff staff)
        {
            _context.Staff.Update(staff);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var staff = await GetByIdAsync(id);
            if (staff != null)
            {
                _context.Staff.Remove(staff);
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Staff.AnyAsync(s => s.Id == id);
        }
        

    }
}