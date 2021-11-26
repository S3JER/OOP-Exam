using System;
namespace OOP_Eksamen
{
    internal class StregsystemController
    {
        private IStregsystemUI ui;
        private IStregsystem stregsystem;

        public StregsystemController(IStregsystemUI ui, IStregsystem stregsystem)
        {
            this.ui = ui;
            this.stregsystem = stregsystem;
            ui.CommandEntered += ParseCommand;
        }
        public void ParseCommand(string command)
        {
            string[] commandParts = command.Split(' ');
            if(commandParts[0][0] == ';')
            {
                Console.WriteLine("Hej");
            }
            else
            {

            }
        }
    }
}