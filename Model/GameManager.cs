using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Timers;

namespace FirstAPI.Model
{
    public static class GameManager
    {
        private static float minuteIntervalBeforeDeleteGame = 2;
        /// <summary>
        /// Delete games that weren't changed for more than specified time interval
        /// </summary>
        public static void DeleteOldGames()
        {
            int i = 0;
            while (GetGameByID(i) != null)
            {
                GameModel game = GetGameByID(i);
                if ((DateTime.Now - game.LastChanged).Minutes >= minuteIntervalBeforeDeleteGame)
                    DeleteGame(game);
                i++;
            }
        }
        /// <summary>
        /// Create new GameModel object and write it down into the file with at the server. Returncreated Game Model
        /// </summary>
        /// <returns></returns>
        public static GameModel CreateNewGame()
        {
            int id = FindIDForNewGame();
            GameModel model = new GameModel(id);
            using (FileStream fs = new FileStream(@"Game"+id.ToString(), FileMode.Create))
            {
                DataContractSerializer ds = new DataContractSerializer(typeof(GameModel));
                ds.WriteObject(fs, model);
            }
            return model;
        }
        /// <summary>
        /// Find a name for new game file at the server
        /// </summary>
        /// <returns></returns>
        private static int FindIDForNewGame()
        {
            int i = 0;
            while (File.Exists(@"Game" + i.ToString()))
                i++;
            return i;

        }
        /// <summary>
        /// Read game from server by its ID. Return null if game doesn't exists
        /// </summary>
        /// <param name="id">Game to return ID</param>
        /// <returns></returns>
        public static GameModel GetGameByID(int id)
        {
            if (!File.Exists(@"Game" + id.ToString()))
                return null;
            GameModel model;
            using (FileStream fs = new FileStream(@"Game"+id.ToString(), FileMode.Open))
            {
                DataContractSerializer ds = new DataContractSerializer(typeof(GameModel));
                model = ds.ReadObject(fs) as GameModel;

            }
            return model;
        }
        /// <summary>
        /// Save GameModel at the server by its ID
        /// </summary>
        /// <param name="model">Game Model to save</param>
        public static void SaveGame(GameModel model)
        {
            if (File.Exists(@"Game" + model.Id.ToString()))
                File.Delete(@"Game" + model.Id.ToString());
            using (FileStream fs = new FileStream(@"Game" + model.Id.ToString(), FileMode.CreateNew))
            {
                DataContractSerializer ds = new DataContractSerializer(typeof(GameModel));
                ds.WriteObject(fs, model);

            }
        }
        /// <summary>
        /// Delete game from the server
        /// </summary>
        /// <param name="game">game to delete</param>
        public static void DeleteGame(GameModel game)
        {
            if (File.Exists(@"Game" + game.Id.ToString()))
                File.Delete(@"Game" + game.Id.ToString());
        }
    }
}
