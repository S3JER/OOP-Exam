using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace OOP_Eksamen
{
    public class Stregsystem : IStregsystem
    {
        List<Product> productList = new List<Product>();
        List<User> userList = new List<User>();
        public void AddProductToList(Product product) => productList.Add(product);
        public void AddUserToList(User user) => userList.Add(user);
        public IEnumerable<Product> ActiveProducts => productList.Where(p => p.Active);

        public event IStregsystem.UserBalanceNotification UserBalanceWarning;
        public delegate void UserBalanceNotification(User user, decimal balance);

        public InsertCashTransaction AddCreditsToAccount(User user, Decimal amount)
        {
            int id = FindNewId();
            InsertCashTransaction transaction = new InsertCashTransaction(id, user, amount, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            ExecuteTransaction(transaction);
            return transaction;
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            transaction.Execute();
            if(transaction.User.Balance < 50)
            {
                UserBalanceWarning?.Invoke(transaction.User, transaction.User.Balance);
            }
            InputTransactionToFile(transaction);
        }

        public BuyTransaction BuyProduct(User user, Product product)
        {
            int id = FindNewId();
            BuyTransaction transaction = new BuyTransaction(id, user, product.Price, product, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            ExecuteTransaction(transaction);
            return transaction;
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
            throw new ProductNotFoundException(id.ToString());
        }

        public List<Transaction> GetTransactions(User user, int count) => TransactionList().Where(t => t.User.Equals(user)).OrderByDescending(t => t.Date).Take(count).ToList();

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

        public List<User> GetUsers(Func<User, bool> predicate)
        {
            List<User> res = userList.Where(predicate).ToList();
            if(res.Count > 0)
            {
                return res;
            }
            throw new NoUsersFoundException();
        }
        private int FindNewId()
        {
            string path = @"..\..\..\LogFiles\transactions.csv";
            return File.ReadAllLines(path).Length;
        }
        /// <summary>
        /// Read all the transactions from the logfile.
        /// </summary>
        /// <returns>A list with all the transactions made.</returns>
        private List<Transaction> TransactionList()
        {
            List <Transaction> transactions = new List <Transaction>();
            string path = @"..\..\..\LogFiles\transactions.csv";
            string[] lines = File.ReadAllLines(path);
            string[] line;
            for (int i = 1; i < lines.Length; i++)
            {
                line = lines[i].Split(';').ToArray();
                if(line.Length == 5)
                {
                    transactions.Add(new BuyTransaction(Int32.Parse(line[0]), GetUserByUsername(line[2]), Int32.Parse(line[3]), GetProductByID(Int32.Parse(line[4])), line[1]));
                }
                else if(line.Length == 4)
                {
                    transactions.Add(new InsertCashTransaction(Int32.Parse(line[0]), GetUserByUsername(line[2]), Int32.Parse(line[3]), line[1]));
                }
            }
            return transactions;
        }
        private void InputTransactionToFile(Transaction transaction)
        {
            string filePath = @"..\..\..\LogFiles\transactions.csv";
            List<string> lines = File.ReadAllLines(filePath).ToList();
            lines.Add(transaction.ToString());
            File.WriteAllLines(filePath, lines);
        }
        /// <summary>
        /// Constructs all the users in the system from the users.csv file.
        /// </summary>
        public void InputUsersFromFile()
        {
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = @"..\..\..\LogFiles\users.csv";
            string sFile = Path.Combine(sCurrentDirectory, path);
            string[] lines = File.ReadAllLines(sFile);
            string[] line;
            for (int i = 1; i < lines.Length; i++)
            {
                line = lines[i].Split(',');
                AddUserToList(new User(line[3], line[1], line[2], line[5], Decimal.Parse(line[4]) / 100, Int32.Parse(line[0])));
            }
        }

        /// <summary>
        /// Constructs all the products in the system from the product.csv file.
        /// </summary>
        public void InputProductFromFile()
        {
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = @"..\..\..\LogFiles\products.csv";
            string sFile = Path.Combine(sCurrentDirectory, path);
            string[] lines = File.ReadAllLines(sFile);
            string[] line;
            for (int i = 1; i < lines.Length; i++)
            {
                line = lines[i].Split(';');
                if (line.Length == 4)
                {
                    AddProductToList(new Product(Int32.Parse(line[0]), RemoveHtmlTags(line[1]), Decimal.Parse(line[2]) / 100, CheckForTrueOrFalse(line[3]), false));
                }
                else if (line.Length == 5)
                {
                    AddProductToList(new SeasonalProduct(Int32.Parse(line[0]), RemoveHtmlTags(line[1]), Decimal.Parse(line[2]) / 100, CheckForTrueOrFalse(line[3]), false, DateTime.Now.ToString("yyyy/MM/dd HH:mm"), line[4]));
                }
            }
        }

        /// <summary>
        /// Checks if the string value is equal to 1, if so return true, else false.
        /// </summary>
        /// <param name="value">The string.</param>
        /// <returns></returns>
        private bool CheckForTrueOrFalse(string value) => Int32.Parse(value) == 1 ? true : false;

        /// <summary>
        /// Replaces different html tags from a string with an empty string.
        /// </summary>
        /// <param name="value">The string.</param>
        /// <returns></returns>
        private string RemoveHtmlTags(string value)
        {
            return value.Replace("<h1>", "").Replace("<h2>", "").Replace("<h3>", "").Replace("<b>", "").Replace("</h1>", "").Replace("</h2>", "").Replace("</h3>", "").Replace("</b>", "");
        }
    }
}