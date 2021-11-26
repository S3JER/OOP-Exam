using System;
namespace OOP_Eksamen
{
    internal class StregsystemCLI : IStregsystemUI
    {
        private readonly IStregsystem stregsystem;

        public bool Running { get; private set; }

        public StregsystemCLI(IStregsystem stregsystem)
        {
            this.stregsystem = stregsystem;
        }

        public event IStregsystemUI.StregsystemEvent CommandEntered;
        public delegate void StregsystemEvent(string inputstring);

        public void Close()
        {
            Running = false;
        }

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            throw new NotImplementedException();
        }

        public void DisplayGeneralError(string errorString)
        {
            throw new NotImplementedException();
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            throw new NotImplementedException();
        }

        public void DisplayProductNotFound(string product)
        {
            throw new NotImplementedException();
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            throw new NotImplementedException();
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void DisplayUserInfo(User user)
        {
            throw new NotImplementedException();
        }

        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine($"User [{username}] not found!");
        }

        public void Start()
        {
            Running = true;
            while (Running)
            {
                string command = Console.ReadLine();
                CommandEntered.Invoke(command);
            }
        }
        public event StregsystemEvent CommandEvent;
    }
}