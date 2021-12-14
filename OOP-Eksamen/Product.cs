using System;

namespace OOP_Eksamen
{
    public class Product
    {
        private int _id;
        private string _name;
        public Product(int id, string name, decimal price, bool active, bool canBeBoughtOnCredit)
        {
            if(id !< 1)
            {
                throw new IdException(id);
            }
            Id = id;
            Name = name;
            Price = price;
            Active = active;
            CanBeBoughtOnCredit = canBeBoughtOnCredit;
        }

        public int Id { 
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if(value != null)
                {
                    _name = value;
                }
                else
                {
                    throw new ArgumentNullException("Product name cannot be null.");
                }
            }
        }
        public decimal Price { get; set; }
        public bool Active { get; set; }
        public bool CanBeBoughtOnCredit { get; set; }

        public override string ToString()
        {
            return $"{Id} {Name} {Price}";
        }
    }
}
