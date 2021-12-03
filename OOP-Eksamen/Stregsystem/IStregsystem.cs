using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Eksamen
{
    interface IStregsystem
    {
        void AddProductToList(Product product);
        void AddUserToList(User user);
        void AddTransaction(Transaction transaction);
        public List<Transaction> ReturnTransactionList();
        IEnumerable<Product> ActiveProducts { get; }
        InsertCashTransaction AddCreditsToAccount(User user, Decimal amount);
        BuyTransaction BuyProduct(User user, Product product);
        Product GetProductByID(int id);
        IEnumerable<Transaction> GetTransactions(User user, int count);
        User GetUsers(Func<User, bool> predicate);
        User GetUserByUsername(string username);
        //event UserBalanceNotification UserBalanceWarning;
    }
}
