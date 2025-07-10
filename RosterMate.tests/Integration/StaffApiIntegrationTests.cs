using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using RosterMate.Application.DTOs;
using RosterMate.Domain.Enums;
using Xunit;

public class StaffApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public StaffApiIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task UpdateStaff_ReturnsNoContent_AndPersistsChange()
    {
        // Arrange: First create a staff
        var newStaff = new CreateStaffDto
        {
            FirstName = "Test",
            LastName = "User",
            Email = "test.user@example.com",
            MobileNumber = "0400000000",
            Gender = "Other",
            DateOfBirth = new DateTime(1995, 1, 1),
            Password = "Test@123",
            EmploymentDetail = new EmploymentDetailDto
            {
                Position = "QA",
                StartDate = DateTime.Now.AddYears(-1),
                EmploymentType = EmploymentType.Contract
            },
            PayrollDetail = new PayrollDetailDto
            {
                HourlyRate = 50,
                BankAccountNumber = "12345678",
                BSB = "000-000",
                TaxFileNumber = "111111111"
            }
        };

        // Call POST /api/staff
        var createResponse = await _client.PostAsJsonAsync("/api/staff", newStaff);
        if (!createResponse.IsSuccessStatusCode)
        {
            var errorContent = await createResponse.Content.ReadAsStringAsync();
            throw new Exception($"Create failed: {createResponse.StatusCode} - {errorContent}");
        }

        var createdDto = await createResponse.Content.ReadFromJsonAsync<StaffDto>();
        Assert.NotNull(createdDto);

        // Prepare update DTO with new FirstName
        var updateDto = new UpdateStaffDto
        {
            Id = createdDto.Id,
            FirstName = "Updated",
            LastName = createdDto.LastName,
            Email = createdDto.Email,
            MobileNumber = createdDto.MobileNumber,
            Gender = createdDto.Gender,
            DateOfBirth = createdDto.DateOfBirth,
            EmploymentDetail = new EmploymentDetailDto
            {
                Position = createdDto.EmploymentDetail.Position,
                StartDate = createdDto.EmploymentDetail.StartDate,
                EndDate = createdDto.EmploymentDetail.EndDate,
                EmploymentType = createdDto.EmploymentDetail.EmploymentType
            },
            PayrollDetail = new PayrollDetailDto
            {
                HourlyRate = createdDto.PayrollDetail.HourlyRate,
                BankAccountNumber = createdDto.PayrollDetail.BankAccountNumber,
                BSB = createdDto.PayrollDetail.BSB,
                TaxFileNumber = createdDto.PayrollDetail.TaxFileNumber
            }
        };

        // Call PUT /api/staff/{id}
        var updateResponse = await _client.PutAsJsonAsync($"/api/staff/{createdDto.Id}", updateDto);
        if (!updateResponse.IsSuccessStatusCode)
        {
            var errorContent = await updateResponse.Content.ReadAsStringAsync();
            throw new Exception($"Update failed: {updateResponse.StatusCode} - {errorContent}");
        }

        Assert.Equal(System.Net.HttpStatusCode.NoContent, updateResponse.StatusCode);

        // GET /api/staff/{id} and validate
        var getUpdated = await _client.GetFromJsonAsync<StaffDto>($"/api/staff/{createdDto.Id}");

        Assert.NotNull(getUpdated);
        Assert.Equal("Updated", getUpdated.FirstName);
    }
}
