using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmawEngineLibrary.Collisions;
using EmawEngineLibrary.Terrain;
using Microsoft.Xna.Framework;

namespace EmawEngineLibrary.Physics
{
    /// <summary>
    /// This class defines a collision box.
    /// Since this can be used for the world collision box, we defy
    /// normal conventions and we'll skip over the BoundingSphere when checking.
    /// </summary>
    public class CollisionBox : ICollidable
    {
        public CollisionBox(Vector3 min, Vector3 max)
        {
            m_boundingBox = new BoundingBoxWrapper(min, max);
        }

        private BoundingBoxWrapper m_boundingBox;

        public BoundingSphereWrapper BoundingSphere
        {
            get { throw new NotImplementedException("BoundingSphere accessed on a CollisionBox. CollisionBoxes only have BoundingBoxes defined. Please double check your usage."); }
        }

        public BoundingBoxWrapper BoundingBox
        {
            get { return m_boundingBox; }
        }

        public BoundingOrientedBox OrientedBoundingBox
        {
            get { return null; }
        }

        public void HandleCollision(ICollidable collider, Microsoft.Xna.Framework.Ray ray)
        {
        }

        public void HandleTerrainCollision(ITerrain terrain)
        {
        }
    }
}
