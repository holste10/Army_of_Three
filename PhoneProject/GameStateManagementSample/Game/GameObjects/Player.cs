using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/************************************************************************/
/*  TODO: 
 *          - Add special ability logic
 *          - Add correct sprite and animation
/************************************************************************/
namespace GameStateManagement
{

    #region Class PlayerGreen

    /// <summary>
    /// Just a placeholder character, with a green square.
    /// TODO: Add the actual sprite and animations. 
    ///       Add special ability.
    /// </summary>
     public class PlayerGreen : Character
     {
        public PlayerGreen(Game game, Point position, Vector2 gravity)
            : base(game, position, gravity)
        {

        }

        public override void LoadContent()
        {
            objectSprite = game.Content.Load<Texture2D>("green");
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            //if action button pressed
            //DoAction();
            //------------------------
        }

        public override void DoAction()
        {
            // Do this type of character special ability
        }
    }
    #endregion

    #region Class PlayerRed

     /// <summary>
    /// Just a placeholder character, with a Red square.
    /// TODO: Add the actual sprite and animations. 
    ///       Add special ability.
    /// </summary>
    public class PlayerRed : Character
    {
        public PlayerRed(Game game, Point position, Vector2 gravity)
            : base(game, position, gravity)
        {

        }

        public override void LoadContent()
        {
            objectSprite = game.Content.Load<Texture2D>("red");
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            //if action button pressed
            //DoAction();
            //------------------------
        }

        public override void DoAction()
        {
            // Do this type of character special ability
        }
    }

    #endregion

    #region Class PlayerBlue

    /// <summary>
    /// Just a placeholder character, with a Blue square.
    /// TODO: Add the actual sprite and animations. 
    ///       Add special ability.
    /// </summary>
    public class PlayerBlue : Character
    {
        public PlayerBlue(Game game, Point position, Vector2 gravity)
            : base(game, position, gravity)
        {
            // Do this type of character special ability
        }

        public override void LoadContent()
        {
            objectSprite = game.Content.Load<Texture2D>("blue");
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            //if action button pressed
            //DoAction();
            //------------------------
        }

        public override void DoAction()
        {
            // Do this type of character special ability
        }
    }

    #endregion
}
