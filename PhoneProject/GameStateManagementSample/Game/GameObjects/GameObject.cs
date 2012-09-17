using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameStateManagement
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class GameObject
    {
        protected CollisionManager collisionManager;

        public GameObject(Microsoft.Xna.Framework.Game game, Point position)
        {
            this.game = game;
            this.position = new Vector2(position.X, position.Y);
            this.velocity = Vector2.Zero;
            drawObject = true; //Letting it be default that an gameObject is drawn
            collisionManager = (CollisionManager) game.Services.GetService(typeof(CollisionManager));
        }

        public bool deleteFlag { get; set; }
        public bool drawObject { get; set; }
        public Texture2D objectSprite { get; set; }
        public Point size { get; set; }
        public Rectangle destinationBox { get; set; }
        public Rectangle prevDestinationBox { get; set; }
        public Rectangle sourceBox { get; set; }

        protected Microsoft.Xna.Framework.Game game;
        public Vector2 position { get; set; }
        public Vector2 velocity { get; set; }
        public virtual void LoadContent() { }
        public virtual void Update(float deltaTime) 
        {
            prevDestinationBox = destinationBox;
        }
        public virtual void ApplyGravity(float deltaTime) { }
        public virtual void Collision(GameObject otherObj) { }
    }
}
    