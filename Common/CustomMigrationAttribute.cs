namespace Common
{
    public class CustomMigrationAttribute : FluentMigrator.MigrationAttribute
    {

        public CustomMigrationAttribute(int branchId, int year, int month, int day, 
                                        int hour, int minute, string author, 
                                        FluentMigrator.TransactionBehavior transactionBehavior = FluentMigrator.TransactionBehavior.Default)
       : base(CalculateValue(branchId, year, month, day, hour, minute), transactionBehavior)
        {
            this.Author = author;
        }

        public string Author { get; private set; }

        private static long CalculateValue(int branchNumber, int year, int month, int day, 
                                           int hour, int minute)
        {
            return branchNumber * 1000000000000L + year * 100000000L + month * 1000000L + day * 10000L + hour * 100L + minute;
        }

    }
}