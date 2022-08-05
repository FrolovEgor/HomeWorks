using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HomeWork_9.BotLogic.MessageLogic;
using HomeWork_9.BotLogic.Interface;
using HomeWork_9.BotLogic.DataBase;

namespace HomeWork_9.My_TG_API
{
    /// <summary>
    ///  Represenst an object of text message, that can contains user or admin comands
    /// </summary>
    internal class TGText : TG_Message_Base
    {
        #region Fields
        /// <summary>
        /// An instance of TG_client that communicate with telegram 
        /// </summary>
        private TG_Client _client;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialize a new instance of file message
        /// </summary>
        /// <param name="Message">Initialize a new instance of file message</param>
        /// <param name="key">True if text message, false if text need to be read from inlinekeyboard</param>
        /// <param name="Client">An instance of TG_client that communicate with telegram</param>
        public TGText(JToken Message,bool key, TG_Client Client) : base(Message)
        {
            if (key)
            {
                Text = Message.Value<string>("text");
            }
            else 
            {
                Text = Message.Value<string>("data");
            }
            MessageType = IMessage.MsType.Text;

            this._client = Client;
        }
        #endregion

        /// <summary>
        /// Create a suitable for type of text message comand logic object and transform it's answer to API request
        /// </summary>
        /// <param name="DataBase">Data base with List of user that have administrator rights, Dictionary of
        ///default text answers and List of text messages that have no answer</param>
        /// <returns>Result of admin operation in defaut format</param>
        /// <returns>An API request with and answer</returns>
        public override List<string> GetAnswer(JsonDataBase DataBase)
        {

            if (Text.First() == '/')
            {
                MessageType = IMessage.MsType.userCMD;
            }

            if (DataBase.Admins.Contains(LongUserID) && Text.Contains("ADM_"))
            {
                MessageType = IMessage.MsType.adminCMD;
            }

            var LogicHandler = BotTextLogicSelector.SelectTextMessageLogic(this);    //returns a suitable BotLogic class 

            var AnswersFromLogic = LogicHandler.GetReaction(DataBase);                        //Send text to logic and get answe in defualt format
            List<string> APIAnswers = new List<string>();
            foreach (var Answer in AnswersFromLogic)
            {
                if (Answer.AnswerType == "MS")                  //Create a text message 
                {
                    APIAnswers.Add($"sendMessage?chat_id={Answer.UserID}&text={Answer.Message}");
                }
                if (Answer.AnswerType == "Buttons")             //Create InlineButtons message
                {
                    List<InlineKeyboardButton> ButtonsTiKeyboard = new List<InlineKeyboardButton>();

                    foreach (var Button in Answer.Buttons)
                    {
                        var TempButton = new InlineKeyboardButton(Button.Key, Button.Value);
                        ButtonsTiKeyboard.Add(TempButton);
                    }
                    var KeyBoardFull = new InlineKeyboardMarkup(2, ButtonsTiKeyboard.ToArray());
                    var SerelizedKeyBoard = JsonConvert.SerializeObject(KeyBoardFull);
                    APIAnswers.Add($"sendMessage?chat_id={Answer.UserID}&text={Answer.Message}&reply_markup={SerelizedKeyBoard}");
                }
                if (Answer.AnswerType == "SendFile")            //Send file to bot
                {
                    _client.UploadFileAsync(Answer.UserID, Answer.FileRoute, Answer.FileName);
                }
            }
            return APIAnswers;
        }
    }
}
