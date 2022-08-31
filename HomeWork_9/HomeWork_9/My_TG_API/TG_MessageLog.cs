using HomeWork_9.BotLogic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_9.My_TG_API
{
    public class TG_MessageLog
    {
        public string Text { get; set; }
        /// <summary>
        /// Type of message
        /// </summary>
        public IMessage.MsType MessageType { get; set; }
        /// <summary>
        /// Unique user id as number 
        /// </summary>
        public long LongUserID { get; }
        /// <summary>
        /// First name of user
        /// </summary>
        public string FirstName { get; }
        /// <summary>
        /// Last name of user
        /// </summary>
        public string LastName { get; }

        public TG_MessageLog(IMessage LoggingMessage) 
        {
            if (LoggingMessage.Text != null) Text = LoggingMessage.Text;
            MessageType = LoggingMessage.MessageType;
            LongUserID = LoggingMessage.LongUserID;
            FirstName = LoggingMessage.FirstName;
            LastName = LoggingMessage.LastName;
       
        }
    }
}
