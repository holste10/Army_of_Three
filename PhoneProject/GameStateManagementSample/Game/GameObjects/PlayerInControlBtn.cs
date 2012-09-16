using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagement
{

    /// <summary>
    /// A class for the selection of player in control on top of the screen. 
    /// </summary>
    enum PlayerSelection { P1, P2, P3};
    class PlayerInControlBtn : GameObject
    {

        private PlayerSelection playerSelection;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        /// <param name="position"></param>
        /// <param name="playerselection">Which of the 3 gui elements for player selection this inistance represents</param>
        public PlayerInControlBtn(Game game, Point position, PlayerSelection playerselection)
            : base(game, position)
        {
            this.playerSelection = playerselection;
        }

        public override void LoadContent()
        {
            switch (playerSelection)
            {
                case PlayerSelection.P1:
                    objectSprite = game.Content.Load<Texture2D>("clickforp1");
                    break;
                case PlayerSelection.P2:
                    objectSprite = game.Content.Load<Texture2D>("clickforp2");
                    break;
                case PlayerSelection.P3:
                    objectSprite = game.Content.Load<Texture2D>("clickforp3");
                    break;
            }
            
            destinationBox = new Rectangle(
                (int)position.X,
                (int)position.Y,
                objectSprite.Width,
                objectSprite.Height
            );

            sourceBox = new Rectangle(
                0,
                0,
                objectSprite.Width,
                objectSprite.Height
            );
        }

        public override void Update(float deltaTime)
        {

        }

    }
}
