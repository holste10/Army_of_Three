using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagement
{
    /// <summary>
    /// Base class for all characters in the game. 
    /// </summary>
    public abstract class Character : GameObject
    {
        protected Vector2 gravity;

        public Character(Game game, Point position, Vector2 gravity)
            : base(game, position)
        {
            this.gravity = gravity;
        }

        public override void LoadContent()
        {
            base.LoadContent();
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
            base.Update(deltaTime);
            position += deltaTime * velocity;

            destinationBox = new Rectangle(
                (int)position.X,
                (int)position.Y,
                objectSprite.Width,
                objectSprite.Height);
        }

        public override void ApplyGravity(float deltaTime)
        {
            velocity += deltaTime * gravity;
        }

        public virtual void DoAction() { }

        public virtual void Jump()
        {
            velocity = new Vector2(velocity.X, -300);
        }
    }
}
