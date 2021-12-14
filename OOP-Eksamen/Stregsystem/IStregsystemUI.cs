using System.Collections.Generic;

namespace OOP_Eksamen
{
    internal interface IStregsystemUI
    {
        void DisplayUserNotFound(string username); 
        void DisplayProductNotFound(string product); 
        void DisplayUserInfo(User user); 
        void DisplayTooManyArgumentsError(string command); 
        void DisplayAdminCommandNotFoundMessage(string adminCommand); 
        void DisplayUserBuysProduct(BuyTransaction transaction); 
        void DisplayUserBuysProduct(int count, List<BuyTransaction> transaction); 
        void Close(); 
        void DisplayInsufficientCash(User user, Product product); 
        void DisplayGeneralError(string errorString); 
        void Start();
        void DisplayBalanceWarning(User user, decimal balance);
        void DisplayProducts();
        event StregsystemEvent CommandEvent;
        public delegate void StregsystemEvent(string inputstring);
    }
}