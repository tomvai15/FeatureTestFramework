namespace Example.Api.Models;

public class ValidateNumbersRequest
{
    public long AnyId { get; set; }
    public long NotExistingId { get; set; }
    public decimal AnyNegativeNumber { get; set; }
    public decimal AnyPositiveNumber { get; set; }
    public long AnyNumber { get; set; }
    public decimal AnyDecimal { get; set; }
}