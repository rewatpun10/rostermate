using RosterMate.Domain.Entities;
public class PayrollDetail
{
    public int Id { get; set; }
    public decimal HourlyRate { get; set; }
    public string BankAccountNumber { get; set; }
    public string BSB { get; set; }
    public string TaxFileNumber { get; set; }

    public int StaffId { get; set; }
    public Staff? Staff { get; set; }
}
