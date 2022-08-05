using System.Collections.Generic;
using HomeWork_9.BotLogic.DataBase;
using HomeWork_9.BotLogic.Interface;

namespace HomeWork_9.BotLogic.MessageLogic
{
    /// <summary>
    /// Contains methods for answer on regulag text messages of user using pre-added reaction in DataBase Dictionary
    /// </summary>
    internal class DictionaryTalk : IBotResponse
    {
        #region Fields
        /// <summary>
        /// Text answer to a user message get from dictionacy
        /// </summary>
        private string _dictionaryAnswer;
        /// <summary>
        /// Contains one or more Answers that need to be send to users
        /// </summary>
        private List<LogicAnswer> _logicAnswers;
        /// <summary>
        /// An instanse of incoming message with regular text
        /// </summary>
        private IMessage _textMessage;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialize a new instance for answer to text message
        /// </summary>
        /// <param name="TextMessage">Message that contains regular text</param>
        public DictionaryTalk(IMessage TextMessage)
        {
            _textMessage = TextMessage;
            _logicAnswers = new List<LogicAnswer>();
        }
        #endregion

        /// <summary>
        /// Get pre-added answer to user text message from Dictionary
        /// </summary>
        /// <param name="DataBase">Data base with Dictionary contains pre-added answers on text messages</param>
        /// <returns>Text answer in default format <see cref="LogicAnswer"/></returns>
        public List<LogicAnswer> GetReaction(JsonDataBase DataBase)
        {
            LogicAnswer DefaultAnswer = new LogicAnswer(_textMessage.UserID);
            string UserText = _textMessage.Text.ToLower();
            bool DataBaseChanged = false;
            bool DataBaseContainsAnserChek = DataBase.AnswersDictionary.TryGetValue(UserText, out _dictionaryAnswer);
            if (!DataBaseContainsAnserChek)
            {
                if (!DataBase.NewRequests.Contains(UserText)) 
                {
                    DataBase.NewRequests.Add(UserText);
                    DataBaseChanged = true;
                } 
                DefaultAnswer.Message = "Не знаю что Вам ответить, но подумаю";
            }
            else DefaultAnswer.Message = _dictionaryAnswer;

            _logicAnswers.Add(DefaultAnswer);
            
            if (DataBaseChanged) 
            {
                DataBase.Serelize();
            }
            
            return _logicAnswers;
        }
    }
}
