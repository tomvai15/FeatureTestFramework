namespace Example.Api.Models
{
    public class Tire
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid? ManufacturerCode { get; set; }
        public int CarId { get; set; }
    }
}
