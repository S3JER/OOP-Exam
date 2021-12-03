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
            Console.WriteLine($"{user.Username} do not have sufficent enough stregdollers for {product.Name}");
        }

        public void DisplayProductNotFound(string product)
        {
            Console.WriteLine($"The product: {product} do not exist.");
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            throw new NotImplementedException();
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            transaction.Execute();
            Console.WriteLine($"{transaction.User} {transaction.Product.Name}");
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            decimal value = transaction.Amount*(decimal)count;
            Console.WriteLine($"{transaction.User} {transaction.Product.Name} {count}");
        }

        public void DisplayUserInfo(User user)
        {
            Console.WriteLine($"{user.Firstname} {user.Lastname} {user.Email} {user.Balance}");
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
        //public event StregsystemEvent CommandEvent;
    }
}