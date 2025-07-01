using AutoMapper;
using RosterMate.Application.DTOs;
using RosterMate.Application.Interfaces;
using RosterMate.Domain.Entities;
using RosterMate.Domain.Interfaces;

namespace RossteraMate.Application.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        private readonly IMapper _mapper;

        public StaffService(IStaffRepository staffRepository, IMapper mapper)
        {
            _staffRepository = staffRepository;
            _mapper = mapper;

        }

        public async Task<IEnumerable<StaffDto>> GetAllAsync()
        {
            var staffList = await _staffRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StaffDto>>(staffList);
        }
        public async Task<StaffDto?> GetByIdAsync(int id)
        {
            var staff = await _staffRepository.GetByIdAsync(id);
            if (staff == null) return null;

            return _mapper.Map<StaffDto>(staff);
        }

        public async Task<Staff> AddAsync(CreateStaffDto staffDto)
        {
            if (staffDto == null) throw new ArgumentNullException(nameof(staffDto), "Staff Dto cannot be null.");

            var staff = _mapper.Map<Staff>(staffDto);

            var created = await _staffRepository.AddAsync(staff);
            return created;
            
        }

        public async Task UpdateAsync(UpdateStaffDto staffDto)

        {
            var existing = await _staffRepository.GetByIdAsync(staffDto.Id);
            if (existing == null) throw new Exception("Staff not found.");  

            _mapper.Map(staffDto, existing);
            await _staffRepository.UpdateAsync(existing);
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