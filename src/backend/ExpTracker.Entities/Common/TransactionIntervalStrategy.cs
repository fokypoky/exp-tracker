namespace ExpTracker.Entities.Common
{
    public enum TransactionIntervalStrategy
    {
        /// <summary>
        /// Транзакция выполняется каждое первое число месяца
        /// </summary>
        FirstDayOfMonth,

        /// <summary>
        /// Транзакция выполняется каждое последнее число месяца
        /// </summary>
        LastDayOfMonth,

        /// <summary>
        /// Транзакция выполняется в указанное число месяца
        /// </summary>
        SpecifiedDayOfMonth
    }
}
