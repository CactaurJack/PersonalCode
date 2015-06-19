using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmawEngineLibrary.Physics;
using Microsoft.Xna.Framework;

namespace EmawEngineLibrary.Cameras
{
    public class ThirdPersonCamera : GameComponent, ICamera
    {
        Vector3 cameraPosition = new Vector3(0, 50, 50);
        Vector3 cameraFront = new Vector3(0, 0, -1);

        /// <summary>
        /// The View matrix 
        /// </summary>
        public Matrix View
        {
            get { return view; }
            set { view = value; }
        }
        Matrix view;

        /// <summary>
        /// The projection matrix
        /// </summary>
        public Matrix Projection
        {
            get { return projection; }
            set { projection = value; }
        }
        Matrix projection;

        public Vector3 Position
        {
            get { return cameraPosition; }
            set { cameraPosition = value; }
        }

        private IPosition m_focus;

        /// <summary>
        /// Creates a BasicCamera with specified position
        /// </summary>
        /// <param name="position">The position of the camera</param>
        /// <param name="game">The game creating the camera</param>
        public ThirdPersonCamera(IPosition focus, Game game)
            : base(game)
        {
            m_focus = focus;
        }

        public ThirdPersonCamera(Game game) : base(game)
        {
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

        public void Focus(IPosition focus)
        {
            m_focus = focus;
        }



        /// <summary>
        /// Updates the camera based on user input
        /// </summary>
        /// <param name="gameTime">The current gametime</param>
        public override void Update(GameTime gameTime)
        {
            if (m_focus != null)
            {
                Matrix facing = Matrix.CreateRotationY(m_focus.Position.Facing);
                cameraPosition = m_focus.Position.Location.Translation + Vector3.Up*10 + facing.Forward*20;
                // Calculate the new veiw matrix
                view = Matrix.CreateLookAt(cameraPosition, m_focus.Position.Location.Translation, Vector3.Up);
            }
        }
    }


}
