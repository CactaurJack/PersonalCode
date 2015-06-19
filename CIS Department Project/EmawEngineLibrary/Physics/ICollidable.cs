using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace EmawEngineLibrary.Physics
{
    public interface ICollidable
    {
        //Todo: Wrap the bounding objects so we can actually use spheres too.
        BoundingBox BoundingBox { get; }

        /// <summary>
        /// Is called when something collides with this object
        /// </summary>
        /// <param name="collider">The object that collided with this one</param>
        /// <param name="ray">Unused currently. Once physics is implemented, allows for knockback.</param>
        void HandleCollision(ICollidable collider, Ray ray);
    }
}
