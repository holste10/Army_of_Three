using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagement
{
    /// <summary>
    /// The Camera decides the part of the world that is shown on the phone screen.
    /// It uses the active player's reference to set camera's position. That way 
    /// The camera follows the player.
    /// </summary>
    /// 
    public class Camera
    {
        #region Fields
        // The final transformation matrix that holds final coordinates
        private Matrix _transform; 
        // Center the positioning according to player position.
        private Vector2 _centre;
        // Graphics viewport
        private Viewport _viewport;
        // Active player.
        private Character _activePlayer;
        #endregion

        #region  Properties
        public Matrix transform
        {
            get { return _transform; }
            set { _transform = transform; }
        }

        public Character activePlayer
        {
            get { return _activePlayer; }
            set { _activePlayer = value; }
        }
        #endregion

        /// <summary>
        /// Constructor. 
        /// </summary>
        /// <param name="viewport">The graphics viewport</param>
        public Camera(Viewport viewport)
        {
            this._viewport = viewport;
        }

        /// <summary>
        /// Repositioning of the camera according to player's position.
        /// Handles input.
        /// </summary>
        public void Update()
        {
            Input();

            // Calculate the "matrix" that center the camera.
            _centre = new Vector2(
                activePlayer.destinationBox.X + (activePlayer.destinationBox.Width / 2) 
                        - (_viewport.Width / 2),
                activePlayer.destinationBox.Y + (activePlayer.destinationBox.Height / 2)
                        - (_viewport.Height - _viewport.Height / 3));

            // Calculate new camera position.
            _transform = Matrix.CreateScale(new Vector3(1.0f, 1.0f, 0)) *
                         Matrix.CreateTranslation(-_centre.X, -_centre.Y, 0);
        }

        /// <summary>
        /// TODO: Implement zoom function. 
        /// </summary>
        public virtual void Input()
        {
            // Can add zoom function to the camera.
        }
    }
}
