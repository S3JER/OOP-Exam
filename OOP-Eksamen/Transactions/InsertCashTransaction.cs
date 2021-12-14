using System;
using System.Collections.Generic;

namespace OOP_Eksamen
{
    public class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(int id, User user, decimal amount, string date) : base(id, user, amount, date)
        {
        }
        public override string ToString()
        {
            return $"{Id};{Date};{User.Username};{Amount}";
        }
        public override void Execute()
        {
            User.Balance += Amount;
        }
    }
}
