namespace Example.Api.Models;

public record ValidateDatesRequest
{
    public DateTime Past { get; set; }
    public DateTime Today { get; set; }
    public DateTime Future { get; set; }
    public DateTime NoDate { get; set; }
    public DateTime JustAfterMidnight { get; set; }
    public DateTime JustBeforeMidnight { get; set; }
    public DateTime Midnight { get; set; }
}