
namespace HomeWork_9.My_TG_API
{
    /// <summary>
    /// Represents one button of an inline keyboard
    /// </summary>
    public class InlineKeyboardButton
    {
        #region Fields
        /// <summary>
        /// Text that will be writen on button
        /// </summary>
        public string text;
        /// <summary>
        /// Command to a bot
        /// </summary>
        public string callback_data;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialize a new instance of InlineKeyboardButton
        /// </summary>
        /// <param name="text">Text that will be writen on button</param>
        /// <param name="callback_data">Command to a bot without '/'</param>
        public InlineKeyboardButton(string text, string callback_data) 
        {
        this.text = text;
        this.callback_data = "/"+callback_data;
        }
        #endregion
    }
}
