using System;
using System.Text;
using System.Net;
using System.Collections.Generic;
using HtmlAgilityPack;


namespace HomeWork_9.BotLogic.ExchangeRate
{
    /// <summary>
    /// Used for HTML parse exhange rate of dollad and euro from <see href="https://www.cbr.ru/"/> and from <see href="https://ru.investing.com/"/>
    /// </summary>
    internal class ExchangeRateParser
    {
        #region fields
        /// <summary>
        /// Collection of <see cref="ExchangeRate"/> that contains parsed exchange rates from cbr.ru. 
        /// First element is today rate, second tommorow rate
        /// </summary>
        private List<ExchangeRate> NowExchangeRate;
        /// <summary>
        /// Contains exchange rate of current treading from investing.com in string foemat
        /// </summary>
        private string TradersRate;
        #endregion

        #region Constructor
        public ExchangeRateParser()
        {
            NowExchangeRate = new List<ExchangeRate>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Update values of exchange rates
        /// </summary>
        /// <returns>upadated values with header</returns>
        public string Update()
        {
            DownloadCBR();
            DownloadTreaders();

            string UpdaredRates = "Курс ЦБ:";
            foreach (var Rate in NowExchangeRate)
            {
                UpdaredRates += $"\nНа {Rate.Date} $ - {Rate.Dollar} € - {Rate.Euro}";
            }
            UpdaredRates += $"\n\nНа бирже:\n{TradersRate}\n\nОбновленно:{DateTime.Now}";
            return UpdaredRates;
        }
        /// <summary>
        /// Parse exchange rates from <see href="https://www.cbr.ru/"/>
        /// </summary>
        private void DownloadCBR()
        {
            string CentralBankMainPage;
            NowExchangeRate.Clear();
            using (WebClient HtmlParseWebClient = new WebClient()) 
            {
                HtmlParseWebClient.Encoding = Encoding.UTF8;
                CentralBankMainPage = HtmlParseWebClient.DownloadString(@"https://www.cbr.ru/");
            }

            var MainPageHTML = new HtmlDocument();
            MainPageHTML.LoadHtml(CentralBankMainPage);

            List<float> DollarExchange = new List<float>();
            List<float> EuroExchange = new List<float>();

            var ParsedNodes = MainPageHTML.DocumentNode.SelectNodes("//div[@class='main-indicator_rate']");
            foreach (var node in ParsedNodes)
            {
                if (node.InnerHtml.Contains("USD"))
                {
                    DollarExchange = ParseEchangeNoteOfCentralBank(node);
                }
                if (node.InnerHtml.Contains("EUR"))
                {
                    EuroExchange = ParseEchangeNoteOfCentralBank(node);
                }
            }

            var DatesOfCourse = MainPageHTML.DocumentNode.SelectNodes("//div[@class='col-md-2 col-xs-7 _right']");

            foreach (HtmlNode DateNode in DatesOfCourse)
            {
                NowExchangeRate.Add(new ExchangeRate(DateNode.InnerText));
            }

            for (int i = 0; i < NowExchangeRate.Count; i++)
            {
                NowExchangeRate[i].Dollar = DollarExchange[i];
                NowExchangeRate[i].Euro = EuroExchange[i];
            }
        }
        /// <summary>
        /// Parse values of one currency from CBR Html note
        /// </summary>
        /// <param name="node">note to parse</param>
        /// <returns>Values of currency</returns>
        private List<float> ParseEchangeNoteOfCentralBank(HtmlNode node)
        {
            List<float> OneСurrencyExchanges = new List<float>();

            var ExchangeRates = node.SelectNodes("div[@class='col-md-2 col-xs-9 _right mono-num']");
            foreach (HtmlNode Values in ExchangeRates)
            {
                OneСurrencyExchanges.Add(float.Parse(Values.InnerText.Remove(7)));
            }
            return OneСurrencyExchanges;
        }
        /// <summary>
        /// Parse values of dollar currency from investing.com
        /// </summary>
        private void DownloadTreaders()
        {
            string Traders;
            using (WebClient TreadersParseClient = new WebClient()) 
            {
                TreadersParseClient.Encoding = Encoding.UTF8;
                Traders = TreadersParseClient.DownloadString(@"https://ru.investing.com/currencies/usd-rub");
            }

            var CurrencyContainsPage = new HtmlDocument();
            CurrencyContainsPage.LoadHtml(Traders);

            var Node = CurrencyContainsPage.DocumentNode.SelectNodes("//span[@class='text-2xl']");
            TradersRate = Node[0].InnerText;
        }
        #endregion
    }
}



