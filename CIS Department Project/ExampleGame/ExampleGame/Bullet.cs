using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmawEngineLibrary.Cameras;
using EmawEngineLibrary.Collisions;
using EmawEngineLibrary.Graphics.Primitives;
using EmawEngineLibrary.Messaging;
using EmawEngineLibrary.Physics;
using EmawEngineLibrary.Terrain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace ExampleGame
{
    class Bullet : DrawableGameComponent, ICollidable, IAudioEmitter
    {
        #region Fields

        private ICamera m_camera;
        private Vector3 m_velocity;
        private Matrix m_position;
        private PostMan m_postMan;
        private SpherePrimitive m_bullet;
        private BoundingSphereWrapper m_boundingSphere;
        private bool m_explode;
        public Tank FiredByTank { get; private set; }
        private SoundEffect bulletExplosionSoundEffect;
        private SoundEffectInstance bulletExplosionSoundEffectInstance;
        private AudioManager audioManager;

        private const float GRAVITY_STRENGTH = -0.5f;
        private const float MAX_FALL_SPEED = -30.0f;

        #endregion

        #region Methods

        public Bullet(Game game, Ray facing, float speed, Tank tank) : base(game) 
        { 
            m_position = Matrix.CreateTranslation(facing.Position);
            m_velocity = facing.Direction * speed;
            m_bullet = new SpherePrimitive(game.GraphicsDevice, 1, 12);
            m_postMan = new PostMan(game);
            m_boundingSphere = new BoundingSphereWrapper(m_position.Translation, .5f);

            ((ICollisionManager) Game.Services.GetService(typeof(ICollisionManager))).RegisterTerrainCollisionObject(this);
            m_camera = (ICamera) Game.Services.GetService(typeof (ICamera));

            FiredByTank = tank;

            audioManager = new AudioManager(game);
            game.Components.Add(audioManager);

            Game.Components.Add(this);
        }

        protected override void LoadContent()
        {
            bulletExplosionSoundEffect = Game.Content.Load<SoundEffect>("Sounds/BulletExplosion");
            bulletExplosionSoundEffectInstance = bulletExplosionSoundEffect.CreateInstance();

            base.LoadContent();
        }

        protected override void Dispose(bool disposing)
        {
            ((ICollisionManager)Game.Services.GetService(typeof(ICollisionManager))).UnregisterTerrainCollisionObject(this);
            Game.Components.Remove(this);
            base.Dispose(disposing);
        }

        public override void Update(GameTime gameTime)
        {
            if (m_explode)
            {
                m_explode = false;
                PositionVect = this.m_position.Translation;
                Forward = this.m_position.Forward;
                Up = this.m_position.Up;
                Velocity = Vector3.Zero;
                audioManager.Listener.Position = m_camera.View.Translation;
                audioManager.Listener.Forward = m_camera.View.Forward;
                audioManager.Listener.Up = m_camera.View.Up;
                audioManager.Listener.Velocity = Vector3.Zero;
                audioManager.Play3DSound("Sounds/BulletExplosion", false, this);
                new EXPLOSION(Game, m_position.Translation);
                Dispose(true);
            }
            m_velocity.Y += GRAVITY_STRENGTH;
            if (m_velocity.Y < MAX_FALL_SPEED) m_velocity.Y = MAX_FALL_SPEED;
            m_position.Translation += m_velocity * ((float) gameTime.ElapsedGameTime.Milliseconds / 1000);
            m_boundingSphere = new BoundingSphereWrapper(m_position.Translation, .5f);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            m_bullet.Draw(m_position, m_camera.View, m_camera.Projection, Color.Gray);
            base.Draw(gameTime);
        }

        protected override void UnloadContent()
        {
            ((ICollisionManager)Game.Services.GetService(typeof(ICollisionManager))).UnregisterCollisionObject(this);
        }

        public BoundingSphereWrapper BoundingSphere
        {
            get { return m_boundingSphere; }
        }

        public BoundingBoxWrapper BoundingBox
        {
            get { return null; }
        }

        public BoundingOrientedBox OrientedBoundingBox
        {
            get { return null; }
        }

        public void HandleCollision(ICollidable collider, Ray ray)
        {
            if (collider != null)
                m_postMan.SendMessage(MessageType.Damage, this, collider, 34);
            m_explode = true;
        }

        public void HandleTerrainCollision(ITerrain terrain)
        {
            m_explode = true;
        }

        #endregion

        #region IAudioEmitter Members

        public Vector3 PositionVect
        {
            get { return position; }
            set { position = value; }
        }
        Vector3 position;

        public Vector3 Forward
        {
            get { return forward; }
            set { forward = value; }
        }
        Vector3 forward;

        public Vector3 Up
        {
            get { return up; }
            set { up = value; }
        }
        Vector3 up;

        public Vector3 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        Vector3 velocity;

        #endregion
    }
}
