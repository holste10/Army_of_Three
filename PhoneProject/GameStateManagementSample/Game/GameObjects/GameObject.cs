using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameStateManagement.Game.GameObjects
{
    public abstract class GameObject
    {
        public GameObject(Microsoft.Xna.Framework.Game game, Point position)
        {
            this.game = game;
            this.position = new Vector2(position.X, position.Y);
        }

        public Texture2D objectSprite { get; set; }
        public Rectangle destinationBox { get; set; }
        public Rectangle sourceBox { get; set; }

        protected Microsoft.Xna.Framework.Game game;
        public Vector2 position { get; set; }

        public virtual void LoadContent() { }
        public virtual void Update(float deltaTime) { }
    }

    public class Character : GameObject
    {
        public Character(Microsoft.Xna.Framework.Game game, Point position)
            : base(game, position)
        {

        }
    }

    /// <summary>
    /// Example of how a gameobject can be created. 
    /// easy huh? :D
    /// </summary>
    public class Player : Character
    {
        Vector2 velocity;
        Vector2 gravity;
        public Player(Microsoft.Xna.Framework.Game game, Point position, Vector2 gravity)
            : base(game, position)
        {
            velocity = new Vector2(0, 0);
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
            float x = position.X;
            x += deltaTime * velocity.X;

            position = deltaTime * gravity;
            position = deltaTime * velocity;

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
