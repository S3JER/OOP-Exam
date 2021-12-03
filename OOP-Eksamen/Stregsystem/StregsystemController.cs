using System;
using System.Collections.Generic;
using System.IO;

namespace OOP_Eksamen
{
    internal class StregsystemController
    {
        private IStregsystemUI _ui;
        private IStregsystem _stregsystem;

        Dictionary<string, Action<string[]>> _adminCommands = new Dictionary<string, Action<string[]>>();
        Dictionary<string, Action<string[]>> _usereCommand = new Dictionary<string, Action<string[]>>();



        public StregsystemController(IStregsystemUI ui, IStregsystem stregsystem)
        {
            _ui = ui;
            _stregsystem = stregsystem;
            ui.CommandEvent += ParseCommand;
            InputUsersFromFile();
            InputProductFromFile();
            InputTransactionToFile();

            _adminCommands.Add(":quit", (s) => _ui.Close());
            _adminCommands.Add(":activate", (s) => _stregsystem.GetProductByID(Int32.Parse(s[1])).Active = true);
            _adminCommands.Add(":deactivate", (s) => _stregsystem.GetProductByID(Int32.Parse(s[1])).Active = false);
            _adminCommands.Add(":crediton", (s) => _stregsystem.GetProductByID(Int32.Parse(s[1])).CanBeBoughtOnCredit = true);
            _adminCommands.Add(":creditoff", (s) => _stregsystem.GetProductByID(Int32.Parse(s[1])).CanBeBoughtOnCredit = false);
            _adminCommands.Add(":addcredits", (s) => _stregsystem.AddCreditsToAccount(_stregsystem.GetUserByUsername(s[1]), Decimal.Parse(s[2])));
        }
        public void ParseCommand(string command)
        {
            string[] commandParts = command.Split(' ');
            Action<string[]> outPut;
            if (_adminCommands.TryGetValue(commandParts[0], out outPut))
            {
                try
                {
                    outPut.Invoke(commandParts);
                }
                catch (UserNotFoundException)
                {
                    _ui.DisplayUserNotFound(commandParts[1]);
                }
                catch (ProductNotFoundException)
                {
                    _ui.DisplayProductNotFound(commandParts[1]);
                }
                catch (InsufficientCreditsException)
                {
                    User user = _stregsystem.GetUserByUsername(commandParts[0]);
                    Product product = _stregsystem.GetProductByID(Int32.Parse(commandParts[1]));
                    _ui.DisplayInsufficientCash(user, product);
                }
            }
            else
            {
                try
                {
                    if (commandParts.Length == 1)
                    {
                        User user = _stregsystem.GetUserByUsername(commandParts[0]);
                        _ui.DisplayUserInfo(user);
                    }

                    else if (commandParts.Length == 2)
                    {
                        User user = _stregsystem.GetUserByUsername(commandParts[0]);
                        Product product = _stregsystem.GetProductByID(Int32.Parse(commandParts[1]));
                        if (product.Active)
                        {
                            _ui.DisplayUserBuysProduct(_stregsystem.BuyProduct(user, product));
                        }
                    }
                    else if (commandParts.Length == 3)
                    {
                        int count = Int32.Parse(commandParts[2]);
                        if (count > 0)
                        {
                            User user = _stregsystem.GetUserByUsername(commandParts[0]);
                            Product product = _stregsystem.GetProductByID(Int32.Parse(commandParts[1]));
                            if (product.Active)
                            {
                                for (int i = 1; i <= count; i++)
                                {
                                    _ui.DisplayUserBuysProduct(_stregsystem.BuyProduct(user, product));
                                }
                            }
                        }
                    }
                }
                catch(UserNotFoundException)
                {
                    _ui.DisplayUserNotFound(commandParts[0]);
                }
                catch (ProductNotFoundException)
                {
                    _ui.DisplayProductNotFound(commandParts[1]);
                }
                catch (InsufficientCreditsException)
                {
                    User user = _stregsystem.GetUserByUsername(commandParts[0]);
                    Product product = _stregsystem.GetProductByID(Int32.Parse(commandParts[1]));
                    _ui.DisplayInsufficientCash(user, product);
                }
            }
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
                _stregsystem.AddUserToList(new User(line[3], line[1], line[2], line[5], Decimal.Parse(line[4]), Int32.Parse(line[0])));
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
                    _stregsystem.AddProductToList(new Product(Int32.Parse(line[0]), RemoveHtmlTags(line[1]), Decimal.Parse(line[2]), CheckForTrueOrFalse(line[3]), false));
                }
                else if (line.Length == 5)
                {
                    _stregsystem.AddProductToList(new SeasonalProduct(Int32.Parse(line[0]), RemoveHtmlTags(line[1]), Decimal.Parse(line[2]), CheckForTrueOrFalse(line[3]), false, DateTime.Now.ToString("yyyy/MM/dd HH:mm"), line[4]));
                }
            }
        }

        public void InputTransactionToFile()
        {
            string path = @"..\..\..\LogFiles\transactions.csv";
            string[] lines = File.ReadAllLines(path);
            _stregsystem.ReturnTransactionList();
            int i = 1;
            foreach (Transaction transaction in _stregsystem.ReturnTransactionList())
            {
                lines[i] = transaction.ToString();
                i++;
            }
            File.WriteAllLines(path, lines);
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