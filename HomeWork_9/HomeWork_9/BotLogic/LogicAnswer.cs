using System.Collections.Generic;

namespace HomeWork_9.BotLogic
{
    /// <summary>
    /// Regulated answer from bot to API side
    /// </summary>
    public class LogicAnswer
    {
        #region Fields
        /// <summary>
        /// Type of messege that bot will send to user
        /// MS - text message
        /// Buttons - inline buttons (if supported). Field <see cref="Buttons"/> must be filled by data
        /// SendFile - file need to send in bot. Fields <see cref="FileName"/> and <see cref="FileRoute"/> must be filled by data
        /// </summary>
        public string AnswerType;
        /// <summary>
        /// Id of user to send message
        /// </summary>
        public string UserID;
        /// <summary>
        /// Text message to send
        /// </summary>
        public string Message;
        /// <summary>
        /// Data to create inline buttons (if AnswerType is "Buttons"). Key contains text on button. Value - bot comand
        /// </summary>
        public Dictionary<string,string> Buttons;
        /// <summary>
        /// Name of sending file (if AnswerType is "SendFile")
        /// </summary>
        public string FileName;
        /// <summary>
        /// Route to sending file (if AnswerType is "SendFile")
        /// </summary>
        public string FileRoute;
        #endregion

        #region Constructors
        /// <summary>
        /// Initialize a new instance of answer message
        /// </summary>
        /// <param name="answerType">Type of messege that bot will send to user
        /// MS - text message
        /// Buttons - inline buttons (if supported). Field <see cref="Buttons"/> must be filled by data
        /// SendFile - file need to send in bot. Fields <see cref="FileName"/> and <see cref="FileRoute"/> must be filled by data</param>
        /// <param name="userID">Id of user to send message</param>
        /// <param name="message">Text message to send</param>
        public LogicAnswer(string answerType, string userID, string message) 
        {
            AnswerType = answerType;
            UserID = userID;
            Message = message;
        }
        /// <summary>
        /// Initialize a new instance of answer message
        /// </summary>
        /// <param name="userID">Id of user to send message</param>
        public LogicAnswer(string userID) : this("MS", userID, "")
        { }
        #endregion
    }
}
