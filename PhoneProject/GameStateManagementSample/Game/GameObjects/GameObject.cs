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
        protected Vector2 position;

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

    public class Player : Character
    {
        public Player(Microsoft.Xna.Framework.Game game, Point position)
            : base(game, position)
        {
            
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
            // Move 

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
}
