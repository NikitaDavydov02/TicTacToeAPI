using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Runtime.Serialization;


namespace FirstAPI.Model
{
    [DataContract]
    public class GameModel
    {
        [DataMember]
        public int Id { get; private set; }
        [DataMember]
        public List<List<int>> Field { get;private set; }
        [DataMember]
        public bool ZerosTurn { get; private set; }
        [DataMember]
        public bool FieldIsFull { get; private set; }
        [DataMember]
        public bool GameIsFinished { get; private set; }
        [DataMember]
        private int? FirstPlayerID;
        [DataMember]
        private int? SecondPlayerID;
        [DataMember]
        public DateTime LastChanged { get; private set; }

        /// <summary>
        /// Create GameModel with specified ID
        /// </summary>
        /// <param name="id"></param>
        public GameModel(int id)
        {
            this.Id = id;
            ZerosTurn = true;
            GameIsFinished = false;
            FieldIsFull = false;
            Field = new List<List<int>>();
            for (int i = 0; i < 3; i++)
            {
                Field.Add(new List<int>());
                for (int j = 0; j < 3; j++)
                {
                    Field[i].Add(0);
                }
                    
            }
            LastChanged = DateTime.Now;  
        }
        /// <summary>
        /// Make a turn in the game. Player with specified ID is trying to make move in cell [xIndex, Index]. If move is accepted return true and false in opposite case.
        /// </summary>
        /// <param name="xIndex"></param>
        /// <param name="yIndex"></param>
        /// <param name="playerID"></param>
        /// <returns></returns>
        public bool MakeATurn(int xIndex,int yIndex, int playerID)
        {
            if (ZerosTurn && playerID != FirstPlayerID)
                return false;
            if (!ZerosTurn && playerID != SecondPlayerID)
                return false;
            if (Field[xIndex][yIndex] != 0 || GameIsFinished)
                return false;
            if (xIndex < 0 || xIndex > 2)
                return false;
            if (yIndex < 0 || yIndex > 2)
                return false;
            if (ZerosTurn)
                Field[xIndex][yIndex] = 1;
            else
                Field[xIndex][yIndex] = 2;
            CheckTheEndOfTheGame();
            if(!GameIsFinished)
                ZerosTurn = !ZerosTurn;
            LastChanged = DateTime.Now;
            GameManager.SaveGame(this);
            return true;

        }
        /// <summary>
        /// Return ID if there are less than two players in the game. Return null in opposite case
        /// </summary>
        /// <returns></returns>
        public int? GenerateIDForPlayer()
        {
            LastChanged = DateTime.Now;
            if (FirstPlayerID == null)
            {
                FirstPlayerID = 1;
                return 1;
            }
            if (SecondPlayerID == null)
            {
                SecondPlayerID = 2;
                return 2;
            }
            return null;
        }
        /// <summary>
        /// Check if the game is finished
        /// </summary>
        private void CheckTheEndOfTheGame()
        {
            //Check if the field is full
            FieldIsFull = true;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (Field[i][j] == 0)
                    {
                        FieldIsFull = false;
                        break;
                    }
            if (FieldIsFull)
            {
                GameIsFinished = true;
                return;
            }
            //CheckColoumns
            for (int i = 0; i < 3; i++)
            {
                bool coloumnIsFull = true;
                int firstCell = Field[i][0];
                if (firstCell == 0)
                    continue;
                for(int j=0;j<3;j++)
                    if (firstCell != Field[i][j])
                    {
                        coloumnIsFull = false;
                        break;
                    }
                if (coloumnIsFull)
                {
                    GameIsFinished = true;
                    return;
                }
            }
            //Check Rows
            for (int i = 0; i < 3; i++)
            {
                bool rowIsFull = true;
                int firstCell = Field[0][i];
                if (firstCell == 0)
                    continue;
                for (int j = 0; j < 3; j++)
                    if (firstCell != Field[j][i])
                    {
                        rowIsFull = false;
                        break;
                    }
                if (rowIsFull)
                {
                    GameIsFinished = true;
                    return;
                }
            }
            //Check diagonals
            if((Field[0][0]== Field[1][1]&& Field[1][1] == Field[2][2] && Field[1][1] !=0) || (Field[0][2] == Field[1][1] && Field[1][1] == Field[2][0] && Field[2][1] != 0))
            {
                GameIsFinished = true;
                return;
            }
           
        }
        /// <summary>
        /// Make players ID in the game equal to null (disconnect player) if it was connected to the game. Return true if the process was sucessful and false in opposite case.
        /// </summary>
        /// <param name="playerID"></param>
        /// <returns></returns>
        public bool LeaveTheGame(int playerID)
        {
            if (FirstPlayerID == playerID)
            {
                FirstPlayerID = null;
                return true;
            }
            if (SecondPlayerID == playerID)
            {
                SecondPlayerID = null;
                return true;
            }
            return false;
        }
    }

}
