using System;
using System.Collections.Generic;

namespace OOP_Eksamen
{
    internal class Stregsystem : IStregsystem
    {
        public IEnumerable<Product> ActiveProducts => throw new NotImplementedException();

        /*public event UserBalanceNotification UserBalanceWarning;
        public delegate void UserBalanceNotification(string inputstring);*/

        public InsertCashTransaction AddCreditsToAccount(User user, int amount)
        {
            return new InsertCashTransaction(1, user, amount);
        }

        public BuyTransaction BuyProduct(User user, Product product)
        {
            return new BuyTransaction(1, user, product.Price, product);
        }

        public Product GetProductByID(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            throw new NotImplementedException();
        }

        public User GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public User GetUsers(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}