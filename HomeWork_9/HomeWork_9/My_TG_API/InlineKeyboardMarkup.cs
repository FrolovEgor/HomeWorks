using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HomeWork_9.My_TG_API
{   /// <summary>
    /// Represents an inline keyboard that appears right next to the message it belongs to
    /// </summary>
    public class InlineKeyboardMarkup
    {
        #region Fields
        /// <summary>
        /// Commands that witt be serilized in a JSON 
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        private List<List<InlineKeyboardButton>> inline_keyboard;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialize a new instance of InlineKeyboardMarkup
        /// </summary>
        /// <param name="columns">Count of columns in chat</param>
        /// <param name="buttons">Buttons <see cref="InlineKeyboardButton"/> that sound be send</param>
        public InlineKeyboardMarkup(int columns, params InlineKeyboardButton[] buttons) 
        {
            inline_keyboard = new List<List<InlineKeyboardButton>>();

            List<InlineKeyboardButton> TempKeyboardRow = new List<InlineKeyboardButton>();
            foreach (var Butt in buttons)
            {
                if (TempKeyboardRow.Count == columns)
                {
                    inline_keyboard.Add(TempKeyboardRow);
                    TempKeyboardRow = new List<InlineKeyboardButton>();
                }
                TempKeyboardRow.Add(Butt);
            }
            inline_keyboard.Add(TempKeyboardRow);
        }
        #endregion
    }
}
