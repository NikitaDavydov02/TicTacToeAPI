using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Model
{
    public class GameContext:DbContext
    {
        public GameContext(DbContextOptions<GameContext> options)
        : base(options)
        {
        }

        public DbSet<GameModel> TodoItems { get; set; } = null!;
    }
}
