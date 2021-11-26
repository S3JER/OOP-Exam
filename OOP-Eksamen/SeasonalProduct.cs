namespace OOP_Eksamen
{
    class SeasonalProduct : Product
    {
        public SeasonalProduct(int id, string name, decimal price, bool active, bool canBeBoughtOnCredit, string seasonStartDate, string seasonEndDate) : base(id, name, price, active, canBeBoughtOnCredit)
        {
            SeasonStartDate = seasonStartDate;
            SeasonEndDate = seasonEndDate;
        }

        public string SeasonStartDate { get; set; }
        public string SeasonEndDate { get; set; }
    }
}
