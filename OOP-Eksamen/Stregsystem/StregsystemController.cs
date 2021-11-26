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



            _adminCommands.Add(":q", (s) => _ui.Close());
            _adminCommands.Add(":Activeate", (commandParts[1]) => _stregsystem.GetProductByID(Int32.Parse(commandParts[1])));





            switch (commandParts[0])
            {
                case ":q":
                    _ui.Close();
                    break;
                case "print":
                    InputUsersFromFile();
                    break;
                default:
                    break;
            }
        }
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
    }
}