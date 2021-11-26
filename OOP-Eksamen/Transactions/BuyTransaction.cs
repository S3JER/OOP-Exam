using System;
using System.Collections.Generic;

namespace OOP_Eksamen
{
    class BuyTransaction : Transaction
    {
        public BuyTransaction(int id, User user, decimal amount, Product product) : base(id, user, amount)
        {
            Product = product;
            Amount = Product.Price;
        }
        public Product Product { get; set; }

        public override string ToString()
        {
            return $"{Amount} {User.Username} {Product.Name} {Date} {Id}";
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
                        throw new InsufficientCreditsException(User, Product, "The user do not have enough money to buy the product");
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
