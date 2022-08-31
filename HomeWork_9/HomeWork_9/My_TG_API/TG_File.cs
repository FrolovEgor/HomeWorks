using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using HomeWork_9.BotLogic.MessageLogic;
using HomeWork_9.BotLogic.DataBase;
using HomeWork_9.BotLogic.Interface;

namespace HomeWork_9.My_TG_API
{
    /// <summary>
    /// Represenst an object of file message
    /// </summary>
    internal class TG_File : TG_Message_Base
    {
        #region Fields
        /// <summary>
        /// An instance of TG_client that communicate with telegram 
        /// </summary>
        private TG_Client _client { get; }
        /// <summary>
        /// Name of file
        /// </summary>
        public string file_name { get; }
        /// <summary>
        /// File ID
        /// </summary>
        public string file_id { get; }
        /// <summary>
        /// Unique file ID. Used for get download link
        /// </summary>
        public string file_unique_id { get; }
        /// <summary>
        /// Size of file
        /// </summary>
        public string file_size { get; }
        /// <summary>
        /// File extantion
        /// </summary>
        public string Extention { get; }
        /// <summary>
        /// Link to download from telegram 
        /// </summary>
        public string FileDonwnloadPath { get; private set; }
        /// <summary>
        /// Initialize a new instance of file message
        /// </summary>
        /// <param name="message">Json message that represents a file</param>
        /// <param name="Client">An instance of TG_client that communicate with telegram</param>
        /// <param name="TypeOfFile">Type of send file: 0 - document, 1 - voice, 2 - photo as file</param>
        #endregion

        #region Constructor
        public TG_File(JToken message, TG_Client Client,int TypeOfFile) : base(message)
        {
            JToken DocumentContent = null;

            switch (TypeOfFile) 
            {
                //get content from diferent keys proceeding from file type
                case 0:
                    MessageType = IMessage.MsType.File;
                    DocumentContent = message["document"];
                    file_name = DocumentContent.Value<string>("file_name");
                    Extention = new FileInfo(@$"{file_name}").Extension;
                    break;
                case 1:
                    MessageType = IMessage.MsType.Audio;
                    DocumentContent = message["voice"];
                    file_name = $"{ DateTime.Now.ToString().Replace(":", "_").Replace(".", "_")}.ogg";
                    Extention = ".ogg";
                    break;
                case 2:
                    MessageType = IMessage.MsType.Photo;
                    var PhotoArray = message["photo"].ToArray<JToken>();
                    DocumentContent = PhotoArray[PhotoArray.Length - 1];
                    file_name = DocumentContent.Value<string>("file_unique_id") + ".jpg";
                    Extention = ".jpg";
                    break;
            }

            file_id = DocumentContent.Value<string>("file_id");
            file_unique_id = DocumentContent.Value<string>("file_unique_id");
            file_size = DocumentContent.Value<string>("file_size");
            this._client = Client;
        }
        #endregion

        #region Methods
        /// <summary>
        /// If the logic in the bot gives permission, the file is uploaded to the directory obtained from the logic of the bot
        /// </summary>
        /// <param name="DataBase">Data base with information that can be used for answer</param>
        /// <returns>An API request with a message about sucsses of downloading</returns>
        public override List<string> GetAnswer(JsonDataBase DataBase)
        {
            List<string> FileMessageTextAnswers = new List<string>();

            string GetFileLinkResponse = _client.GetFileDownloadPath(file_id);  //make a request and 
            var ParsedResponse = JObject.Parse(GetFileLinkResponse);            //get download link
            bool ResponseIsOk = ParsedResponse.Value<bool>("ok");
            if (ResponseIsOk)
            {
                JToken result = ParsedResponse["result"];
                FileDonwnloadPath = result.Value<string>("file_path");
            }

            FileAgregator File = new FileAgregator(this);           //Create a BotLogic object for analize information about file
            var FileMessageAnswers = File.GetReaction(DataBase);    


            foreach (var Answer in FileMessageAnswers)              //If is allowed by logic downloadfile on drive
            {
                if (FileDonwnloadPath != null)
                {
                    _client.DownloadFile(FileDonwnloadPath, Answer.FileRoute);
                }

                FileMessageTextAnswers.Add($"sendMessage?chat_id={Answer.UserID}&text={Answer.Message}");
            }


            return FileMessageTextAnswers;
        }
        #endregion
    }
}
