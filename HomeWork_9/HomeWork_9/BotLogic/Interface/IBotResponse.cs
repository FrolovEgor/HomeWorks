using System.Collections.Generic;
using HomeWork_9.BotLogic.DataBase;

namespace HomeWork_9.BotLogic.Interface
{   /// <summary>
    /// Defines a method that create an correct type Bot Answer for diferent types of User requests
    /// </summary>
    internal interface IBotResponse
    {
        /// <summary>
        /// Create a Aswer from Bot logical side to bot web side in unique format <see cref="LogicAnswer"/> 
        /// </summary>
        /// <param name="DataBase">Data base with information that can be used for answer</param>
        /// <returns>One or more Answer's from Bot logic side</returns>
        public List<LogicAnswer> GetReaction(JsonDataBase DataBase);

            
    }
}
