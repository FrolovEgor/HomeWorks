using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;



namespace HomeWork_9.BotLogic.DataBase
{   /// <summary>
    /// Object that contains constant Data for Bot lonic and saving it in JSON format on drive
    /// </summary>
    public class JsonDataBase
    {
        #region fields
        /// <summary>
        /// Default path to answer dictionary
        /// </summary>
        private string _pathToDictionary = @"./../../../Answers.json";
        /// <summary>
        /// Default path to not answered text messages
        /// </summary>
        private string _pathToRequestList = @"./../../../Request.json";
        /// <summary>
        /// Default path to list of administrators
        /// </summary>
        private string _pathToAdmins = @"./../../../Admins.json";
        /// <summary>
        /// dictionary of pre-added answers on text messages. TKey is a string user text message. TValue is a string bot answer 
        /// </summary>
        public Dictionary<string, string> AnswersDictionary;
        /// <summary>
        /// List of text messages witch don't have pre-added answers
        /// </summary>
        public List<string> NewRequests;
        /// <summary>
        /// List of users who have administrator rights
        /// </summary>
        public List<long> Admins;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new istance of empty DataBase
        /// </summary>
        public JsonDataBase()
        {
            new Dictionary<string, string>();
            new Dictionary<string, string>();
            Admins = new List<long>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Deserelize this instance of <see cref="HomeWork_9.BotLogic.DataBase.JsonDataBase"/> from default Json files 
        /// </summary>
        public void Deserelize() 
        {
            Deserelize(_pathToDictionary, _pathToRequestList, _pathToAdmins);
        }
        /// <summary>
        /// Deserelize this instance of <see cref="HomeWork_9.BotLogic.DataBase.JsonDataBase"/> from json files sets in arguments
        /// </summary>
        /// <param name="PathTodictionary">path to json file, contains serelized <see cref="Dictionary{string,string}"/> of strings</param>
        /// <param name="PathToRequestList">path to json file, contains serelized <see cref="List{string}"/> of strings</param>
        /// <param name="PathToAdmins">path to json file, contains serelized <see cref="List{long}"/> of longs</param>
        public void Deserelize(string PathTodictionary, string PathToRequestList, string PathToAdmins)
        {
            CommonDeserelize<Dictionary<string, string>>(PathTodictionary, ref AnswersDictionary);
            CommonDeserelize<List<string>>(PathToRequestList, ref NewRequests);
            CommonDeserelize<List<long>>(PathToAdmins, ref Admins);
        }
        /// <summary>
        /// Used to deserelize from file any JsonDataBase collection
        /// </summary>
        /// <typeparam name="T">Type of deserelized collection</typeparam>
        /// <param name="PathToJson">path to deserelized JSON file</param>
        /// <param name="DeserelizedObject">instance of deserelized object</param>
        private void CommonDeserelize<T>(string PathToJson,ref T DeserelizedObject) 
        {
            if (File.Exists(PathToJson))
            {
                string TextFromJson = File.ReadAllText(PathToJson);

                DeserelizedObject = JsonConvert.DeserializeObject<T>(TextFromJson);
            }
        }
        /// <summary>
        /// Serelize this instance of <see cref="HomeWork_9.BotLogic.DataBase.JsonDataBase"/> in json files with default path
        /// </summary>
        public void Serelize()
        {
            Serelize(_pathToDictionary, _pathToRequestList, _pathToAdmins);
        }
        /// <summary>
        /// Serelize this instance of <see cref="HomeWork_9.BotLogic.DataBase.JsonDataBase"/> in json files sets in arguments
        /// </summary>
        /// <param name="PathTodictionary">Path to Json file to serelize current instance of <see cref="HomeWork_9.BotLogic.DataBase.JsonDataBase.AnswersDictionary"/></param>
        /// <param name="PathToRequestList">Path to Json file to serelize current instance of <see cref="HomeWork_9.BotLogic.DataBase.JsonDataBase.NewRequests"/></param>
        /// <param name="PathToAdmins">Path to Json file to serelize current instance of <see cref="HomeWork_9.BotLogic.DataBase.JsonDataBase.Admins"/></param>
        public void Serelize(string PathTodictionary, string PathToRequestList, string PathToAdmins)
        {
            CommonSerelizeAsync(PathTodictionary, AnswersDictionary);
            CommonSerelizeAsync(PathToRequestList, NewRequests);
            CommonSerelizeAsync(PathToAdmins, Admins);
        }
        /// <summary>
        /// Used to Serelize in file any JsonDataBase collection
        /// </summary>
        /// <param name="PathToJson">path to JSON file</param>
        /// <param name="SerelizedObject">instance of Serelized object</param>
        private async void CommonSerelizeAsync(string PathToJson, object SerelizedObject)
        {
            string JsonDict = JsonConvert.SerializeObject(SerelizedObject);
            if (File.Exists(PathToJson)) File.Delete(PathToJson);

            byte[] buffer = Encoding.Default.GetBytes(JsonDict);

            using (var fStream = new FileStream(PathToJson, FileMode.Create)) 
            {
                await fStream.WriteAsync(buffer, 0, buffer.Length);
            }
        }
        #endregion
    }
}
