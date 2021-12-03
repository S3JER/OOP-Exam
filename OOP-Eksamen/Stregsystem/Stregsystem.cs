using System;
using System.Collections.Generic;

namespace OOP_Eksamen
{
    internal class Stregsystem : IStregsystem
    {
        List<Product> productList = new List<Product>();
        List<User> userList = new List<User>();
        public void AddProductToList(Product product) => productList.Add(product);
        public void AddUserToList(User user) => userList.Add(user);
        public IEnumerable<Product> ActiveProducts => throw new NotImplementedException();

        public event UserBalanceNotification UserBalanceWarning;
        public delegate void UserBalanceNotification(string inputstring);

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
            foreach (Product product in productList)
            {
                if (product.Id == id)
                {
                    return product;
                }
            }
            throw new ProductNotFoundException();
        }

        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            throw new NotImplementedException();
        }

        public User GetUserByUsername(string username)
        {
            foreach (User user in userList)
            {
                if(user.Username == username)
                {
                    return user;
                }
            }
            throw new UserNotFoundException(username);
        }

        public User GetUsers(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}