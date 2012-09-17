using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagement
{
    public class Tile : GameObject
    {
        public Tile(Game game, Point position)
            : base(game, new Point(position.X, position.Y))
        {
            collisionManager.RegisterObject(this);
            size = new Point(50, 50);
        }

        public override void LoadContent()
        {
            objectSprite = game.Content.Load<Texture2D>("gradient");
            destinationBox = new Rectangle(
                (int)position.X,
                (int)position.Y,
                size.X,
                size.Y
            );
            sourceBox = new Rectangle(
                0,
                0,
                size.X,
                size.Y
            );
        }
    }
}
