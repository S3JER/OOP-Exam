using System;

namespace OOP_Eksamen
{
    public class User : IComparable
    {
        public User(string username, string firstname, string lastname, string email, decimal balance, int id)
        {
            Username = username;
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Balance = balance;
            Id = id;
        }

        public string Username { get; }
        public string Firstname { get; }
        public string Lastname { get; }
        public string Email { get; }
        public decimal Balance { get; set; }
        private int Id { get;}

        public int CompareTo(object obj)
        {
            return ((User)obj).Id > Id ? -1 : 1;
        }
        // Klassen skal også implementere en fornuftig Equals-metode samt GetHashCode. MANGLER
        public override string ToString()
        {
            return $"{Firstname} {Lastname} ({Email})";
        }
    }
}
