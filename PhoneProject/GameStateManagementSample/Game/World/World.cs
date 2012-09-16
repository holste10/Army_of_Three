using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameStateManagement
{
    public class World
    {
        private Texture2D _background;

        public Texture2D background
        {
            get { return _background; }
            set { _background = value; }
        }

        private Rectangle _destinationBox;

        public Rectangle destinationBox
        {
            get { return _destinationBox; }
        }

        private Game game;

        public World(Game game)
        {
            this.game = game;
        }

        public void LoadContent()
        {
            background = game.Content.Load<Texture2D>("world");
            _destinationBox = new Rectangle(
                0,
                0,
                _background.Width,
                _background.Height
            );
        }
    }
}
