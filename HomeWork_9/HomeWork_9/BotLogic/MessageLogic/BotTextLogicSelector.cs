using HomeWork_9.BotLogic.Interface;

namespace HomeWork_9.BotLogic.MessageLogic
{
    /// <summary>
    /// Select with class need to create for answer to text message
    /// </summary>
    static class BotTextLogicSelector
    {
        /// <summary>
        /// Select with class need to create for answer to text message
        /// </summary>
        /// <param name="Type">Message that have a text type</param>
        /// <returns>An instance of logic class</returns>
        public static IBotResponse SelectTextMessageLogic(IMessage Type) 
        {
            if (Type.MessageType == IMessage.MsType.adminCMD) return new AdminComands(Type);
            if (Type.MessageType == IMessage.MsType.userCMD) return new UserComands(Type);
            return new DictionaryTalk(Type);
        }

    }
}
