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
        Vector2 gravity = new Vector2(0, 1);

        private List<GameObject> gameObjects = new List<GameObject>();
        private List<Player> players = new List<Player>();
        private int currentPlayerIndex;

        private PlayerInControlBtn p1s;
        private PlayerInControlBtn p2s;
        private PlayerInControlBtn p3s;

        private ClickableRectangle moveLeftBtn;
        private ClickableRectangle moveRightBtn;
        private ClickableRectangle jumpBtn;
        private ClickableRectangle abilityBtn;

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

            moveLeftBtn = new ClickableRectangle(ScreenManager.Game, new Rectangle(0, 320, 160, 160), buttonType.MOVE_LEFT);
            moveRightBtn = new ClickableRectangle(ScreenManager.Game, new Rectangle(640, 320, 160, 160), buttonType.MOVE_RIGHT);
            jumpBtn = new ClickableRectangle(ScreenManager.Game, new Rectangle(0, 160, 160, 160), buttonType.JUMP);
            abilityBtn = new ClickableRectangle(ScreenManager.Game, new Rectangle(640, 160, 160, 160), buttonType.SPECIAL_ATTACK);
            gameObjects.Add(moveLeftBtn);
            gameObjects.Add(moveRightBtn);
            gameObjects.Add(jumpBtn);
            gameObjects.Add(abilityBtn);
            p1s = new PlayerInControlBtn(ScreenManager.Game, new Point(200, 0), PlayerSelection.P1);
            p2s = new PlayerInControlBtn(ScreenManager.Game, new Point(400, 0), PlayerSelection.P2);
            p3s = new PlayerInControlBtn(ScreenManager.Game, new Point(600, 0), PlayerSelection.P3);
            gameObjects.Add(p1s);
            gameObjects.Add(p2s);
            gameObjects.Add(p3s);
            
            Player p1 = new Player(ScreenManager.Game, new Point(100, 100), gravity);
            Player p2 = new Player(ScreenManager.Game, new Point(100, 200), gravity);
            Player p3 = new Player(ScreenManager.Game, new Point(100, 300), gravity);
            currentPlayerIndex = 0;
            players.Add(p1);
            players.Add(p2);
            players.Add(p3);
            gameObjects.Add(p1);
            gameObjects.Add(p2);
            gameObjects.Add(p3);

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
            
            List<GestureSample> gestures = input.Gestures;
            foreach (GestureSample gs in input.Gestures)
            {
                if (gs.GestureType == GestureType.Tap)
                {
                    if (p1s.destinationBox.Contains((int)gs.Position.X, (int)gs.Position.Y))
                    {
                        currentPlayerIndex = 0;
                    }
                    else if (p2s.destinationBox.Contains((int)gs.Position.X, (int)gs.Position.Y))
                    {
                        currentPlayerIndex = 1;
                    }
                    else if (p3s.destinationBox.Contains((int)gs.Position.X, (int)gs.Position.Y))
                    {
                        currentPlayerIndex = 2;
                    }

                }
            }

            if (input.TouchState.Count == 0)
            {
                players[currentPlayerIndex].velocity = new Vector2(0, 0);
            }
            foreach (TouchLocation touch in input.TouchState)
            {
                //Vector2 position = gs.Position;
                if ((touch.State == TouchLocationState.Pressed
                        || touch.State == TouchLocationState.Moved))
                {
                    CheckForMovement(touch.Position);
                    break;
                }
                
            }

            // if the user pressed the back button, we return to the main menu
            PlayerIndex player;
            if (input.IsNewButtonPress(Buttons.Back, ControllingPlayer, out player))
            {
                LoadingScreen.Load(ScreenManager, false, ControllingPlayer, new BackgroundScreen(), new MainMenuScreen());
            }

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
            if (moveRightBtn.destinationBox.Contains((int)position.X, (int)position.Y))
            {
                players[currentPlayerIndex].velocity = new Vector2(0.2f, 0);
            }
            else if (moveLeftBtn.destinationBox.Contains((int)position.X, (int)position.Y))
            {
                players[currentPlayerIndex].velocity = new Vector2(-0.2f, 0);
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
                    if (gameObject.drawObject)
                    {
                        spriteBatch.Draw(
                        gameObject.objectSprite,
                        gameObject.destinationBox,
                        gameObject.sourceBox,
                        Color.White );
                    }
                }
                else
                {
                    Debug.WriteLine("GameObject {0} has not set it's objectSprite", gameObject.ToString());
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
