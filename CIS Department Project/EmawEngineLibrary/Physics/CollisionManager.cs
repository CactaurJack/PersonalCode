using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmawEngineLibrary.Messaging;
using EmawEngineLibrary.Terrain;
using Microsoft.Xna.Framework;

namespace EmawEngineLibrary.Physics
{
    public class CollisionManager : GameComponent, ICollisionManager
    {
        /// <summary>
        /// Creates a CollisionManager and registers it as a game service.
        /// </summary>
        /// <param name="game"></param>
        public CollisionManager(Game game) : base(game)
        {
            m_postMan = new PostMan(game);
            m_collisionObjects = new List<ICollidable>();
            m_terrainCollisionObjects = new List<ICollidable>();
            Game.Components.Add(this);
            Game.Services.AddService(typeof(ICollisionManager), this);
        }

        public void RegisterCollisionObject(ICollidable collisionObject)
        {
            m_collisionObjects.Add(collisionObject);
        }

        public void UnregisterCollisionObject(ICollidable collisionObject)
        {
            m_collisionObjects.Remove(collisionObject);
        }

        /// <summary>
        /// Registers an ICollidable with the manager as something that cares about colliding with terrain.
        /// Also registers for normal collisions.
        /// </summary>
        /// <param name="collisionObject"></param>
        public void RegisterTerrainCollisionObject(ICollidable collisionObject)
        {
            m_terrainCollisionObjects.Add(collisionObject);
            RegisterCollisionObject(collisionObject);
        }

        public void UnregisterTerrainCollisionObject(ICollidable collisionObject)
        {
            m_terrainCollisionObjects.Remove(collisionObject);
            UnregisterCollisionObject(collisionObject);
        }

        public override void Update(GameTime gameTime)
        {
            if (WorldBox != null)
            {
                foreach (ICollidable col in m_collisionObjects)
                {
                    if (!WorldBox.BoundingBox.Intersects(col.BoundingBox))
                        m_postMan.SendMessage(MessageType.Collision, WorldBox, col);
                }
            }

            //TODO:Add some spatial organization to the objects so we don't just brute force it.
            for (int i = 0; i < m_collisionObjects.Count - 1; i++)
            {
                for (int j = i + 1; j < m_collisionObjects.Count; j++)
                {
                    ICollidable c1 = m_collisionObjects[i];
                    ICollidable c2 = m_collisionObjects[j];

                    //If we have a collision, send out messages to each object from the other,
                    //and let them react to it.
                    if (c1.BoundingBox.Intersects(c2.BoundingBox))
                    {
                        m_postMan.SendMessage(MessageType.Collision, c1, c2);
                        m_postMan.SendMessage(MessageType.Collision, c2, c1);
                    }
                }
            }

            ITerrain terrain = (ITerrain) Game.Services.GetService(typeof (ITerrain));

            if (terrain != null)
            {
                foreach (ICollidable col in m_terrainCollisionObjects)
                {
                    foreach (Vector3 corner in col.BoundingBox.GetCorners())
                    {
                        if (terrain.GetHeight(corner.X, corner.Z) >= corner.Y)
                            m_postMan.SendMessage(MessageType.Collision, null, col);
                    }
                }
            }

            base.Update(gameTime);
        }


        public ICollidable WorldBox { get; set; }
        private List<ICollidable> m_collisionObjects;
        private List<ICollidable> m_terrainCollisionObjects;
        private PostMan m_postMan;
    }
}
