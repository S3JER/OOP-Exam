using System;
using System.Text.RegularExpressions;

namespace OOP_Eksamen
{
    public class User : IComparable
    {
        private string _username;
        private string _firstname;
        private string _lastname;
        private decimal _balance;

        public User(string username, string firstname, string lastname, string email, decimal balance, int id)
        {
            if (validateUsername(username))
            {
                Username = username;
            }
            Firstname = firstname;
            Lastname = lastname;
            if (validateEmail(email))
            {
                Email = email;
            }
            else
            {
                throw new EmailNotLegalException(email);
            }
            Balance = balance;
            if (id! < 1)
            {
                throw new IdException(id);
            }
            else
            {
                Id = id;
            }
        }

        public string Username {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }

        public string Firstname
        {
            get
            {
                return _firstname;
            }
            set
            {
                if(value != null)
                {
                    _firstname = value;
                }
                else
                {
                    throw new ArgumentNullException("Firstname can't be null");
                }
            }
        }

        public string Lastname {
            get
            {
                return _lastname;
            }
            set
            {
                if(value != null)
                {
                    _lastname = value;
                }
                else
                {
                    throw new ArgumentNullException("Lastname can't be null");
                }
            }
        }
        public string Email { get; }
        public decimal Balance { 
            get
            {
                return _balance;
            }
            set
            {
                _balance = value;
            }
        }
        private int Id { get;}
       
        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is User))
                return false;
            else
                return Id == ((User)obj).Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public int CompareTo(object obj)
        {
            return ((User)obj).Id > Id ? -1 : 1;
        }

        public override string ToString()
        {
            return $"{Firstname} {Lastname} ({Email})";
        }

        private bool validateUsername(string username)
        {
            if(username != null)
            {
                string usableChars = "abcdefghijklmnopqrstuvwxyz0123456789_";
                foreach (char c in username)
                {
                    if (!usableChars.Contains(c))
                    {
                        throw new UsernameNotLegalException(username);
                    }
                }
                return true;
            }
            throw new UsernameNotLegalException(username);
        }

        private bool validateEmail(string email)
        {
            Regex regex = new Regex(@"^(^[a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9]+)([\.]?)+([a-zA-Z0-9])+((([\.a-z]){2,3})+)$");
            Match matches = regex.Match(email);
            return matches.Success;
        }
    }
}
