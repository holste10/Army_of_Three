#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Threading;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using GameStateManagement.Game.GameObjects;

using Microsoft.Xna.Framework.Input.Touch;
#endregion

namespace GameStateManagement
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class GameplayScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        SpriteFont gameFont;

        Vector2 playerPosition = new Vector2(100, 100);
        Vector2 enemyPosition = new Vector2(100, 100);
        private SpriteBatch spriteBatch;
        Random random = new Random();
        Vector2 gravity = new Vector2(0, 50);

        List<GameObject> gameObjects = new List<GameObject>();

        Rectangle moveLeftBtn;
        Rectangle moveRightBtn;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            EnabledGestures = GestureType.Hold | GestureType.Tap;
            // EnabledGestures = 
            moveLeftBtn = new Rectangle(0, 320, 160, 160);
            moveRightBtn = new Rectangle(640, 320, 160, 160);

        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            spriteBatch = ScreenManager.SpriteBatch;
            gameFont = content.Load<SpriteFont>("gamefont");
            gameObjects.Add(new Player(ScreenManager.Game, new Point(100, 100), gravity));

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.LoadContent();
            }


            // A real game would probably have more content than this sample, so
            // it would take longer to load. We simulate that by delaying for a
            // while, giving you a chance to admire the beautiful loading screen.
            //Thread.Sleep(1000);

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if (IsActive)
            {
                // Apply some random jitter to make the enemy move around.
                const float randomization = 10;

                enemyPosition.X += (float)(random.NextDouble() - 0.5) * randomization;
                enemyPosition.Y += (float)(random.NextDouble() - 0.5) * randomization;

                // Apply a stabilizing force to stop the enemy moving off the screen.
                Vector2 targetPosition = new Vector2(
                    ScreenManager.GraphicsDevice.Viewport.Width / 2 - gameFont.MeasureString("Insert Gameplay Here").X / 2,
                    200);

                enemyPosition = Vector2.Lerp(enemyPosition, targetPosition, 0.05f);
                foreach (GameObject obj in gameObjects)
                {
                    obj.Update(gameTime.ElapsedGameTime.Milliseconds);
                }

            }
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            TouchCollection tc = input.TouchState;

            while (TouchPanel.IsGestureAvailable)
            {
                Debug.WriteLine("1");
            }

            if (!TouchPanel.IsGestureAvailable)
            {
                Debug.WriteLine("2");
            }
            TouchCollection touches = TouchPanel.GetState();
            if (touches.Count == 0)
            {   
                gameObjects[0].velocity = new Vector2(0, 0);
            }
            foreach (TouchLocation touch in touches)
            {
                //Vector2 position = gs.Position;
                if ((touch.State == TouchLocationState.Pressed
                        || touch.State == TouchLocationState.Moved))
                {
                    CheckForMovement(touch.Position);
                    break;
                }   
            }
            //foreach (GestureSample gs in gestures)
            //{
            //    Vector2 position = gs.Position;
            //    if((touches.State == TouchLocationState.Pressed
            //            || touch.State == TouchLocationState.Moved)
            //            && touch.Position.X > centerOfScreen)
            //    {
            //        DoSomething();
            //        break;
            //    }
            //    if (gs.GestureType == GestureType.FreeDrag)
            //    {
            //        CheckForMovement(position);
            //    }
            //    if (gs.GestureType == GestureType.DragComplete)
            //    {
            //        CheckForMovement(position);
            //    }

            //    //switch (gs.GestureType)
            //    //{
            //    //    case GestureType.FreeDrag:
            //    //        CheckForMovement(position);
            //    //        break;
            //    //    case GestureType.Tap:
            //    //        CheckForMovement(position);
            //    //        break;
            //    //    case GestureType.Hold:
            //    //        CheckForMovement(position);
            //    //        break;
            //    //}
            //}

            // if the user pressed the back button, we return to the main menu
            PlayerIndex player;
            if (input.IsNewButtonPress(Buttons.Back, ControllingPlayer, out player))
            {
                LoadingScreen.Load(ScreenManager, false, ControllingPlayer, new BackgroundScreen(), new MainMenuScreen());
            }

            #region asd
            //    else
            //    {
            //        // Otherwise move the player position.
            //        Vector2 movement = Vector2.Zero;

            //        if (keyboardState.IsKeyDown(Keys.Left))
            //            movement.X--;

            //        if (keyboardState.IsKeyDown(Keys.Right))
            //            movement.X++;

            //        if (keyboardState.IsKeyDown(Keys.Up))
            //            movement.Y--;

            //        if (keyboardState.IsKeyDown(Keys.Down))
            //            movement.Y++;

            //        Vector2 thumbstick = gamePadState.ThumbSticks.Left;

            //        movement.X += thumbstick.X;
            //        movement.Y -= thumbstick.Y;

            //        if (movement.Length() > 1)
            //            movement.Normalize();

            //        playerPosition += movement * 2;
            //    }
            #endregion

        }

        private void CheckForAbilitiesUse(Vector2 position)
        {
            //if (burp.Contains((int)position.X, (int)position.Y))
            //{
            //    Debug.WriteLine("X:{0}, Y:{1}", position.X, position.Y);
            //}
        }

        private void CheckForMovement(Vector2 position)
        {
            if (moveRightBtn.Contains((int)position.X, (int)position.Y))
            {
                gameObjects[0].velocity = new Vector2(0.2f, 0);
            }
            else if (moveLeftBtn.Contains((int)position.X, (int)position.Y))
            {
                gameObjects[0].velocity = new Vector2(-0.2f, 0);
            }
            
        }

        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.CornflowerBlue, 0, 0);

            // Our player and enemy are both actually just text strings.
            spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();

            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.objectSprite != null)
                {
                    spriteBatch.Draw(
                        gameObject.objectSprite,
                        gameObject.destinationBox,
                        gameObject.sourceBox,
                        Color.White
                    );
                }
            }

            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0)
                ScreenManager.FadeBackBufferToBlack(1f - TransitionAlpha);
        }


        #endregion
    }
}
