namespace EffectiveCSharp
{
    public class Customer
    {
        public Customer() { }

        public Customer(string title)
        {
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
}