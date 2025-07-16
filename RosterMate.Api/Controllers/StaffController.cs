using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RosterMate.Application.DTOs;
using RosterMate.Application.Interfaces;
using RosterMate.Domain.Entities;
using RosterMate.Domain.Interfaces;

namespace RosterMate.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        private readonly IMapper _mapper;
        public StaffController(IStaffService staffService, IMapper mapper)
        {
            _staffService = staffService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffDto>>> GetAll()

        {
            var staffList = await _staffService.GetAllAsync();
            var staffDtos = _mapper.Map<IEnumerable<StaffDto>>(staffList);
            return Ok(staffList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StaffDto>> GetById(int id)
        {
            var staff = await _staffService.GetByIdAsync(id);
            if (staff == null) return NotFound();
            var staffDto = _mapper.Map<StaffDto>(staff);
            return Ok(staffDto);
        }

        [HttpPost]
        public async Task<ActionResult<StaffDto>> Create([FromBody] CreateStaffDto staffDto)
        {
            if (staffDto == null) return BadRequest("Staff data cannot be null");
            var createdStaff = await _staffService.AddAsync(staffDto);
            if (createdStaff == null) return BadRequest("Failed to create staff");


            var createdDto = _mapper.Map<StaffDto>(createdStaff);
            return CreatedAtAction(nameof(GetById), new { id = createdDto.Id }, createdDto);
        }
   

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateStaffDto staffDto)
        {
            if (staffDto == null) return BadRequest("Staff data cannot be null");
            if (id != staffDto.Id) return BadRequest("Staff ID mismatch");
            var existingStaff = await _staffService.GetByIdAsync(id);
            if (existingStaff == null) return NotFound();

            await _staffService.UpdateAsync(staffDto);
            return NoContent();
        }
      

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var staff = await _staffService.GetByIdAsync(id);
            if (staff == null) return NotFound();
            await _staffService.DeleteAsync(staff.Id);
            return NoContent();
        }
    }
}