using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmawEngineLibrary.Collisions;
using EmawEngineLibrary.Terrain;
using Microsoft.Xna.Framework;
using EmawEngineLibrary.Physics;
using Microsoft.Xna.Framework.Graphics;
using EmawEngineLibrary.Cameras;
using EmawEngineLibrary.Mixins;
using EmawEngineLibrary.Messaging;

namespace ExampleGame
{
    class Tree : DrawableGameComponent, ICollidable
    {

        private float m_xCoordinate;
        private float m_zCoordinate;

        public Tree(Game game, float xCoordinate, float zCoordinate)
            : base(game)
        {
            m_xCoordinate = xCoordinate;
            m_zCoordinate = zCoordinate;
            Game.Components.Add(this);
        }

        protected override void LoadContent()
        {
            m_model = Game.Content.Load<Model>("Models/tree");

            // Because all tank instances in our game share the same 
            // model, we'll need our own "local" copy of the bone
            // transforms
            m_bones = new Matrix[m_model.Bones.Count];

            // We'll copy the "absoulute" bone transforms into our 
            // local bones array - "absolute" means each bone already
            // has its parent's trnasformations concatenated onto it
            m_model.CopyAbsoluteBoneTransformsTo(m_bones);
            
            //Init message passing.
            m_postMan = new PostMan(Game);

            ITerrain terrain = (ITerrain) Game.Services.GetService(typeof(ITerrain));


            Vector3 position = new Vector3(m_xCoordinate, terrain.GetHeight(m_xCoordinate, m_zCoordinate), m_zCoordinate);
            m_position = new Position(position, 0);

            Vector3 min = new Vector3(position.X - .25f, position.Y, position.Z - .25f);
            Vector3 max = new Vector3(position.X + .25f, position.Y + 5f, position.Z + .25f);
            m_boundingBox = new BoundingBox(min, max);

            m_boundingSphere = Microsoft.Xna.Framework.BoundingSphere.CreateFromBoundingBox(m_boundingBox);
                
            ((ICollisionManager)Game.Services.GetService(typeof(ICollisionManager))).RegisterCollisionObject(this);

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            Game.Components.Remove(this);
            ((ICollisionManager)Game.Services.GetService(typeof(ICollisionManager))).UnregisterCollisionObject(this);
            base.UnloadContent();
        }

        public BoundingSphereWrapper BoundingSphere
        {
            get { return m_boundingSphere; }
        }

        public BoundingBoxWrapper BoundingBox
        {
            get { return m_boundingBox; }
        }

        public BoundingOrientedBox OrientedBoundingBox
        {
            get { return null; }
        }

        public void HandleCollision(ICollidable collider, Ray ray)
        {
        }

        public void HandleTerrainCollision(ITerrain terrain)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            Matrix worldMatrix = m_position.Location;
            // Retrieve the current camera
            ICamera camera = Game.Services.GetService(typeof(ICamera)) as ICamera;

            m_model.CopyAbsoluteBoneTransformsTo(m_bones);

            // Draw the tank in our game world
            foreach (ModelMesh mesh in m_model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    // The world transform for our mesh is a
                    // combination of the bone transform and
                    // our world transform
                    effect.World = m_bones[mesh.ParentBone.Index] * worldMatrix;

                    // View and projection transforms come directly from our camera
                    effect.View = camera.View;
                    effect.Projection = camera.Projection;

                    // We'll turn on the default lighting for now
                    effect.EnableDefaultLighting();

                    //TODO: Add code to flash for damage, not collisions.
                    //effect.AmbientLightColor = new Vector3(1, 0, 0);
                }

                // Now we draw the mesh, using the effects that we just set
                mesh.Draw();


            }
            base.Draw(gameTime);
        }

        private PostMan m_postMan;
        private Position m_position;
        private Matrix[] m_bones;
        private Model m_model;
        private BoundingBox m_boundingBox;
        private BoundingSphere m_boundingSphere;
    }
}
