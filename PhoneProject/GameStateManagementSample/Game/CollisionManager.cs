using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameStateManagement
{
    public class CollisionManager : Microsoft.Xna.Framework.GameComponent, ICollisionService
    {
        List<GameObject> _collidables;

        public CollisionManager(Game game)
            : base(game)
        {
            _collidables = new List<GameObject>();
        }

        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameObject go in _collidables)
            {
                if (go.deleteFlag)
                {
                    _collidables.Remove(go);
                }
            }

            foreach (GameObject go1 in _collidables)
            {
                foreach (GameObject go2 in _collidables)
                {
                    if (go1 == go2)
                    {
                        continue;
                    }

                    if (go1.destinationBox.Intersects(go2.destinationBox))
                    {
                        go1.Collision(go2);
                    }
                }
            }

            base.Update(gameTime);
        }

        public void RegisterObject(GameObject go)
        {
            _collidables.Add(go);
        }
    }
}