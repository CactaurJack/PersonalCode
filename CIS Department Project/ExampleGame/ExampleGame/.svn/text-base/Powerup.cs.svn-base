using System;
using System.Collections.Generic;
using EmawEngineLibrary.Collisions;
using EmawEngineLibrary.Mixins;
using EmawEngineLibrary.Input;
using EmawEngineLibrary.Messaging;
using EmawEngineLibrary.Physics;
using EmawEngineLibrary.Terrain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using EmawEngineLibrary;
using EmawEngineLibrary.Graphics;
using EmawEngineLibrary.Graphics.Particles;
using EmawEngineLibrary.Graphics.Primitives;
using EmawEngineLibrary.Cameras;
using EmawEngineLibrary.Graphics.Billboards;

namespace ExampleGame
{   
    public enum PowerupType
    {
        HEALTH,
        AMMO
    }

    public class Powerup : DrawableGameComponent, ICollidable
    {
        /// <summary>
        /// The number of possible powerups.
        /// KEEP THIS UP TO DATE!
        /// </summary>
        private const int POWERUP_COUNT = 2;

        private PowerupType m_powerupType;
        private int m_powerupAmount;
        private BoundingBoxWrapper m_collision;
        private bool m_pickedUp;

        private Billboard m_billboard;
        private Texture2D m_texture;
        private Vector3 m_position;
        public Powerup(Game game, PowerupType type, Vector3 position) : base(game)
        {
            m_powerupType = type;
            m_position = position;

            m_postMan = new PostMan(game);
            UpdateBoundingBox();
            ((ICollisionManager)Game.Services.GetService(typeof(ICollisionManager))).RegisterCollisionObject(this);

            game.Components.Add(this);
        }

        /// <summary>
        /// Creates a new powerup of a random type.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="position"></param>
        public Powerup(Game game, Vector3 position) : base(game)
        {
            m_powerupType = (PowerupType)Math.Abs((Convert.ToInt32(position.Z)%POWERUP_COUNT));

            m_position = position;

            m_postMan = new PostMan(game);
            UpdateBoundingBox();
            ((ICollisionManager)Game.Services.GetService(typeof(ICollisionManager))).RegisterCollisionObject(this);

            game.Components.Add(this);
        }

        protected override void LoadContent()
        {
            switch (m_powerupType)
            {
                case (PowerupType.HEALTH):
                    m_texture = Game.Content.Load<Texture2D>("HealthPowerup");
                    m_powerupAmount = 50;
                    break;
                case (PowerupType.AMMO):
                    m_texture = Game.Content.Load<Texture2D>("AmmoPowerup");
                    m_powerupAmount = 5;
                    break;
                default:
                    throw new NotImplementedException();
            }


            m_billboard = new Billboard(Game, m_texture, m_position, new Vector2(HEIGHT, WIDTH), Color.White);
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            Game.Components.Remove(this);
            ((ICollisionManager)Game.Services.GetService(typeof(ICollisionManager))).UnregisterCollisionObject(this);
            base.UnloadContent();
        }

        /// <summary>
        /// Draws the billboard
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            m_billboard.Draw(gameTime);
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (m_pickedUp)
                Dispose();
            UpdateBoundingBox();
            base.Update(gameTime);
        }



        public void UpdateBoundingBox()
        {
            Vector3 min = new Vector3(m_position.X - (HEIGHT / 2), m_position.Y + .01f, m_position.Z - (WIDTH / 2));
            Vector3 max = new Vector3(m_position.X + (HEIGHT / 2), m_position.Y - .01f, m_position.Z + (WIDTH / 2));

            m_collision = new BoundingBoxWrapper(min, max);

            m_boundingSphere = Microsoft.Xna.Framework.BoundingSphere.CreateFromBoundingBox(m_collision);
        }

        public BoundingSphereWrapper BoundingSphere
        {
            get { return m_boundingSphere; }
        }

        public BoundingBoxWrapper BoundingBox
        {
            get { return m_collision; }
        }

        public BoundingOrientedBox OrientedBoundingBox
        {
            get { return null; }
        }

        public void HandleCollision(ICollidable collider, Ray ray)
        {
            switch (m_powerupType)
            {
                case PowerupType.HEALTH:
                    m_postMan.SendMessage(MessageType.Heal, this, collider, m_powerupAmount);
                    break;
                case PowerupType.AMMO:
                    m_postMan.SendMessage(MessageType.Ammo, this, collider, m_powerupAmount);
                    break;
            }
            m_pickedUp = true;
        }

        public void HandleTerrainCollision(ITerrain terrain)
        {
            
        }

        private PostMan m_postMan;
        private BoundingSphere m_boundingSphere;

        private const float HEIGHT = 4;
        private const float WIDTH = 4;
    }
}
