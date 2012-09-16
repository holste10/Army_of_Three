using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagement
{
    enum buttonType { MOVE_LEFT, MOVE_RIGHT, JUMP, SPECIAL_ATTACK }

    class ClickableRectangle : GameObject
    {
        /// <summary>
        /// Change value to show/hide the texture of the button. In gameplay, the button
        /// will be hidden, but can be changed to show for demonstration/guidance purposes.
        /// </summary>

        private buttonType btnType;
        public ClickableRectangle(Game game, Rectangle btnRect, buttonType btnType)
            : base(game, new Point(btnRect.X, btnRect.Y))
        {
            this.btnType = btnType;
            destinationBox = btnRect;
            sourceBox = btnRect;
        }

        public override void LoadContent()
        {
            switch (btnType)
            {
                case buttonType.JUMP:
                    objectSprite = game.Content.Load<Texture2D>("jumpBtn");
                    break;
                case buttonType.MOVE_LEFT:
                    objectSprite = game.Content.Load<Texture2D>("leftBtn");
                    break;
                case buttonType.MOVE_RIGHT:
                    objectSprite = game.Content.Load<Texture2D>("rightBtn");
                    break;
                case buttonType.SPECIAL_ATTACK:
                    objectSprite = game.Content.Load<Texture2D>("specialAbilityBtn");
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
