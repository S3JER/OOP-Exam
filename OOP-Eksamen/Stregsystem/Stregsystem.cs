using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_Eksamen
{
    internal class Stregsystem : IStregsystem
    {
        List<Product> productList = new List<Product>();
        List<User> userList = new List<User>();
        List<Transaction> transactionsList = new List<Transaction>();
        public void AddProductToList(Product product) => productList.Add(product);
        public void AddUserToList(User user) => userList.Add(user);
        public List<Transaction> ReturnTransactionList() { return transactionsList; }
        public void AddTransaction(Transaction transaction) => transactionsList.Add(transaction);
        public IEnumerable<Product> ActiveProducts => productList.Where(p => p.Active);

        public event UserBalanceNotification UserBalanceWarning;
        public delegate void UserBalanceNotification(string inputstring);

        public InsertCashTransaction AddCreditsToAccount(User user, Decimal amount)
        {
            int id = transactionsList.Count;
            InsertCashTransaction transaction = new InsertCashTransaction(id+1, user, amount);
            transaction.Execute();
            transactionsList.Add(transaction);
            return transaction;
        }

        public BuyTransaction BuyProduct(User user, Product product)
        {
            int id = transactionsList.Count;
            if (product.Active || (product.Active && product.CanBeBoughtOnCredit))
            {
                BuyTransaction transaction = new BuyTransaction(id + 1, user, product.Price, product);
                transaction.Execute();
                transactionsList.Add(transaction);
                return transaction;
            }
            throw new ProductNotFoundException();

        }

        public Product GetProductByID(int id)
        {
            foreach (Product product in ActiveProducts)
            {
                if (product.Id == id)
                {
                    return product;
                }
            }
            throw new ProductNotFoundException();
        }

        public IEnumerable<Transaction> GetTransactions(User user, int count) => transactionsList.Where(t => t.User == user).OrderBy(t => t.Date).Take(count);

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
            var res = userList.Where(predicate);
            List<User> filteredUserList = new List<User>();

            foreach (User u in res)
            {
                return u;
            }
            throw new NoUsersFoundException();
        }
    }
}