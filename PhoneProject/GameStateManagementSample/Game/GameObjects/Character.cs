using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagement
{
    public class Character : GameObject
    {
        public Character(Microsoft.Xna.Framework.Game game, Point position)
            : base(game, position)
        {

        }
    }
}
