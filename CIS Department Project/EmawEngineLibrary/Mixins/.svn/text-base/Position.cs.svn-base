using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace EmawEngineLibrary.Mixins
{
    public class Position
    {
        public Position(Vector3 position, float facing)
        {
            Facing = facing;
            m_location = Matrix.CreateTranslation(position);
        }

        /// <summary>
        /// Moves the object by the Vector3 passed in relative to the current position.
        /// </summary>
        /// <param name="untransformedInput">The movement vecotr. Note that this is untransformed.
        /// This function will transform the movement by the object's orientation.</param>
        public void Move(Vector3 untransformedInput)
        {
            Vector3 move = Vector3.Transform(untransformedInput, Orientation);
            m_location *= Matrix.CreateTranslation(move);
        }

        /// <summary>
        /// Moves to the absolute location newPosition.
        /// </summary>
        /// <param name="newPosition"></param>
        public void MoveTo(Vector3 newPosition)
        {
            m_location = Matrix.CreateTranslation(newPosition);
        }

        /// <summary>
        /// Moves to the absolute location newPosition.
        /// </summary>
        /// <param name="newPosition"></param>
        /// <param name="orientation"></param>
        public void MoveTo(Vector3 newPosition, float orientation)
        {
            Facing = orientation;
            MoveTo(newPosition);
        }

        public Position Clone()
        {
            Vector3 trans = m_location.Translation;
            Position newPos = new Position(new Vector3(trans.X, trans.Y, trans.Z), m_facing);
            newPos.Normal = new Vector3(m_normal.X, m_normal.Y, m_normal.Z);
            return newPos;
        }

        public float Facing
        {
            get { return m_facing; }
            set
            {
                m_facing = value;

                //On an update to our facing direction, update our orientation matrix.
                m_orientation = Matrix.CreateRotationY(m_facing);

                m_orientation.Up = m_normal;

                m_orientation.Right = Vector3.Cross(m_orientation.Forward, m_orientation.Up);
                m_orientation.Right = Vector3.Normalize(m_orientation.Right);

                m_orientation.Forward = Vector3.Cross(m_orientation.Up, m_orientation.Right);
                m_orientation.Forward = Vector3.Normalize(m_orientation.Forward);
            }
        }

        public Vector3 Normal
        {
            get { return m_normal; }
            set
            {
                m_normal = value;

                m_orientation = Matrix.CreateRotationY(m_facing);

                m_orientation.Up = m_normal;

                m_orientation.Right = Vector3.Cross(m_orientation.Forward, m_orientation.Up);
                m_orientation.Right = Vector3.Normalize(m_orientation.Right);

                m_orientation.Forward = Vector3.Cross(m_orientation.Up, m_orientation.Right);
                m_orientation.Forward = Vector3.Normalize(m_orientation.Forward);
            }
        }

        public Matrix Orientation
        {
            get { return m_orientation; }
        }

        public Matrix Location
        {
            get { return m_location; }
            set { m_location = value; }
        } 
   
        public Matrix LocationFacing
        {
            get { return m_orientation * m_location; }
        }

        public Ray Ray
        {
            get
            {
                return new Ray(m_location.Translation, m_orientation.Backward);
            }
        }

        private float m_facing = 0.0f;
        private Matrix m_orientation;
        private Matrix m_location;
        private Vector3 m_normal = Vector3.Up;
    }
}
