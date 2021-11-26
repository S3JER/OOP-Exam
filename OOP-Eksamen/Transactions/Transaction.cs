using System;

namespace OOP_Eksamen
{
    abstract class Transaction
    {
        public Transaction(int id, User user, decimal amount)
        {
            Id = id;
            User = user;
            Date = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            Amount = amount;
        }

        public int Id { get; set; }
        public User User { get; set; }
        public string Date;
        public decimal Amount { get; set; }

        public override string ToString()
        {
            return $"{Id} {User.Username} {Amount} {Date}";
        }

        public abstract void Execute();
    }
}
