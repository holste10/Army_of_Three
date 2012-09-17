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
            collisionManager.RegisterObject(this);
            size = new Point(30, 30);
        }

        public override void LoadContent()
        {
            base.LoadContent();

            destinationBox = new Rectangle(
                (int)position.X,
                (int)position.Y,
                size.X,
                size.Y
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

            destinationBox = new Rectangle(
                (int)position.X,
                (int)position.Y,
                size.X,
                size.Y);

            position += deltaTime * velocity;
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

        public override void Collision(GameObject otherObj)
        {
            Rectangle collisionRect = Rectangle.Intersect(destinationBox, otherObj.destinationBox);

            if (!(otherObj is Character))
            {
                // Check otherObj's right side.
                if (otherObj.destinationBox.Right > destinationBox.Left && otherObj.destinationBox.Right <= prevDestinationBox.Left)
                {
                    position = new Vector2((float)collisionRect.Width + position.X, position.Y);
                }
                // Check otherObj's left side.
                if (otherObj.destinationBox.Left < destinationBox.Right && otherObj.destinationBox.Left >= prevDestinationBox.Right)
                {
                    position = new Vector2(position.X - (float)destinationBox.Width, position.Y);
                }
                // Check otherObj's top side.
                if (otherObj.destinationBox.Top < destinationBox.Bottom && otherObj.destinationBox.Top >= prevDestinationBox.Bottom)
                {
                    position = new Vector2(position.X, position.Y - (float)destinationBox.Height);
                    velocity = new Vector2(velocity.X, 0);
                }
                // Check otherObj's bottom side.
                if (otherObj.destinationBox.Bottom > destinationBox.Top && otherObj.destinationBox.Bottom <= prevDestinationBox.Top)
                {
                    position = new Vector2(position.X, position.Y + (float)destinationBox.Height);
                    velocity = new Vector2(velocity.X, 0);
                }
            }

            base.Collision(otherObj);        
        }
    }
}
