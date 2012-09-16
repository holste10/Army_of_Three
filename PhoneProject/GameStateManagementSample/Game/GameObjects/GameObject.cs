using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameStateManagement
{
    public abstract class GameObject
    {
        
        public GameObject(Microsoft.Xna.Framework.Game game, Point position)
        {
            this.game = game;
            this.position = new Vector2(position.X, position.Y);
            drawObject = true; //Letting it be default that an gameObject is drawn
        }

        public bool drawObject { get; set; }
        public Texture2D objectSprite { get; set; }
        public Rectangle destinationBox { get; set; }
        public Rectangle sourceBox { get; set; }

        protected Microsoft.Xna.Framework.Game game;
        public Vector2 position { get; set; }
        public Vector2 velocity { get; set; }
        public virtual void LoadContent() { }
        public virtual void Update(float deltaTime) { }
        public virtual void ApplyGravity(float deltaTime) { }
    }
}
    