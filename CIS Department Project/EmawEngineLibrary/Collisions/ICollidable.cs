using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmawEngineLibrary.Collisions;
using EmawEngineLibrary.Terrain;
using Microsoft.Xna.Framework;


namespace EmawEngineLibrary.Physics
{
    public interface ICollidable
    {
        /// <summary>
        /// Used for initial collision checks.
        /// Must not be null. 
        /// </summary>
        BoundingSphereWrapper BoundingSphere {get;}

        /// <summary>
        /// Used for secondary collision checks.
        /// If null, the result from the BoundingSphere is used.
        /// </summary>
        BoundingBoxWrapper BoundingBox { get; }

        /// <summary>
        /// Used for final collision detection.
        /// If null, results from the BoundingBox is used.
        /// </summary>
        BoundingOrientedBox OrientedBoundingBox { get; }

        /// <summary>
        /// Is called when something collides with this object
        /// </summary>
        /// <param name="collider">The object that collided with this one</param>
        /// <param name="ray">Unused currently. Once physics is implemented, allows for knockback.</param>
        void HandleCollision(ICollidable collider, Ray ray);

        void HandleTerrainCollision(ITerrain terrain);
    }
}
