using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using HomeWork_9.BotLogic.Interface;
using HomeWork_9.BotLogic.DataBase;

namespace HomeWork_9.My_TG_API
{   /// <summary>
    /// Base class Represenst common messages field
    /// </summary>
    public abstract class TG_Message_Base : IMessage
    {
        #region Fields
        /// <summary>
        /// Text in message
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// message unique ID
        /// </summary>
        public long Message_id { get; }
        /// <summary>
        /// Type of message
        /// </summary>
        public IMessage.MsType MessageType { get; set; }
        /// <summary>
        /// ID of user that send message
        /// </summary>
        public string UserID { get; }
        /// <summary>
        /// User ID in Long format
        /// </summary>
        public long LongUserID { get; }
        /// <summary>
        /// True if message was send by bot
        /// </summary>
        public bool IsBot { get; }
        /// <summary>
        /// First name of user
        /// </summary>
        public string FirstName { get; }
        /// <summary>
        /// Last name of user
        /// </summary>
        public string LastName { get; }
        /// <summary>
        /// Defines what language used in message
        /// </summary>
        public string LanguageCode { get; }
        /// <summary>
        /// Date when message was send
        /// </summary>
        public string Date { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Base constructor for all messages. Parse common fields from JSON
        /// </summary>
        /// <param name="JSONmessage">JSON message to parse</param>
        protected TG_Message_Base(JToken JSONmessage)
        {
            if (JSONmessage.Contains("id")) Message_id = JSONmessage.Value<long>("id");
            if (JSONmessage.Contains("message_id")) Message_id = JSONmessage.Value<long>("message_id");

            var tempFrom = JSONmessage["from"];

            UserID = tempFrom.Value<string>("id");
            LongUserID = long.Parse(UserID);
            IsBot = tempFrom.Value<bool>("is_bot");
            FirstName = tempFrom.Value<string>("first_name");
            LastName = tempFrom.Value<string>("last_name");
            LanguageCode = tempFrom.Value<string>("language_code");
        }
        #endregion

        public abstract List<string> GetAnswer(JsonDataBase Util);

    }
}
