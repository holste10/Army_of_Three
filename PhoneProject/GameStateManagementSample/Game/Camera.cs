using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagement
{
    public class Camera
    {
        #region Fields
        private Matrix _transform;
        private Vector2 _centre;
        private Viewport _viewport;
        private Player _activePlayer;
        #endregion

        #region  Properties
        public Matrix transform
        {
            get { return _transform; }
            set { _transform = transform; }
        }

        public Player activePlayer
        {
            get { return _activePlayer; }
            set { _activePlayer = value; }
        }
        #endregion

        public Camera(Viewport viewport)
        {
            this._viewport = viewport;
        }

        public void Update()
        {
            Input();

            _centre = new Vector2(
                activePlayer.destinationBox.X + (activePlayer.destinationBox.Width / 2) - 400,
                activePlayer.destinationBox.Y + (activePlayer.destinationBox.Height / 2) - 240);

            _transform = Matrix.CreateScale(new Vector3(1.0f, 1.0f, 0)) *
                         Matrix.CreateTranslation(-_centre.X, -_centre.Y, 0);
        }

        public virtual void Input()
        {
            // Can add zoom function to the camera.
        }
    }
}
