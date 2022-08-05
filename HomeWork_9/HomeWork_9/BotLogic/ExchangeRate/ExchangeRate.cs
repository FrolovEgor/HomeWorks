
namespace HomeWork_9.BotLogic.ExchangeRate
{
    /// <summary>
    /// Contains a exchange rate information of dollar and euro for one date 
    /// </summary>
    class ExchangeRate
    {
        #region fields
        /// <summary>
        /// Date of exchange rate
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// Dollar exchange rate
        /// </summary>
        public float Dollar { get; set; }
        /// <summary>
        /// Euro exchange rate
        /// </summary>
        public float Euro { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialize a new instanse of <see cref="HomeWork_9.BotLogic.ExchangeRate.ExchangeRate"/> for specified date
        /// </summary>
        /// <param name="date">Date of exchange rate</param>
        public ExchangeRate(string date) 
        { 
            Date = date;
            Dollar = 0;
            Euro = 0;
        }
        #endregion



    }
}
