using System;
using System.Collections.Generic;

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

        public event IStregsystemUI.StregsystemEvent CommandEvent;
        public delegate void StregsystemEvent(string inputstring);

        public void Close()
        {
            Running = false;
        }

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            Console.WriteLine($"The admincommand {adminCommand} do not exist");
        }

        public void DisplayGeneralError(string errorString)
        {
            Console.WriteLine(errorString);
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            Console.WriteLine($"{user.Username} do not have sufficent enough stregdollers for {product.Name}");
        }

        public void DisplayProductNotFound(string product)
        {
            Console.WriteLine($"The product id: {product} do not exist.");
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            Console.WriteLine($"To manny arguments: {command}");
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine($"{transaction.User} {transaction.Product.Name}");
        }

        public void DisplayUserBuysProduct(int count, List<BuyTransaction> transactions)
        {
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"{transactions[i].User} {transactions[i].Product.Name}");
            }
        }

        public void DisplayUserInfo(User user)
        {
            List<Transaction> transactions = stregsystem.GetTransactions(user, 5);
            Console.WriteLine("Your last 5 transactions:");
            foreach (Transaction transaction in transactions)
            {
                Console.WriteLine(" * " + transaction);
            }
            Console.WriteLine();
            if(user.Balance < 50)
            {
                Console.WriteLine("Your balance are under 50 DKK.");
            }
            Console.WriteLine("Your information: " + user + $" balance:{user.Balance}");
        }

        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine($"User [{username}] not found!");
        }

        public void DisplayBalanceWarning(User user, decimal balance)
        {
            Console.WriteLine("Balance notification: " + user.ToString() + " balance: " + balance.ToString());
        }

        public void Start()
        {
            Running = true;
            DisplayProducts();
            while (Running)
            {
                string command = Console.ReadLine();
                DisplayProducts();
                CommandEvent.Invoke(command);
            }
        }

        private void CreateTable()
        {
            Console.WriteLine("You can buy with two different methods: ");
            Console.WriteLine("| Id  |               Product                  | Price | ");
            CreateLine();
            foreach (Product product in stregsystem.ActiveProducts)
            {
                string id = product.Id.ToString();
                string productname = product.Name.ToString();
                string price = product.Price.ToString();
                Console.Write("|");
                Console.Write(id.PadRight(5));
                Console.Write("|");
                Console.Write(productname.PadRight(40));
                Console.Write("|");
                Console.Write(price.PadRight(7));
                Console.WriteLine("|");
                CreateLine();
            }
        }

        private void CreateLine()
        {
            Console.WriteLine("|-----|----------------------------------------|-------|");
        }

        public void DisplayProducts()
        {
            Console.Clear();
            CreateTable();
        }
    }
}