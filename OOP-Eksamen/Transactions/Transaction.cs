using System;

namespace OOP_Eksamen
{
    public abstract class Transaction
    {
        public Transaction(int id, User user, decimal amount, string date)
        {
            Id = id;
            User = user;
            Date = date;
            Amount = amount;
        }

        public int Id { get; set; }
        public User User { get; set; }
        public string Date { get; }
        public decimal Amount { get; set; }

        public override string ToString()
        {
            return $"{Id};{User.Username};{Amount};{Date}";
        }

        public abstract void Execute();
    }
}
