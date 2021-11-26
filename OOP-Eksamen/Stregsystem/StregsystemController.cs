using System;
using System.Collections.Generic;
using System.IO;

namespace OOP_Eksamen
{
    internal class StregsystemController
    {
        private IStregsystemUI _ui;
        private IStregsystem _stregsystem;

        Dictionary<string, Action> _adminCommands = new Dictionary<string, Action>();

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



            _adminCommands.Add(":q", () => _ui.Close()); ;
            _adminCommands.Add(":Activeate", () => _stregsystem.GetProductByID());





            switch (commandParts[0])
            {
                case ":q":
                    _ui.Close();
                    break;
                case ":activate":
                    
                    break;
                case ":deactivate":
                    break;
                case ":crediton":
                    break;
                case ":creditoff":
                    break;
                case ":addcredits":
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
            for (int i = 0; i < lines.Length;i++)
            {

            }
        }
    }
}