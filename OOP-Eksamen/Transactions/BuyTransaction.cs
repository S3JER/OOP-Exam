using System;
using System.Collections.Generic;

namespace OOP_Eksamen
{
    public class BuyTransaction : Transaction
    {
        public BuyTransaction(int id, User user, decimal amount, Product product, string date) : base(id, user, amount, date)
        {
            Product = product;
            Amount = Product.Price;
        }
        public Product Product { get; set; }

        public override string ToString()
        {
            return $"{Id};{Date};{User.Username};{Amount};{Product.Id}";
        }
        public override void Execute()
        {
            if (Product.Active)
            {
                if(User.Balance >= Product.Price)
                {
                    User.Balance -= Product.Price;
                }
                else
                {
                    if(Product.CanBeBoughtOnCredit)
                    {
                        User.Balance -= Product.Price;
                    }
                    else
                    {
                        throw new InsufficientCreditsException(User, Product);
                    }
                }
            }
            else
            {
                throw new ProductNotActiveException($"The product {Product.Name} is not activ");
            }
        }
    }
}
