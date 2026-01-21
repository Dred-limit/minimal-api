namespace MinimalApi.Models
{
    public class Fruit
    {
        public int FruitId { get; set; }

        public string Name { get; set; } = string.Empty;
        public int Stock { get; set; }

        public Fruit() { }
    }
}
