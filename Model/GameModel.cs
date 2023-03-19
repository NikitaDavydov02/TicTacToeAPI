using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FirstAPI.Model
{
    public class GameModel
    {
        public int Id { get; private set; } = 0;
        private string password;
        public string Password { set
            {
                password = value;
            } }
        public int[,] field { get; private set; }
        public bool ZerosTurn { get; private set; }
        public bool GameIsFinished { get; private set; }

        public GameModel()
        {
            ZerosTurn = true;
            GameIsFinished = false;
            field = new int[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    field[i, j] = 0;
        }
        public bool MakeATurn(int xIndex,int yIndex)
        {
            if (field[xIndex, yIndex] != 0 || GameIsFinished)
                return false;
            if (xIndex < 0 || xIndex > 2)
                return false;
            if (yIndex < 0 || yIndex > 2)
                return false;
            if (ZerosTurn)
                field[xIndex, yIndex] = 1;
            else
                field[xIndex, yIndex] = 2;
            CheckTheEndOfTheGame();
            if(!GameIsFinished)
                ZerosTurn = !ZerosTurn;
            //OnMoveIsMade();
            return true;
        }
        private void CheckTheEndOfTheGame()
        {
            //CheckColoumns
            for(int i = 0; i < 3; i++)
            {
                bool coloumnIsFull = true;
                int firstCell = field[i, 0];
                if (firstCell == 0)
                    continue;
                for(int j=0;j<3;j++)
                    if (firstCell != field[i, j])
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
            for (int i = 0; i < 3; i++)
            {
                bool rowIsFull = true;
                int firstCell = field[0, i];
                if (firstCell == 0)
                    continue;
                for (int j = 0; j < 3; j++)
                    if (firstCell != field[j, i])
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
            if((field[0,0]== field[1, 1]&& field[1, 1] == field[2, 2] && field[1, 1] !=0) || (field[0, 2] == field[1, 1] && field[1, 1] == field[2, 0] && field[2, 1] != 0))
            {
                GameIsFinished = true;
                return;
            }
        }
    }

}
