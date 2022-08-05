using System;
using System.Text;
using System.Collections.Generic;
using HomeWork_9.BotLogic.DataBase;
using HomeWork_9.BotLogic.Interface;


namespace HomeWork_9.BotLogic.MessageLogic
{
    /// <summary>
    /// Contains methods that allow interogate with bot settings from a chat if user have administrator rights
    /// </summary>
    internal class AdminComands : IBotResponse
    {
        #region Fields
        /// <summary>
        /// Contains one or more Answers that need to be send to users
        /// </summary>
        private List<LogicAnswer> _logicAnswers;
        /// <summary>
        /// Defines with type of comand was send by user
        /// </summary>
        private string _typeOfCommand;
        /// <summary>
        /// An instanse of incoming message with admin comand
        /// </summary>
        private IMessage _adminCommandMessage;
        /// <summary>
        /// Information about suported admin comands
        /// </summary>
        private string _helpInfo = "Доступны следующие команды:" +
                                  "\nADM_HELP - вызов справки" +
                                  "\n\nADM_ADD_Reaction - добавляет реакцию на реплику" +
                                  "\nПример: ADM_ADD_Reaction$Привет$Приветствую" +
                                  "\n\nADM_ADD_Admin - добавляет администратора в бот" +
                                  "\nПример: ADM_ADD_Admin$21312312213" +
                                  "\n\nADM_SEE_Nodictionary - показывает какие новые сообщения поступили" +
                                  "\nПример: ADM_SEE_Nodictionary";
        #endregion

        #region Constructor
        /// <summary>
        /// Initialize a new instance for answer to admin comand message
        /// </summary>
        /// <param name="AdminCommandMessage">Message that contains an admin comand</param>
        public AdminComands(IMessage AdminCommandMessage) 
        {
            _adminCommandMessage = AdminCommandMessage;
            _logicAnswers = new List<LogicAnswer>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Pasre user comands with '$' separator, transform them in alowed actions with bot settings and create a response to user
        /// </summary>
        /// <param name="DataBase">Data base with List of user that have administrator rights, Dictionary of
        ///default text answers and List of text messages that have no answer</param>
        /// <returns>Result of admin operation in defaut format <see cref="LogicAnswer"/></returns>
        public List<LogicAnswer> GetReaction(JsonDataBase DataBase) 
        {    
            bool DataBaseChanged = false;
            LogicAnswer DefaultAnswer = new LogicAnswer(_adminCommandMessage.UserID);

            string[] CommandComponents = _adminCommandMessage.Text.Split('$', StringSplitOptions.RemoveEmptyEntries);
            if (CommandComponents.Length > 0) 
            {
                _typeOfCommand = CommandComponents[0];
            }
                        
            switch (_typeOfCommand) 
            {
                //Send a list of suported admin comands
                case "ADM_HELP":                        
                    DefaultAnswer.Message = _helpInfo;
                    break;
                //Add a new pair in dictionary of ansewers to text messages
                case "ADM_ADD_Reaction":
                    if (CommandComponents.Length < 3) 
                    {
                        DefaultAnswer.Message = "Неверная команда";
                        break;
                    }
                    string ToReactText = CommandComponents[1].ToLower();
                    string NewReaction = CommandComponents[2].ToLower();

                    bool InBaseAnswersContainsCheck = DataBase.AnswersDictionary.ContainsKey(ToReactText);
                    if (!InBaseAnswersContainsCheck)
                    {
                        DataBase.AnswersDictionary.Add(ToReactText, NewReaction);
                        DataBase.NewRequests.Remove(ToReactText);
                        DataBaseChanged = true;
                        DefaultAnswer.Message = "Словарь обновлен";
                        break ;
                    }
                    DefaultAnswer.Message = "Ответ на эту реплику уже существует";
                    break;
                //Add a new user with administrator rights and send him an information about awaliable comands
                case "ADM_ADD_Admin":
                    if (CommandComponents.Length < 2)
                    {
                        DefaultAnswer.Message = "Неверная команда";
                        break;
                    }
                    long NewAdminId;
                    bool IdParseCheck = long.TryParse(CommandComponents[1], out NewAdminId);

                    bool InBaseContainsAdminChek = DataBase.Admins.Contains(NewAdminId);
                    if (!InBaseContainsAdminChek && IdParseCheck)
                    {
                        DataBase.Admins.Add(NewAdminId);
                        var ToNewAdminMessage = new LogicAnswer("MS", NewAdminId.ToString(), "Вам дали права администратора бота.\nADM_HELP - вызов справки");
                        _logicAnswers.Add(ToNewAdminMessage);
                        DataBaseChanged = true;
                        DefaultAnswer.Message = "Админ добавлен";
                        break;
                    }
                    DefaultAnswer.Message = "Такой админ уже существует или неверно указан ID";
                    break;
                //Show with text messages was send to bot and don't have a dictionary answer
                case "ADM_SEE_Nodictionary":
                    StringBuilder NoDictionaryMessages = new StringBuilder();
                    int number = 1;
                    foreach(string Req in DataBase.NewRequests) 
                    {
                        NoDictionaryMessages.Append($"{number}. {Req}\n");
                        number++;
                    }
                    string NoDictionaryMessagesInOneString = NoDictionaryMessages.ToString();
                    DefaultAnswer.Message = NoDictionaryMessagesInOneString;
                    break;
                default:
                    DefaultAnswer.Message = _helpInfo;
                    break;
            }
            _logicAnswers.Add(DefaultAnswer);

            //serelized database in Json if there was any changes
            if (DataBaseChanged) 
            {
                DataBase.Serelize();
            }
            return _logicAnswers;
        }
        #endregion
    }
}
