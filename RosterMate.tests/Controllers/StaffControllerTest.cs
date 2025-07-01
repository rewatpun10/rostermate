using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RosterMate.Api.Controllers;
using RosterMate.Application.DTOs;
using RosterMate.Application.Interfaces;
using RosterMate.Domain.Entities;
using RosterMate.Domain.Enums;

public class StaffControllerTest
{
    private readonly Mock<IStaffService> _staffServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly StaffController _controller;

    public StaffControllerTest()
    {
        _staffServiceMock = new Mock<IStaffService>();
        _mapperMock = new Mock<IMapper>();
        _controller = new StaffController(_staffServiceMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtActionResult_WithStaffDto()
    {
        // Arrange
        var createDto = new CreateStaffDto
        {
            FirstName = "Robert",
            MiddleName = "Ram",
            LastName = "Poon",
            DateOfBirth = new DateTime(1990, 6, 15),
            Gender = "Male",
            Email = "Robert.poon@example.com",
            MobileNumber = "0412345678",
            EmploymentDetail = new EmploymentDetailDto
            {
                Position = "Senior Developer",
                StartDate = new DateTime(2023, 2, 1),
                EndDate = null,
                EmploymentType = EmploymentType.FullTime
            },
            PayrollDetail = new PayrollDetailDto
            {
                HourlyRate = 60.00m,
                BankAccountNumber = "12345678",
                BSB = "062-000",
                TaxFileNumber = "123456789"
            }
        };

      var createdEntity = new Staff
        {
            Id = 1,
            FirstName = "Robert",
            LastName = "Poon",
        };

        var staffDtoResult = new StaffDto
        {
            Id = 1,
            FirstName = "Robert",
            LastName = "Poon",
        };

        _staffServiceMock.Setup(s => s.AddAsync(createDto)).ReturnsAsync(createdEntity);
        _mapperMock.Setup(m => m.Map<StaffDto>(createdEntity)).Returns(staffDtoResult);

        // Act
        var result = await _controller.Create(createDto);

        // Assert
        var createdAt = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<StaffDto>(createdAt.Value);
        Assert.Equal("Robert", returnValue.FirstName);
    }

    [Fact]
    public async Task Update_ReturnsNoContent_WhenSuccessful()
    {
        // Arrange
        var updateDto = new UpdateStaffDto
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            EmploymentDetail = new EmploymentDetailDto(),
            PayrollDetail = new PayrollDetailDto()
        };

        var existing = new StaffDto { Id = 1 };

        _staffServiceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(existing);

        _staffServiceMock.Setup(s => s.UpdateAsync(updateDto)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Update(1, updateDto);

        // Assert
        Assert.IsType<NoContentResult>(result);
        }
}
