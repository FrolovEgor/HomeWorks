using System.Collections.Generic;
using HomeWork_9.BotLogic.DataBase;

namespace HomeWork_9.BotLogic.Interface
{
    /// <summary>
    /// Defines fields that shoud be in message incoming from web side to logic side from any API.
    /// Also defines a method that used for create a API answer from logic Answer
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Type of message
        /// </summary>
        public MsType MessageType { get; set; }
        /// <summary>
        /// Text that contains in message
        /// </summary>
        public string Text { get; }
        /// <summary>
        /// Unique user id as string
        /// </summary>
        public string UserID { get; }
        /// <summary>
        /// Unique user id as number 
        /// </summary>
        public long LongUserID { get; }
        /// <summary>
        /// Flag for indicate that message does/doesn't send from bot
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
        public enum MsType
        {
            Text = 0x0,
            adminCMD = 0x2,
            userCMD = 0x3,
            Stiker = 0x4,
            File = 0x5,
            Audio = 0x6,
            Photo = 0x7
        }
        /// <summary>
        /// Trasform bot answer from standart bot <see cref="LogicAnswer"/> to <see cref="string"/> format that will be send to API
        /// </summary>
        /// <param name="Util">Data base with information that can be used for answer</param>
        /// <returns>Answer to API with API defined comands that can be send to URL</returns>
        public List<string> GetAnswer(JsonDataBase Util);
    }
}
