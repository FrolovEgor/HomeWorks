﻿//#define DEBUG
#undef DEBUG

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json.Linq;
using HomeWork_9.BotLogic.Interface;
using HomeWork_9.BotLogic.DataBase;
using System.Threading.Tasks;
//using Telegram.Bot.Types;

namespace HomeWork_9.My_TG_API
{
    /// <summary>
    /// Represents a object that connents to a Telegram API and make communicates with the Telegram server
    /// </summary>
    public class TG_Client
    {
        #region Fields
        /// <summary>
        /// URL of Teleram API with bot token for sending messages
        /// </summary>
        private string URL;
        /// <summary>
        /// ID of last recived array of messages used to sincronize bot and client
        /// </summary>
        private int UpdateID;
        /// <summary>
        /// Object that contains constant Data for Bot lonic and saving it in JSON format on drive
        /// </summary>
        private JsonDataBase ClientDataBase;
        /// <summary>
        /// Web client that connected to Bot
        /// </summary>
        private WebClient TGClient;
        /// <summary>
        /// URL of Teleram API with bot token for download files from telegram
        /// </summary>
        private string FileURL;

        private WPFSyncronize _WPFData;

        public ObservableCollection<TG_MessageLog> LoggingMessages;
        
        
        #endregion

        /// <summary>
        /// Initialize a new instance of Telegram Client for one Bot
        /// </summary>
        /// <param name="Token">Token of telegram Bot</param>
        public TG_Client(string Token) 
        {
            URL = $"https://api.telegram.org/bot{Token}/";
            FileURL = $"https://api.telegram.org/file/bot{Token}/";
            UpdateID = 0;
            ClientDataBase = new JsonDataBase();
            TGClient = new WebClient();
            _WPFData = new WPFSyncronize();
            LoggingMessages = new ObservableCollection<TG_MessageLog>();
        }
        #region Methods
        /// <summary>
        /// Starting bot. Deserelizing DataBase of bot paramets and start async infinite loop of dialog with API
        /// </summary>
        public async void Start()
        {
            //Read DataBase from Json's on drive
            ClientDataBase.Deserelize();

            await Task.Run(() => MainLoop());        
        }
        /// <summary>
        /// Infinite loop of getting updates and answer to them in "Auto" Mode and send messages from UI in manual
        /// </summary>
        private void MainLoop() 
        {
            while (true)
            {
                var updates = TGClient.DownloadString($"{URL}GetUpdates?offset={UpdateID}");

                var MessagesUpdate = JObject.Parse(updates);
                var NewMessages = MessagesUpdate["result"].ToArray();
                foreach (var message in NewMessages)
                {
                    UpdateID = int.Parse(message["update_id"].ToString()) + 1;
                    bool MessageContainsFlag = message.ToString().Contains("\"message\""); //Filter system comands of bot (like "stop")
                    if (!MessageContainsFlag) continue;

                    var ParsedMessage = GetTypeOfMessage(message);
                    List<string> AnswersToMessage = new List<string>();
                    if (ParsedMessage != null)
                    {
                        LoggingMessages.Add(new TG_MessageLog(ParsedMessage));
                        AnswersToMessage = ParsedMessage.GetAnswer(ClientDataBase);
                    }
#if DEBUG
                    Console.WriteLine(message);
#endif
                    //Used for safe use of data that can be changed from UI
                    //Cheking Mode of bot and send internal logic genegated messages if mode is not Manual
                    lock (_WPFData) 
                    { 
                        if (!_WPFData.ManualMode) 
                        {
                            foreach (string Answer in AnswersToMessage)
                            {
                                string Asns = TGClient.DownloadString($"{URL}{Answer}");
#if DEBUG
                                Console.WriteLine(Asns);
#endif
                            }
                        }

                    }

                    Thread.Sleep(1000);
                }

                //Used for safe use of data that can be changed from UI
                //Cheking Mode of bot and send manual messages from UI if mode is Manual and there are messages to send
                lock (_WPFData) 
                {
                    if (_WPFData.ManualMode && _WPFData.IsManualAnswer)
                    {
                        string Asns = TGClient.DownloadString($"{URL}sendMessage?chat_id={_WPFData.ID}&text={_WPFData.Message}");
                        _WPFData.IsManualAnswer = false;
                    }
                }
            }
        }
        /// <summary>
        /// Changing mode bot mode on manual
        /// </summary>
        /// <param name="ManualOn">True to switch on "Manual" mode, False to switch off
        public void TurnOnManualMode(bool ManualOn)
        {
            lock (_WPFData)
            {
                _WPFData.ManualMode = ManualOn;
            }
        }
        /// <summary>
        /// Sending Manual messages from Bot to selected user
        /// </summary>
        /// <param name="UserID">ID of user who you want send message</param>
        /// <param name="Message">Text of sending message</param>
        public void SendManualMessage(long UserID, string Message) 
        {
            lock (_WPFData)
            {
                _WPFData.IsManualAnswer = true;

                if (_WPFData.ManualMode)
                {
                    _WPFData.ID = UserID.ToString();
                    _WPFData.Message = Message;
                }
            }
        }
        /// <summary>
        /// Find in message JSON unique keys and initialize an object that represents a message of it's type
        /// </summary>
        /// <param name="JsonMessage">JSON message from bot</param>
        /// <returns>Object that represents a message of it's type</returns>
        private IMessage GetTypeOfMessage(JToken JsonMessage)
        {
            var CurrentMessage = JsonMessage["callback_query"];
            CurrentMessage = CurrentMessage == null ? JsonMessage["message"] : CurrentMessage;

            Console.WriteLine(JsonMessage);

            if (CurrentMessage["text"] != null) return new TGText(CurrentMessage, true, this);
            if (CurrentMessage["chat_instance"] != null) return new TGText(CurrentMessage, false, this);
            if (CurrentMessage["document"] != null) return new TG_File(CurrentMessage, this, 0);
            if (CurrentMessage["photo"] != null) return new TG_File(CurrentMessage, this, 2);
            if (CurrentMessage["voice"] != null) return new TG_File(CurrentMessage, this, 1);
            if (CurrentMessage["sticker"] != null) return new TG_Sticker(CurrentMessage);

            return null;
        }
        /// <summary>
        /// Sending request to bot for preparing a required file to download
        /// </summary>
        /// <param name="file_id">ID of downloadable file</param>
        /// <returns>Link that sould be added to FileURL for download file</returns>
        internal string GetFileDownloadPath(string file_id)
        {

            string FileDowdloadRequest = $"getFile?file_id={file_id}";
            return TGClient.DownloadString($"{URL}{FileDowdloadRequest}");

        }
        /// <summary>
        /// Download a file from telegram server to local server
        /// </summary>
        /// <param name="Link">Link to telegram File</param>
        /// <param name="Path">Directory where file need to be saved</param>
        internal void DownloadFile(string Link,string Path) 
        {
            TGClient.DownloadFile(FileURL + Link, Path);
        }
        /// <summary>
        /// Upload files from drive in telegram bot using http POST
        /// </summary>
        /// <param name="UserID">ID of user to send a file</param>
        /// <param name="fileRoute">Path to sending file</param>
        /// <param name="fileName">Name of sending file</param>
        internal async void UploadFileAsync(string UserID, string fileRoute, string fileName)
        { 
            HttpClient client = new HttpClient();
            string APIcomand = "sendDocument?chat_id=";

            using (var requestContent = new MultipartFormDataContent())
            {
                using (var fileStream = File.OpenRead(fileRoute))
                {
                    var contentDisposition = $@"form-data; name=""document""; filename=""{EncodeUtf8(fileName)}""";
                    var mediaPartContent = new StreamContent(fileStream)
                    {
                        Headers =
                        {
                            {"Content-Type", "application/octet-stream"},
                            {"Content-Disposition", contentDisposition}
                        }
                    };

                    requestContent.Add(mediaPartContent, "document", fileName);

                    await client.PostAsync($"{URL}{APIcomand}{UserID}&document=", requestContent);
                }
            }
        }

        /// <summary>
        /// Stolen from Telegram.Bot library. Doesn't POST ContentDisposition without it in strange symbols
        /// </summary>
        /// <param name="value">String that need to transform</param>
        /// <returns>String where all character replased by characters in Unicode from UTF-8 byte numbers of characters</returns>
        internal static string EncodeUtf8(string value) => new(Encoding.UTF8.GetBytes(value).Select(c => Convert.ToChar(c)).ToArray());
        #endregion
    }
}
