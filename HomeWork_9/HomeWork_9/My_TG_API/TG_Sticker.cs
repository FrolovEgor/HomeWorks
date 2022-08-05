using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;
using HomeWork_9.BotLogic.Interface;
using HomeWork_9.BotLogic.DataBase;

namespace HomeWork_9.My_TG_API
{   /// <summary>
    /// Represenst an object of file message
    /// </summary>
    internal class TG_Sticker: TG_Message_Base
    {   /// <summary>
        /// Initialize a new instance of stiker message
        /// </summary>
        /// <param name="message">Json message that represents a stiker</param>
        public TG_Sticker(JToken message) : base(message) 
        {
            MessageType = IMessage.MsType.Stiker;
        }
        /// <summary>
        /// Create a text answer for a sended stiker
        /// </summary>
        /// <param name="DataBase">Data base with information that can be used for answer</param>
        /// <returns>An API request with text message</returns>
        public override List<string> GetAnswer(JsonDataBase DataBase)
        {
            List<string> Answers = new List<string>();
            Answers.Add($"sendMessage?chat_id={UserID}&text=Я пока не придумал, что ответить на стикеры..." );
            return Answers;
        }

    }
}
