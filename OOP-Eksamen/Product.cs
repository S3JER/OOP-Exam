namespace OOP_Eksamen
{
    class Product
    {
        public Product(int id, string name, decimal price, bool active, bool canBeBoughtOnCredit)
        {
            Id = id;
            Name = name;
            Price = price;
            Active = active;
            CanBeBoughtOnCredit = canBeBoughtOnCredit;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
        public bool CanBeBoughtOnCredit { get; set; }

        public override string ToString()
        {
            return $"{Id} {Name} {Price}";
        }
    }
}
