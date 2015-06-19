using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmawEngineLibrary.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EmawEngineLibrary.Cameras
{
    /// <summary>
    /// A simple fly-through camera implementation using the direction and wasd keys, 
    /// or both joysticks on a gamepad.  Suitable for debugging
    /// </summary>
    public sealed class BasicCamera : Microsoft.Xna.Framework.GameComponent, ICamera
    {
        #region Fields and Members

        KeyboardState currentKeyboardState;
        GamePadState currentGamePadState;

        Vector3 cameraPosition = new Vector3(0, 50, 50);
        Vector3 cameraFront = new Vector3(0, 0, -1);


        Matrix view;


        Matrix projection;

        /// <summary>
        /// The View matrix 
        /// </summary>
        public Matrix View
        {
            get { return view; }
            set { view = value; }
        }

        /// <summary>
        /// The projection matrix
        /// </summary>
        public Matrix Projection
        {
            get { return projection; }
            set { projection = value; }
        }

        public Vector3 Position
        {
            get { return cameraPosition; }
            set { cameraPosition = value; }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Creates a BasicCamera with default position
        /// </summary>
        /// <param name="game">The game creating the camera</param>
        public BasicCamera(Game game) : this (new Vector3(0, 100, 0), game)
        {
        }

        /// <summary>
        /// Creates a BasicCamera with specified position
        /// </summary>
        /// <param name="position">The position of the camera</param>
        /// <param name="game">The game creating the camera</param>
        public BasicCamera(Vector3 position, Game game)
            : base(game)
        {
            cameraPosition = position;
        }

        public override void Initialize()
        {
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Game.GraphicsDevice.Viewport.AspectRatio, 1, 10000);
            base.Initialize();
        }
        /// <summary>
        /// Creates the camera's projection matrix
        /// </summary>
        public void LoadContent()
        {
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Game.GraphicsDevice.Viewport.AspectRatio, 1, 10000);
        }

        #endregion

        public void Focus(IPosition focus)
        {
            //throw new NotImplementedException("Basic camera cannot focus.");
        }

        # region Update

        /// <summary>
        /// Updates the camera based on user input
        /// </summary>
        /// <param name="gameTime">The current gametime</param>
        public override void Update(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(PlayerIndex.One);

            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // Check for input to rotate the camera.
            float pitch = -currentGamePadState.ThumbSticks.Right.Y * time * 0.001f;
            float turn = -currentGamePadState.ThumbSticks.Right.X * time * 0.001f;

            if (currentKeyboardState.IsKeyDown(Keys.Up))
                pitch += time * 0.001f;

            if (currentKeyboardState.IsKeyDown(Keys.Down))
                pitch -= time * 0.001f;

            if (currentKeyboardState.IsKeyDown(Keys.Left))
                turn += time * 0.001f;

            if (currentKeyboardState.IsKeyDown(Keys.Right))
                turn -= time * 0.001f;

            Vector3 cameraRight = Vector3.Cross(Vector3.Up, cameraFront);
            Vector3 flatFront = Vector3.Cross(cameraRight, Vector3.Up);

            Matrix pitchMatrix = Matrix.CreateFromAxisAngle(cameraRight, pitch);
            Matrix turnMatrix = Matrix.CreateFromAxisAngle(Vector3.Up, turn);

            Vector3 tiltedFront = Vector3.TransformNormal(cameraFront, pitchMatrix *
                                                          turnMatrix);

            // Check angle so we cant flip over
            if (Vector3.Dot(tiltedFront, flatFront) > 0.001f)
            {
                cameraFront = Vector3.Normalize(tiltedFront);
            }

            // Check for input to move the camera around.
            if (currentKeyboardState.IsKeyDown(Keys.W))
                cameraPosition += cameraFront * time * 0.1f;

            if (currentKeyboardState.IsKeyDown(Keys.S))
                cameraPosition -= cameraFront * time * 0.1f;

            if (currentKeyboardState.IsKeyDown(Keys.A))
                cameraPosition += cameraRight * time * 0.1f;

            if (currentKeyboardState.IsKeyDown(Keys.D))
                cameraPosition -= cameraRight * time * 0.1f;

            cameraPosition += cameraFront *
                              currentGamePadState.ThumbSticks.Left.Y * time * 0.1f;

            cameraPosition -= cameraRight *
                              currentGamePadState.ThumbSticks.Left.X * time * 0.1f;

            if (currentGamePadState.Buttons.RightStick == ButtonState.Pressed ||
                currentKeyboardState.IsKeyDown(Keys.R))
            {
                cameraPosition = new Vector3(0, 50, 50);
                cameraFront = new Vector3(0, 0, -1);
            }

            // Calculate the new veiw matrix
            view = Matrix.CreateLookAt(cameraPosition, cameraPosition + cameraFront, Vector3.Up);
        }

        #endregion
    }
}