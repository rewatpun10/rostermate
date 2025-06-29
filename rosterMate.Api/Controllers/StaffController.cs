using Microsoft.AspNetCore.Mvc;
using RosterMate.Domain.Entities;
using RosterMate.Domain.Interfaces;

namespace RosterMate.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly IStaffRepository _staffRepository;
        public StaffController(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> GetAll()

        {
            var staffList = await _staffRepository.GetAllAsync();
            return Ok(staffList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetById(int id)
        {
            var staff = await _staffRepository.GetByIdAsync(id);
            if (staff == null) return NotFound();
            return Ok(staff);
        }

        [HttpPost]
        public async Task<ActionResult<Staff>> Create([FromBody] Staff staff)
        {
            if (staff == null) return BadRequest("Staff cannot be null");
            await _staffRepository.AddAsync(staff);
            return CreatedAtAction(nameof(GetById), new { id = staff.Id }, staff);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Staff staff)
        {
            if (id != staff.Id) return BadRequest("Staff ID mismatch");
            if (staff == null) return BadRequest("Staff cannot be null");
            var existingStaff = await _staffRepository.GetByIdAsync(id);
            if (existingStaff == null) return NotFound();
            await _staffRepository.UpdateAsync(staff);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var staff = await _staffRepository.GetByIdAsync(id);
            if (staff == null) return NotFound();
            await _staffRepository.DeleteAsync(staff.Id);
            return NoContent();
        }
    }
}