using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace EmawEngineLibrary.Physics
{
    public class CollisionBox : ICollidable
    {
        public CollisionBox(Vector3 min, Vector3 max)
        {
            m_boundingBox = new BoundingBox(min, max);
        }

        private BoundingBox m_boundingBox;
        public BoundingBox BoundingBox
        {
            get { return m_boundingBox; }
        }

        public void HandleCollision(ICollidable collider, Microsoft.Xna.Framework.Ray ray)
        {
        }
    }
}
