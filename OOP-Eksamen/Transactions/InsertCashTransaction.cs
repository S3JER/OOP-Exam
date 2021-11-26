using System;
using System.Collections.Generic;

namespace OOP_Eksamen
{
    class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(int id, User user, decimal amount) : base(id, user, amount)
        {
        }
        public override string ToString()
        {
            return $"{Amount} {User.Username} {Date} {Id}";
        }
        public override void Execute()
        {
            User.Balance += Amount;
        }
    }
}
