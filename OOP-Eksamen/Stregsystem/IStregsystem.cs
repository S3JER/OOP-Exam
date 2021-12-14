using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Eksamen
{
    public interface IStregsystem
    {
        void AddProductToList(Product product);
        void AddUserToList(User user);
        IEnumerable<Product> ActiveProducts { get; }
        InsertCashTransaction AddCreditsToAccount(User user, Decimal amount);
        void ExecuteTransaction(Transaction transaction);
        BuyTransaction BuyProduct(User user, Product product);
        Product GetProductByID(int id);
        List<Transaction> GetTransactions(User user, int count);
        List<User> GetUsers(Func<User, bool> predicate);
        User GetUserByUsername(string username);
        void InputUsersFromFile();
        void InputProductFromFile();
        event UserBalanceNotification UserBalanceWarning;
        public delegate void UserBalanceNotification(User user, decimal balance);
    }
}
