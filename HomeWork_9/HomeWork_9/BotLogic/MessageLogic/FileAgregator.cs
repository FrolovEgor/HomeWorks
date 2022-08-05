using System.IO;
using System.Collections.Generic;
using HomeWork_9.BotLogic.DataBase;
using HomeWork_9.BotLogic.Interface;


namespace HomeWork_9.BotLogic.MessageLogic

{
    /// <summary>
    /// Contains methods for operate with path of downloading file and chek files extantion
    /// </summary>
    internal class FileAgregator : IBotResponse
    {
        #region Fields
        /// <summary>
        /// Instanse of message that contains income file
        /// </summary>
        private My_TG_API.TG_File _fileMessage;
        /// <summary>
        /// Contains one or more Answers that need to be send to users
        /// </summary>
        private List<LogicAnswer> _logicAnswers;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialize a new instance for answer to file message
        /// </summary>
        /// <param name="FileMessage">Message that contains a file</param>
        public FileAgregator(My_TG_API.TG_File FileMessage)
        {
            _logicAnswers = new List<LogicAnswer>();
            _fileMessage = FileMessage;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Check extention of user file, create directoty for each user and set path for downloading in instance of income file message class
        /// </summary>
        /// <param name="DataBase">Data base with information that can be used for answer</param>
        /// <returns>Result of user file processing in defaut format <see cref="LogicAnswer"/></returns>
        public List<LogicAnswer> GetReaction(JsonDataBase DataBase) 
        {
            LogicAnswer DefaultAnswer = new LogicAnswer(_fileMessage.UserID);

            if (_fileMessage.Extention == ".exe") 
            {
                DefaultAnswer.Message = "Зачем мне \"exe\"? Вирус хочешь мне подкинуть? Не буду это сохранять";
                _logicAnswers.Add(DefaultAnswer);
                return _logicAnswers;
            }

            if (!Directory.Exists($"./{_fileMessage.UserID}")) 
            {
                Directory.CreateDirectory($"./{_fileMessage.UserID}");
            } 

            DefaultAnswer.Message = $"{_fileMessage.file_name} сохранен";
            DefaultAnswer.FileRoute = $"./{_fileMessage.UserID}/{_fileMessage.file_name}";

            _logicAnswers.Add(DefaultAnswer);
            return _logicAnswers;
        }
        #endregion
    }
}
