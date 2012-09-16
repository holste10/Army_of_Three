using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagement
{
     public class Player : Character
    {
        Vector2 gravity;
        public Player(Microsoft.Xna.Framework.Game game, Point position, Vector2 gravity)
            : base(game, position)
        {
            velocity = new Vector2(0.0f, 0.0f);
            this.gravity = gravity;
        }

        public override void LoadContent()
        {
            objectSprite = game.Content.Load<Texture2D>("green");
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

            //velocity += deltaTime * gravity;
            position += deltaTime * velocity;

            destinationBox = new Rectangle(
                (int)position.X,
                (int)position.Y,
                objectSprite.Width,
                objectSprite.Height);

            //if action button pressed
            //DoAction();
            //------------------------
        }

        public virtual void Jump()
        {
            velocity = new Vector2(velocity.X, -1);
        }

        public override void ApplyGravity(float deltaTime)
        {
            velocity += deltaTime * gravity;
        }
        public virtual void DoAction()
        {

        }
    }

    /*  public class GreenDude : Player
      {
          public GreenDude(Microsoft.Xna.Framework.Game game, Point position)
              : base(game, position)
          { 
            
          }

          public override void LoadContent()
          {
              objectSprite = game.Content.Load<Texture2D>("green");
              base.LoadContent();
          }

          public override void DoAction()
          {
            
          }
      }*/
}
