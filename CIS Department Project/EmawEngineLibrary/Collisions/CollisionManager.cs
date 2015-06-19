using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmawEngineLibrary.Collisions;
using EmawEngineLibrary.Messaging;
using EmawEngineLibrary.Terrain;
using Microsoft.Xna.Framework;
using EmawEngineLibrary.Logging;

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
            m_log = (ILog) Game.Services.GetService(typeof(ILog));

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

        public void ClearCollisionList()
        {
            m_collisionObjects.Clear();
            m_terrainCollisionObjects.Clear();
        }

        public override void  Update(GameTime gameTime)
        {
            if (WorldBox != null)
            {
                foreach (ICollidable col in m_collisionObjects)
                {
                    if (!WorldBox.BoundingBox.Intersects(col.BoundingSphere))
                    {
                        if (col.BoundingBox == null || !WorldBox.BoundingBox.Intersects(col.BoundingBox))
                        {
                            if (col.OrientedBoundingBox == null || !col.OrientedBoundingBox.Intersects(WorldBox.BoundingBox))
                            {
                                m_postMan.SendMessage(MessageType.Collision, WorldBox, col);
                            }
                        }
                    }
                }
            }


            for (int i = 0; i < m_collisionObjects.Count - 1; i++)
            {
                for (int j = i + 1; j < m_collisionObjects.Count; j++)
                {
                    ICollidable c1 = m_collisionObjects[i];
                    ICollidable c2 = m_collisionObjects[j];

                    IBoundingObject[] c1Objects = new IBoundingObject[] { c1.BoundingSphere, c1.BoundingBox, c1.OrientedBoundingBox };
                    IBoundingObject[] c2Objects = new IBoundingObject[] { c2.BoundingSphere, c2.BoundingBox, c2.OrientedBoundingBox };

                    bool collided = false;
                    for (int k = 0, l = 0, m = 0; m < c1Objects.Length; k++, l++, m++)
                    {
                        if (c1Objects[k] == null)
                            k--;
                        if (c2Objects[l] == null)
                            l--;
                        if (c1Objects[k] != null && c2Objects[l] != null)
                        {
                            switch (l)
                            {
                                case 0:
                                    collided = c1Objects[k].Intersects(c2Objects[l] as BoundingSphereWrapper);
                                    break;
                                case 1:
                                    collided = c1Objects[k].Intersects(c2Objects[l] as BoundingBoxWrapper);
                                    break;
                                case 2:
                                    collided = c1Objects[k].Intersects(c2Objects[l] as BoundingOrientedBox);
                                    break;
                            }

                        }
                        if (!collided)
                            break;
                    }
                    if (collided)
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
                    bool collided = false;
                    if (col.BoundingSphere != null && col.BoundingSphere.Center.Y - col.BoundingSphere.Radius <= terrain.GetHeight(col.BoundingSphere.Center.X, col.BoundingSphere.Center.Z))
                    {
                        if (col.BoundingBox == null)
                        {
                            m_postMan.SendMessage(MessageType.Collision, terrain, col);
                            continue;
                        }
                        foreach (Vector3 corner in col.BoundingBox.GetCorners())
                        {
                            if (terrain.GetHeight(corner.X, corner.Z) >= corner.Y)
                            {
                                if (col.OrientedBoundingBox == null)
                                {
                                    m_postMan.SendMessage(MessageType.Collision, terrain, col);
                                    break;
                                }
                                foreach (Vector3 oCorner in col.OrientedBoundingBox.GetCorners())
                                {
                                    if (terrain.GetHeight(oCorner.X, oCorner.Z) >= oCorner.Y)
                                    {
                                        m_postMan.SendMessage(MessageType.Collision, terrain, col);
                                        collided = true;
                                        break;
                                    }
                                }
                                if (collided)
                                    break;
                            }
                        }
                    }
                }
            }

            base.Update(gameTime);
        }


        public ICollidable WorldBox { get; set; }
        private List<ICollidable> m_collisionObjects;
        private List<ICollidable> m_terrainCollisionObjects;
        private PostMan m_postMan;
        private ILog m_log;
    }
}
