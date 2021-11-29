using System;
using System.Collections.Generic;
using System.IO;

namespace OOP_Eksamen
{
    internal class StregsystemController
    {
        private IStregsystemUI _ui;
        private IStregsystem _stregsystem;

        Dictionary<string, Action<string>> _adminCommands = new Dictionary<string, Action<string>>();

        public StregsystemController(IStregsystemUI ui, IStregsystem stregsystem)
        {
            _ui = ui;
            _stregsystem = stregsystem;
            ui.CommandEntered += ParseCommand;
           
        }
        public void ParseCommand(string command)
        {
            string[] commandParts = command.Split(' ');

            bool isNumber = false;
            if (commandParts.Length > 1)
            {
                for (int i = 0; i < commandParts[1].Length; i++)
                {
                    if (char.IsDigit(commandParts[1][i]))
                    {
                        isNumber = true;
                    }
                    if (char.IsLetter(commandParts[1][i]))
                    {
                        isNumber = false;
                        break;
                    }
                }
            }
            if (isNumber)
            {
                Console.WriteLine($"{commandParts[1]}");
            }



          /*  _adminCommands.Add(":q", (s) => _ui.Close());
            _adminCommands.Add(":Activeate", (commandParts[1]) => _stregsystem.GetProductByID(Int32.Parse(commandParts[1])));
*/




            switch (commandParts[0])
            {
                case ":q":
                    _ui.Close();
                    break;
                case "printUser":
                    InputUsersFromFile();
                    break;
                case "printProduct":
                    InputProductFromFile();
                    break;
                default:
                    break;
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
            List<User> userList = new List<User>();
            for (int i = 1; i < lines.Length; i++)
            {
                line = lines[i].Split(',');
                userList.Add(new User(line[3], line[1], line[2], line[5], Decimal.Parse(line[4]), Int32.Parse(line[0])));
            }
            foreach (var item in userList)
            {
                Console.WriteLine(item.ToString());
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
            List<Product> productList = new List<Product>();
            for (int i = 1; i < lines.Length; i++)
            {
                line = lines[i].Split(';');
                if (line.Length == 4)
                {
                    productList.Add(new Product(Int32.Parse(line[0]), RemoveHtmlTags(line[1]), Decimal.Parse(line[2]), CheckForTrueOrFalse(line[3]), false));
                }
                else if (line.Length == 5)
                {
                    productList.Add(new SeasonalProduct(Int32.Parse(line[0]), RemoveHtmlTags(line[1]), Decimal.Parse(line[2]), CheckForTrueOrFalse(line[3]), false, DateTime.Now.ToString("yyyy/MM/dd HH:mm"), line[4]));
                }
            }
            foreach (var item in productList)
            {
                Console.WriteLine(item.ToString());
            }
        }

        /// <summary>
        /// Checks if the string value is equal to 1, if so return true, else false.
        /// </summary>
        /// <param name="value">The string.</param>
        /// <returns></returns>
        private bool CheckForTrueOrFalse(string value) =>Int32.Parse(value) == 1 ? true : false;

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