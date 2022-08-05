using System;
using System.IO;
using System.Collections.Generic;
using HomeWork_9.BotLogic.DataBase;
using HomeWork_9.BotLogic.Interface;
using HomeWork_9.BotLogic.ExchangeRate;
    
namespace HomeWork_9.BotLogic.MessageLogic
{
    /// <summary>
    /// Contains methods that allow interogate with bot by user comands starsts with '/'
    /// </summary>
    internal class UserComands : IBotResponse
    {
        #region Fields
        /// <summary>
        /// Contains one or more Answers that need to be send to users
        /// </summary>
        private List<LogicAnswer> _logicAnswers;
        /// <summary>
        /// Defines with type of comand was send by user
        /// </summary>
        private string _typeOfComand;
        /// <summary>
        /// An instanse of incoming message with user comand
        /// </summary>
        private IMessage _userCommandMessage;
        /// <summary>
        /// Default answer in format <see cref="LogicAnswer"/>
        /// </summary>
        private LogicAnswer _defaultAnswer;
        /// <summary>
        /// Information about suported user comands
        /// </summary>
        private string _helpInfo = "Доступны следующие команды:" +
                                  "\n/course - показывает текущие значения курса ЦБ и на бирже" +
                                  "\n/myfiles - показывает сохраненные на сервере файлы. Что-бы скачать файл нажмите на соответствующую кнопку" +
                                  "\n\nТак-же можно присылать мне любую фигню, я похраню ее у себя";
        #endregion

        #region Constructor
        /// <summary>
        /// Initialize a new instance for answer to user comand message
        /// </summary>
        /// <param name="UserComandMessage">Message that contains an user comand</param>
        public UserComands(IMessage UserComandMessage) 
        {
            _userCommandMessage = UserComandMessage;
            _logicAnswers = new List<LogicAnswer>();
            _defaultAnswer = new LogicAnswer(_userCommandMessage.UserID) {Message = _helpInfo };
        }
        #endregion

        #region Methods
        /// <summary>
        /// Create filds for bot InlineButtons with information about his fieles and comands to downdload them
        /// </summary>
        private void GetUserFilesList() 
        {
            Dictionary<string, string> ResultButtons = new Dictionary<string, string>();
            DirectoryInfo UserFileDirectory = new DirectoryInfo($"./{_userCommandMessage.UserID}/");

            FileInfo[] FilesInDirectory = null;
            if (UserFileDirectory.Exists)
            {
                FilesInDirectory = UserFileDirectory.GetFiles();
            }

            if (FilesInDirectory != null)
            {
                foreach (var e in FilesInDirectory)
                {
                    string DownloadComand = $"fd${e.Name}";
                    ResultButtons.Add(e.Name, DownloadComand);
                }
                _defaultAnswer.Message = "Ваши файлы:";
                _defaultAnswer.AnswerType = "Buttons";
                _defaultAnswer.Buttons = ResultButtons;
                return;
            }
            _defaultAnswer.Message = "Вы пока не загружали файлы, или вашу папку удалил злобный админ(";
        }
        /// <summary>
        /// Create a Request for API side to send selected file from server in bot
        /// </summary>
        /// <param name="FileId">Name of upload file</param>
        private void MakeDownloadFileRequest(string FileId) 
        {
            if (FileId == null)
            {
                _defaultAnswer.Message = "Что то не так";
                return;
            }

            _defaultAnswer.Message = "Вот ваш файл";
            LogicAnswer UploadFileRequest = new LogicAnswer(_userCommandMessage.UserID)
            {
                AnswerType = "SendFile",
                FileName = FileId,
                FileRoute = @$"{_userCommandMessage.UserID}/{FileId}"
            };

            _logicAnswers.Add(UploadFileRequest);
        }
        /// <summary>
        /// Pasre user comands with '/'and '$' separators, transform them in alowed actions and create a response to user
        /// </summary>
        /// <param name="DataBase">Data base with information that can be used for answer</param>
        /// <returns>One or more Answer's from Bot logic side</returns>
        public List<LogicAnswer> GetReaction(JsonDataBase DataBase)
        {
            char[] SplitOptions = { '/', '$' };
            string[] ComandComponents = _userCommandMessage.Text.Split(SplitOptions, StringSplitOptions.RemoveEmptyEntries);
            _typeOfComand = ComandComponents[0].ToLower();

            string FileId = null;

            if (ComandComponents.Length == 2)
            {
                   FileId = ComandComponents[1];
            }

            switch (_typeOfComand)
            {
                case ("start"):
                    _defaultAnswer.Message = $"Привет, {_userCommandMessage.FirstName} {_userCommandMessage.LastName}!\n" +
                                            $"Тебе доступны следующие команды:\n\n{_helpInfo}";
                    break;
                case ("course"):
                    var CourseParser = new ExchangeRateParser();
                    string CourseParserResult = CourseParser.Update();
                    _defaultAnswer.Message = CourseParserResult;
                    break;
                case ("myfiles"):
                    GetUserFilesList();
                    break;
                case ("fd"):
                    MakeDownloadFileRequest(FileId);
                    break;
                default:
                    break;
                    _logicAnswers.Add(_defaultAnswer);
            }
            _logicAnswers.Add(_defaultAnswer);
            return _logicAnswers;
        }
        #endregion
    }
}
