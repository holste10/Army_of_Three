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
        Vector2 gravity = new Vector2(0, 700.0f);

        private List<GameObject> gameObjects = new List<GameObject>();
        private List<GameObject> buttons = new List<GameObject>();
        private List<Character> players = new List<Character>();
        private int currentPlayerIndex;

        private PlayerInControlBtn p1s;
        private PlayerInControlBtn p2s;
        private PlayerInControlBtn p3s;

        private ClickableRectangle moveLeftBtn;
        private ClickableRectangle moveRightBtn;
        private ClickableRectangle jumpBtn;
        private ClickableRectangle abilityBtn;
        private float deltaTime;
        private Camera cam;
        private World world;

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

            world = new World(ScreenManager.Game);
            world.LoadContent();

            moveLeftBtn = new ClickableRectangle(ScreenManager.Game, new Rectangle(0, 320, 160, 160), buttonType.MOVE_LEFT);
            moveRightBtn = new ClickableRectangle(ScreenManager.Game, new Rectangle(640, 320, 160, 160), buttonType.MOVE_RIGHT);
            jumpBtn = new ClickableRectangle(ScreenManager.Game, new Rectangle(0, 159, 160, 160), buttonType.JUMP);
            abilityBtn = new ClickableRectangle(ScreenManager.Game, new Rectangle(640, 159, 160, 160), buttonType.SPECIAL_ATTACK);
            buttons.Add(moveLeftBtn);
            buttons.Add(moveRightBtn);
            buttons.Add(jumpBtn);
            buttons.Add(abilityBtn);
          
            p1s = new PlayerInControlBtn(ScreenManager.Game, new Point(200, 0), PlayerSelection.P1);
            p2s = new PlayerInControlBtn(ScreenManager.Game, new Point(400, 0), PlayerSelection.P2);
            p3s = new PlayerInControlBtn(ScreenManager.Game, new Point(600, 0), PlayerSelection.P3);
            buttons.Add(p1s);
            buttons.Add(p2s);
            buttons.Add(p3s);
            
            PlayerGreen p1 = new PlayerGreen(ScreenManager.Game, new Point(100, 100), gravity);
            PlayerBlue p2 = new PlayerBlue(ScreenManager.Game, new Point(100, 200), gravity);
            PlayerRed p3 = new PlayerRed(ScreenManager.Game, new Point(100, 300), gravity);
            currentPlayerIndex = 0;
            players.Add(p1);
            players.Add(p2);
            players.Add(p3);
            gameObjects.Add(p1);
            gameObjects.Add(p2);
            gameObjects.Add(p3);

            Tile tile = new Tile(ScreenManager.Game, new Point(100, 400));
            gameObjects.Add(tile);

            tile = new Tile(ScreenManager.Game, new Point(700, 300));
            gameObjects.Add(tile);

            tile = new Tile(ScreenManager.Game, new Point(100, 300));
            gameObjects.Add(tile);

            tile = new Tile(ScreenManager.Game, new Point(800, 300));
            gameObjects.Add(tile);

            cam = new Camera(ScreenManager.GraphicsDevice.Viewport);
            cam.activePlayer = players[currentPlayerIndex];

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.LoadContent();
            }

            foreach (GameObject button in buttons)
            {
                button.LoadContent();
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
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if (IsActive)
            {
                foreach (Character p in players)
                {
                    p.ApplyGravity(deltaTime);
                    float x = p.velocity.X;
                    float y = p.velocity.Y;
                    if ((p.position.Y + p.sourceBox.Height) >= world.background.Height - 280
                        && p.velocity.Y > 0)
                    {
                        y = 0;
                        p.position = new Vector2(p.position.X, world.background.Height - p.sourceBox.Height - 280);
                    }
                    p.velocity = new Vector2(x, y);
                }
                foreach (GameObject obj in gameObjects)
                {
                    obj.Update(deltaTime);
                }

                cam.activePlayer = players[currentPlayerIndex];
                cam.Update();
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
                    CheckForAbilitiesUse(gs.Position);
                }
            }

            if (input.TouchState.Count == 0)
            {
                foreach (Character c in players)
                {
                    c.velocity = new Vector2(0, c.velocity.Y);
                }
            }
            foreach (TouchLocation touch in input.TouchState)
            {
                if ((touch.State == TouchLocationState.Pressed
                        || touch.State == TouchLocationState.Moved))
                {
                    CheckForMovement(touch.Position);
                }


                //else if (touch.State == TouchLocationState.Released)
                //{
                //    if(players[currentPlayerIndex].velocity.X > 0)
                //        players[currentPlayerIndex].velocity = new Vector2(players[currentPlayerIndex].velocity.X - 200, 
                //               players[currentPlayerIndex].velocity.Y);
                //}
                //else
                //{
                //    players[currentPlayerIndex].velocity = new Vector2(players[currentPlayerIndex].velocity.X + 200, 
                //               players[currentPlayerIndex].velocity.Y);
                //}
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
            if (jumpBtn.destinationBox.Contains((int)position.X, (int)position.Y))
            {
                players[currentPlayerIndex].Jump();
            }
        }

        private void CheckForMovement(Vector2 position)
        {
            if (moveRightBtn.destinationBox.Contains((int)position.X, (int)position.Y))
            {
                players[currentPlayerIndex].velocity = new Vector2(200.0f, players[currentPlayerIndex].velocity.X);
            }
            if (moveLeftBtn.destinationBox.Contains((int)position.X, (int)position.Y))
            {
                players[currentPlayerIndex].velocity = new Vector2(-200.0f, players[currentPlayerIndex].velocity.Y);
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

            // Draw from camera tranformation!
            // Draw object according to world coordinates.
            spriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                null,
                null,
                null,
                null,
                cam.transform
            );

            spriteBatch.Draw(world.background, world.destinationBox, Color.White);

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

            // Draw's the object according to window coordinates.
            // Don't let camera fuck up the positioning of the object! 
            spriteBatch.Begin();
            foreach (GameObject button in buttons)
            {
                if (button.objectSprite != null)
                {
                    if (button.drawObject)
                    {
                        spriteBatch.Draw(
                            button.objectSprite,
                            button.destinationBox,
                            button.sourceBox,
                            Color.White
                        );
                    }
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
