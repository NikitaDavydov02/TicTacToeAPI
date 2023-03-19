using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstAPI.Model;


namespace FirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicTacToeController : ControllerBase
    {
        //private readonly GameContext _context;
        private static GameModel model;
        //[HttpPut]
        //public IActionResult Move(Turn move)
        //{
        //    if (model.MakeATurn(move.xIndex, move.yIndex))
        //        return CreatedAtAction("move", model.field);
        //    return CreatedAtAction("move", model.field);

        //}
        [HttpPut]
        public GameModel Move(Turn move)
        {
            model.MakeATurn(move.xIndex, move.yIndex);
                //return CreatedAtAction("move", model.field);
            return model;

        }
        [HttpGet]
        public IActionResult NewGame()
        {
            model = new GameModel();

            //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return Ok(model);
        }
    }
}
