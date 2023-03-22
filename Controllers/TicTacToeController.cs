using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstAPI.Model;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Runtime.Serialization;

namespace FirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicTacToeController : ControllerBase
    {
        /// <summary>
        /// Make a turn in the game. To do this playerID and gameID are required as well as the turn. If game wasn't found or playerID is incorrect or turn is incorrect return BadRequest, otherwise return updated GameModel;
        /// </summary>
        /// <param name="move">Turn struct that contains information about move</param>
        /// <param name="playerID">Player ID in this game</param>
        /// <param name="gameID">Game ID of game to move</param>
        /// <returns></returns>
        [HttpPut("{gameID}/turn")]
        public IActionResult MakeATurn(Turn move, int playerID, int gameID)
        {
            GameModel model = GameManager.GetGameByID(gameID);
            if (model == null)
                return StatusCode(500);
            if (!model.MakeATurn(move.xIndex, move.yIndex, playerID))
                return StatusCode(503);
            return Ok(model);
        }
        /// <summary>
        /// Create new game at server and return its id
        /// </summary>
        /// <returns></returns>
        [HttpGet("new")]
        public IActionResult NewGame()
        {
            GameModel model = GameManager.CreateNewGame();
            if (model == null)
                return StatusCode(500);
            return Ok(model.Id);
        }
        /// <summary>
        /// Connect to the game by id. (Generate ID for player if there is still place in the game)
        /// </summary>
        /// <param name="gameID"></param>
        /// <returns></returns>
        [HttpGet("{gameID}/connect")]
        public IActionResult ConnectToTheGame(int gameID)
        {
            GameModel model = GameManager.GetGameByID(gameID);
            if (model == null)
                return StatusCode(500);
            int? playerID = model.GenerateIDForPlayer();
            GameManager.SaveGame(model);
            if (playerID == null)
                return StatusCode(503);
            else
            {
                InitializeConnectionStruct output = new InitializeConnectionStruct();
                output.Id = playerID;
                output.Model = model;
                return Ok(output);
            }

        }
        [HttpGet("{gameID}/disconnect")]
        public IActionResult DosconnectFromTheGame(int gameID, int playerID)
        {
            GameModel model = GameManager.GetGameByID(gameID);
            if (model == null)
                return StatusCode(500);
            bool result = model.LeaveTheGame(playerID);
            GameManager.SaveGame(model);
            if (!result)
                return StatusCode(503);
            else
                return Ok();

        }
    }
}
