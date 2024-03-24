namespace Example.Api.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Tire>? Tires { get; set; } = new();
    }
}
