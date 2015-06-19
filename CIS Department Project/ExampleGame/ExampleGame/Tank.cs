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

    /// <summary>
    /// This class represents a simple tank in the game world.
    /// </summary>
    class Tank : DrawableGameComponent, ICollidable, IPosition, IAudioEmitter
    {
        #region Fields

        Vector3 l_Exhaust_Offset = new Vector3(2.3f, 3, -3);
        Vector3 r_Exhaust_Offset = new Vector3(-2.3f, 3, -3);
        Vector3 l_Dust_Offset = new Vector3(2, .2f, -1.5f);
        Vector3 r_Dust_Offset = new Vector3(-2, .2f, -1.5f);

        private DebugDraw m_debugDraw;

        // The model we'll use to represent our tank
        Model model;

        // The array of bones corresponding to our model
        Matrix[] bones;

        string name = String.Empty;

        GameMode GameType;
        string GameTypeString = "";
        SpriteBatch m_spriteBatch;
        SpriteFont m_spriteFont;
        BasicEffect m_basicEffect;

        Vector3 lightingEffect = Vector3.Zero;

        public TankInput Input { get; set; }

        private BoundingBox m_boundingBox;

        public BoundingSphereWrapper BoundingSphere
        {
            get { return m_boundingSphere; }
        }

        public BoundingBoxWrapper BoundingBox {get{return m_boundingBox;}}

        private BoundingOrientedBox m_orientedBoundingBox;
        private BoundingOrientedBox m_oldOrientedBoundingBox;
        public BoundingOrientedBox OrientedBoundingBox
        {
            get { return m_orientedBoundingBox; }
        }

        public void HandleCollision(ICollidable collider, Ray ray)
        {
            //If we collided, then move back to the last position.
            //Will slow us down if it's a bullet that's hit, but it works.
            m_position = m_oldPosition.Clone();
            m_boundingBox = m_oldBoundingBox;

            m_postMan.SendMessage(MessageType.Damage, this, collider, 1);
        }

        public void HandleTerrainCollision(ITerrain terrain)
        {
        }

        private ITerrain m_terrain;

        private Position m_position;
        public Position Position { get { return m_position; } set { m_position = value; } }
        public float Facing { get { return m_position.Facing; } }

        private Meter m_healthMeter;
        private const int m_maxHealth = 100;
        public double PercentHP { get { return m_healthMeter.CurrentPercent; } }
        public int CurrentHP
        {
            get
            {
                return m_healthMeter.CurrentValue;
            }
            set
            {
                int oldValue = m_healthMeter.CurrentValue;
                m_healthMeter.CurrentValue = value;
                if (oldValue <= 0 && m_healthMeter .CurrentValue > 0)
                    ((ICollisionManager)Game.Services.GetService(typeof(ICollisionManager))).RegisterCollisionObject(this);
            }
        }

        private Meter m_ammoMeter;
        private const int m_maxAmmo = 20;
        public int CurrentAmmo { get { return m_ammoMeter.CurrentValue; } }
        private TimeSpan m_timeOfLastShot;

        private PostMan m_postMan;
        private EXPLOSION boom;

        Matrix leftWheelRollMatrix = Matrix.Identity;
        Matrix rightWheelRollMatrix = Matrix.Identity;

        public float LeftWheelRollFloat { get; set; }  //These are to pass over the network so that non-local tanks have spinning wheels too
        public float RightWheelRollFloat{ get; set; }

        ModelBone leftBackWheelBone;
        ModelBone rightBackWheelBone;
        ModelBone leftFrontWheelBone;
        ModelBone rightFrontWheelBone;

        Matrix leftBackWheelTransform;
        Matrix rightBackWheelTransform;
        Matrix leftFrontWheelTransform;
        Matrix rightFrontWheelTransform;

        const float TankWheelRadius = 3;

        private const float GRAVITY_EXPONENT = 1.0f;
        private const float SLIDE_THRESHOLD = 0.2f;
        private const float SLIDE_STRENGTH = 5.0f;

        private float current_speed;
        private const float MAX_SPEED = 1.0f;
        private const float MIN_SPEED = -1.0f;
        private const float ACCELERATION = 0.25f;
        private const float DECELERATION = -0.1f;
        private const float MOVEMENT_PER_SECOND = 10.0f;
        private const float SHOT_RECOIL = -1.5f;

        ModelBone turretBone;
        ModelBone cannonBone;

        Matrix turretTransform;
        Matrix cannonTransform;

        public float TurretPitch { get;  set; }
        public float TurretYaw { get;  set; }

        private ExhaustTrail m_exhaustTrailRight;
        private ExhaustTrail m_exhaustTrailLeft;

        private DustTrail m_dustTrailRight;
        private DustTrail m_dustTrailLeft;

        SoundEffect tankEngineIdleSoundEffect;
        SoundEffect tankCannonSoundEffect;
        SoundEffect tankTurretTurningSoundEffect;
        SoundEffectInstance tankTurretTurningSoundEffectInstance;
        SoundEffectInstance tankEngineIdleSoundEffectInstance;

        public Player _Player { get; set; }
        public Random _Random { get; set;}

        #endregion

        #region Initialization

        /// <summary>
        /// Constructs a new instance of a tank
        /// </summary>
        /// <param name="game">the game instance this tank belongs to</param>
        /*public Tank(Game game)
            : base(game)
        { }*/

        public Tank(Game game, Gamer gamer)
            : base(game)
        {
            name = gamer.Gamertag;
        }

        //New constructors to handle a Game Type int -gw
        public Tank(Game game, Gamer gamer, GameMode _GameType)
            : base(game)
        {
            name = gamer.Gamertag;
            GameType = _GameType;
            switch (GameType)
            {
                case GameMode.SINGLE:
                    GameTypeString = "Free Roam";
                    break;
                case GameMode.FFA:
                    GameTypeString = "Free For All";
                    break;
                case GameMode.CAPTURETHEFLAG:
                    GameTypeString = "Capture The Flag";
                    break;
            }
        }

        public Tank(Game game, GameMode _GameType)
            : base(game)
        {
            GameType = _GameType;
            switch (GameType)
            {
                case GameMode.SINGLE:
                    GameTypeString = "Free Roam";
                    break;
                case GameMode.FFA:
                    GameTypeString = "Free For All";
                    break;
                case GameMode.CAPTURETHEFLAG:
                    GameTypeString = "Capture The Flag";
                    break;
            }
        }

        protected override void  LoadContent()
        {
            // Load the tank model through the game's content manager
            // Using the game's content manager ensures that all of 
            // our tank instances will be using the same model instance 
            // - the content manager will only load the .xnb file once,
            // and return a reference to that model in subsequent calls
            model = Game.Content.Load<Model>("models/tank");

            // Because all tank instances in our game share the same 
            // model, we'll need our own "local" copy of the bone
            // transforms
            bones = new Matrix[model.Bones.Count];

            // We'll copy the "absoulute" bone transforms into our 
            // local bones array - "absolute" means each bone already
            // has its parent's trnasformations concatenated onto it
            model.CopyAbsoluteBoneTransformsTo(bones);
            
            //Init message passing.
            m_postMan = new PostMan(Game);

            m_basicEffect = new BasicEffect(GraphicsDevice) { TextureEnabled = true, VertexColorEnabled = true };
            m_spriteBatch = new SpriteBatch(GraphicsDevice);
            m_spriteFont = Game.Content.Load<SpriteFont>("DemoFont");

            m_healthMeter = new Meter(m_maxHealth);
            m_ammoMeter = new Meter(m_maxAmmo);
            m_timeOfLastShot = TimeSpan.FromSeconds(0);

            leftBackWheelBone = model.Bones["l_back_wheel_geo"];
            rightBackWheelBone = model.Bones["r_back_wheel_geo"];
            leftFrontWheelBone = model.Bones["l_front_wheel_geo"];
            rightFrontWheelBone = model.Bones["r_front_wheel_geo"];

            turretBone = model.Bones["turret_geo"];
            cannonBone = model.Bones["canon_geo"];

            leftBackWheelTransform = leftBackWheelBone.Transform;
            rightBackWheelTransform = rightBackWheelBone.Transform;
            leftFrontWheelTransform = leftFrontWheelBone.Transform;
            rightFrontWheelTransform = rightFrontWheelBone.Transform;

            LeftWheelRollFloat = 0;
            RightWheelRollFloat = 0;

            turretTransform = turretBone.Transform;
            cannonTransform = cannonBone.Transform;

            TurretPitch = 0;
            TurretYaw = 0;

            current_speed = 0f;

            m_exhaustTrailRight = new ExhaustTrail(this.Game, new Vector3(-2,3.5f,-3.5f));
            m_exhaustTrailLeft = new ExhaustTrail(this.Game, new Vector3(2, 3.5f , -3.5f));
            m_exhaustTrailLeft.Initialize();
            m_exhaustTrailRight.Initialize();

            m_dustTrailRight = new DustTrail(this.Game, new Vector3(-2, 0, -3f));
            m_dustTrailLeft = new DustTrail(Game, new Vector3(2, 0, -3f));
            m_dustTrailRight.Initialize();
            m_dustTrailLeft.Initialize();

            ((ICollisionManager)Game.Services.GetService(typeof(ICollisionManager))).RegisterCollisionObject(this);

            // We want our tank to run around the center of the grid at 
            // a constant radius, so let's set that now
            m_position = new Position(new Vector3(_Random.Next(-200, 200), 10, _Random.Next(-200, 200)), 0);
            m_oldPosition = m_position.Clone();

            // Load in sounds
            tankEngineIdleSoundEffect = Game.Content.Load<SoundEffect>("Sounds/TankEngineIdle");
            tankCannonSoundEffect = Game.Content.Load<SoundEffect>("Sounds/TankFire");
            tankTurretTurningSoundEffect = Game.Content.Load<SoundEffect>("Sounds/TankTurretTurning");

            // Create sound effect instance
            tankEngineIdleSoundEffectInstance = tankEngineIdleSoundEffect.CreateInstance();
            tankTurretTurningSoundEffectInstance = tankTurretTurningSoundEffect.CreateInstance();

            tankEngineIdleSoundEffectInstance.IsLooped = true;
            tankTurretTurningSoundEffectInstance.IsLooped = true;


            //If we're in a debug build, show the tank's bounding box.
#if DEBUG
            m_debugDraw = new DebugDraw(GraphicsDevice);
#endif

            UpdateBoundingBox();

 	        base.LoadContent();
        }

        protected override void UnloadContent()
        {
            m_dustTrailLeft.Dispose();
            m_dustTrailRight.Dispose();
            m_exhaustTrailLeft.Dispose();
            m_exhaustTrailRight.Dispose();
            ((ICollisionManager)Game.Services.GetService(typeof(ICollisionManager))).UnregisterCollisionObject(this);

            tankEngineIdleSoundEffectInstance.Dispose();
            tankTurretTurningSoundEffectInstance.Dispose();

            base.UnloadContent();
        }


        #endregion

        #region Update and Draw

        private Position m_oldPosition;
        private BoundingBox m_oldBoundingBox;
        private bool m_explode;
        private BoundingSphere m_boundingSphere;

        public void Update(GameTime gameTime, Team team)
        {
            m_oldPosition = m_position.Clone();

            if (m_explode)
            {
                Vector3 height = new Vector3(0, 2, 0);
                Position currentPosition = m_position.Clone();
                Game game = Game;
                //Dispose();
                new EXPLOSION(Game, currentPosition.Location.Translation + height);
                m_explode = false;
                return;
            }

            //Recoil Added @ Revision 309 unknown author -gw
            if (Input.Shoot)
            {
                FireShot(gameTime);
                //Vector3 recoil = new Vector3(0f, -25f, 0f);
                //m_position.Move(recoil);

            }

            TurretPitch += Input.TurretUpDown;
            TurretPitch = TurretPitch < 0 ? TurretPitch : 0; //Make sure the turret doesn't go too low and point into the rest of the tank
            TurretPitch = TurretPitch > -MathHelper.Pi / 3 ? TurretPitch : -MathHelper.Pi / 3;  //Make sure the turret doesn't go too high

            TurretYaw += Input.TurretLeftRight;

            if (Input.TurretUpDown != 0)
            {
                tankTurretTurningSoundEffectInstance.Pitch = 0;
                tankTurretTurningSoundEffectInstance.Volume = .3f;
                tankTurretTurningSoundEffectInstance.Play();
            }
            else if (Input.TurretLeftRight != 0)
            {
                tankTurretTurningSoundEffectInstance.Pitch = .1f;
                tankTurretTurningSoundEffectInstance.Volume = .3f;
                tankTurretTurningSoundEffectInstance.Play();
            }
            else
            {
                tankTurretTurningSoundEffectInstance.Stop();
            }


            m_position.Facing += Input.LeftRight;

            // New movement code here.

            // Movement is now per second as opposed to per CPU cycle.
            float movetime = MOVEMENT_PER_SECOND * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // If there is no forward/backward input, the tank will decelerate to 0 movement.
            if (Input.ForwardBackward == 0)
            {
                if (current_speed > 0)
                {
                    current_speed += DECELERATION;
                    if (current_speed < 0) current_speed = 0;
                }
                else if (current_speed < 0)
                {
                    current_speed -= DECELERATION;
                    if (current_speed > 0) current_speed = 0;
                }
            }
            // Otherwise it accelerates up to its maximum forward or back speed.
            else
            {
                current_speed += Input.ForwardBackward * ACCELERATION;
                if (current_speed > MAX_SPEED) current_speed -= ACCELERATION;
                else if (current_speed < MIN_SPEED) current_speed += ACCELERATION;
            }

            // Gravity is based on the normal vector. Degree represents how much the tank is tilted on the Z axis
            // and is calculated based on the cosine of the angle between its normal vector and a forward vector
            // rotated by its Y axis rotation. -1 is pointed straight down, 0 is straight forward, and 1 is straight
            // up.
            float gravity = m_position.Normal.Y;
            Vector3 rotated = Vector3.TransformNormal(Vector3.Forward, Matrix.CreateRotationY(m_position.Facing));
            float degree = Vector3.Dot(m_position.Normal, rotated);

            float grav_modifier;

            // The gravity modifier changes the speed of the tank based on the degree. Gravity is negative, so a
            // negative degree will increase the speed and a positive degree will decrease it. The effect is
            // reversed if the tank is moving backwards.
            if (Input.ForwardBackward < 0)
                grav_modifier = (float)Math.Pow((double)gravity, (double)(-degree * GRAVITY_EXPONENT));
            else
                grav_modifier = (float)Math.Pow((double)gravity, (double)(degree * GRAVITY_EXPONENT));

            float total_speed = current_speed * grav_modifier;

            // Slide is a constant modification to speed on steep slopes. A degree above the slide threshold
            // will reduce the speed, and a degree below the negative slide threshold will increase it.
            float slide = 0;
            if (degree > SLIDE_THRESHOLD)
            {
                slide = (degree - SLIDE_THRESHOLD) * SLIDE_STRENGTH;
                total_speed -= (slide * movetime);
            }
            else if (degree < -SLIDE_THRESHOLD)
            {
                slide = (degree + SLIDE_THRESHOLD) * SLIDE_STRENGTH;
                total_speed -= (slide * movetime);
            }

            Vector3 movement = Vector3.UnitZ * (total_speed * movetime);
            Vector3 location = m_position.Location.Translation;

            if (movement == Vector3.Zero) tankEngineIdleSoundEffectInstance.Play();

            m_terrain = (ITerrain)Game.Services.GetService(typeof(ITerrain));
            if (m_terrain != null)
            {
                movement.Y = m_terrain.GetHeight(location.X, location.Z);
                movement.Y -= location.Y;
            }

            m_position.Move(movement);

            if (m_terrain != null)
                m_position.Normal = m_terrain.GetNormal(location.X, location.Z);

            UpdateBoundingBox();

            float leftWheelSpeed = (total_speed/5.0f) - Input.LeftRight * 3;
            float rightWheelSpeed = (total_speed/5.0f) + Input.LeftRight * 3;

            LeftWheelRollFloat += leftWheelSpeed / TankWheelRadius;
            leftWheelRollMatrix *= Matrix.CreateRotationX(leftWheelSpeed / TankWheelRadius);

            RightWheelRollFloat += rightWheelSpeed / TankWheelRadius;
            rightWheelRollMatrix *= Matrix.CreateRotationX(rightWheelSpeed / TankWheelRadius);

            int leftDustFrequency = (int)(1000f * Math.Abs(leftWheelSpeed));
            if (leftDustFrequency > 2000) leftDustFrequency = 2000;
            int rightDustFrequency = (int)(1000f * Math.Abs(rightWheelSpeed));
            if (rightDustFrequency > 2000) rightDustFrequency = 2000;
            m_dustTrailLeft.SetParticleFrequency(leftDustFrequency);
            m_dustTrailRight.SetParticleFrequency(rightDustFrequency);

            if (Input.ForwardBackward != 0)
            {
                m_exhaustTrailLeft.SetParticleFrequency(40);
                m_exhaustTrailRight.SetParticleFrequency(40);
                tankEngineIdleSoundEffectInstance.Pitch = .3f;
                tankEngineIdleSoundEffectInstance.Volume = .8f;
                tankEngineIdleSoundEffectInstance.Play();

            }
            else if (Input.LeftRight != 0)
            {
                m_exhaustTrailLeft.SetParticleFrequency(20);
                m_exhaustTrailRight.SetParticleFrequency(20);
                tankEngineIdleSoundEffectInstance.Pitch = .2f;
                tankEngineIdleSoundEffectInstance.Volume = .8f;
                tankEngineIdleSoundEffectInstance.Play();
            }
            else
            {
                m_exhaustTrailLeft.SetParticleFrequency(5);
                m_exhaustTrailRight.SetParticleFrequency(5);
                tankEngineIdleSoundEffectInstance.Pitch = 0;
                tankEngineIdleSoundEffectInstance.Volume = .7f;
                tankEngineIdleSoundEffectInstance.Play();
            }

//            m_exhaustTrailRight.Update(gameTime, m_position.Location, m_position.Facing);
//            m_exhaustTrailLeft.Update(gameTime, m_position.Location, m_position.Facing);
//            m_dustTrailRight.Update(gameTime, m_position.Location, m_position.Facing);
//            m_dustTrailLeft.Update(gameTime, m_position.Location, m_position.Facing);

            m_exhaustTrailRight.Update(gameTime, Vector3.Transform(r_Exhaust_Offset, m_position.Orientation) + m_position.Location.Translation);
            m_exhaustTrailLeft.Update(gameTime, Vector3.Transform(l_Exhaust_Offset, m_position.Orientation) + m_position.Location.Translation);
            m_dustTrailRight.Update(gameTime, Vector3.Transform(r_Dust_Offset, m_position.Orientation) + m_position.Location.Translation);
            m_dustTrailLeft.Update(gameTime, Vector3.Transform(l_Dust_Offset, m_position.Orientation) + m_position.Location.Translation);

            switch (team)
            {
                case Team.RED:
                    lightingEffect = new Vector3(3,0,0);
                    break;
                case Team.BLUE:
                    lightingEffect = new Vector3(0,0,3);
                    break;
                case Team.NONE:
                    lightingEffect = Vector3.Zero;
                    break;
            }

            base.Update(gameTime);
        }

        public void Update(GameTime gameTime, Matrix position, float facing, bool firing, Team team)
        {
            m_oldPosition = m_position.Clone();
            m_position.Facing = facing;
            m_position.Location = position;

            Vector3 location = m_position.Location.Translation;

            m_terrain = (ITerrain)Game.Services.GetService(typeof(ITerrain));

            m_position.Normal = m_terrain.GetNormal(location.X, location.Z);

            if (m_explode)
            {
                Vector3 height = new Vector3(0, 2, 0);
                Position currentPosition = m_position.Clone();
                Game game = Game;
                //Dispose();
                new EXPLOSION(Game, currentPosition.Location.Translation + height);
                m_explode = false;
                return;
            }
            if(firing)
            {
                FireShot(gameTime);
            }

            leftWheelRollMatrix = Matrix.CreateRotationX(LeftWheelRollFloat);
            rightWheelRollMatrix = Matrix.CreateRotationX(RightWheelRollFloat);

            TurretPitch += Input.TurretUpDown;
            TurretYaw += Input.TurretLeftRight;

            UpdateBoundingBox();

//            m_exhaustTrailRight.Update(gameTime, m_position.Location, m_position.Facing);
//            m_exhaustTrailLeft.Update(gameTime, m_position.Location, m_position.Facing);
//            m_dustTrailRight.Update(gameTime, m_position.Location, m_position.Facing);
//            m_dustTrailLeft.Update(gameTime, m_position.Location, m_position.Facing);

            m_exhaustTrailRight.Update(gameTime, Vector3.Transform(r_Exhaust_Offset, m_position.Orientation) + m_position.Location.Translation);
            m_exhaustTrailLeft.Update(gameTime, Vector3.Transform(l_Exhaust_Offset, m_position.Orientation) + m_position.Location.Translation);
            m_dustTrailRight.Update(gameTime, Vector3.Transform(r_Dust_Offset, m_position.Orientation) + m_position.Location.Translation);
            m_dustTrailLeft.Update(gameTime, Vector3.Transform(l_Dust_Offset, m_position.Orientation) + m_position.Location.Translation);

           
            switch (team)
            {
                case Team.RED:
                    lightingEffect = new Vector3(1, 0, 0);
                    break;
                case Team.BLUE:
                    lightingEffect = new Vector3(0, 0, 1);
                    break;
                case Team.NONE:
                    lightingEffect = Vector3.Zero;
                    break;
            }

            base.Update(gameTime);
        }

        private void FireShot(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalSeconds - m_timeOfLastShot.TotalSeconds > 1)
            {
                m_timeOfLastShot = gameTime.TotalGameTime;
                if (!m_ammoMeter.IsEmpty)
                {
                    Ray ray = m_position.Ray;
                    
                    //Get the bullet to turret height
                    ray.Position.Y += 3;
                    ray.Position.Y += Math.Abs(TurretPitch)*4; 
                    ray.Position += (Matrix.CreateRotationY(TurretYaw) * m_position.Orientation).Backward * 5;

                    ray.Direction.Y = -TurretPitch;
                    ray.Direction = Vector3.Transform(ray.Direction, Matrix.CreateRotationY(TurretYaw));

                    m_postMan.SendMessage(MessageType.Fire, this, null, ray, 100f);
                    m_ammoMeter.DecreaseCurrentValue(1);

                    tankCannonSoundEffect.Play();
                    new TankFire(Game, ray.Position);

                    // Firing a shot causes the tank to lurch back or forward depending on which way the turret
                    // is rotated.
                    current_speed += SHOT_RECOIL * (float)Math.Cos((double)TurretYaw);
                }                             
            }
        }

        private void UpdateBoundingBox()
        {
            m_oldBoundingBox = m_boundingBox;
            m_oldOrientedBoundingBox = m_orientedBoundingBox;

            m_boundingBox = (BoundingBox)model.Tag;

            m_boundingBox.Min = Vector3.Transform(m_boundingBox.Min, m_position.Location);
            m_boundingBox.Max = Vector3.Transform(m_boundingBox.Max, m_position.Location);

            m_boundingSphere = Microsoft.Xna.Framework.BoundingSphere.CreateFromBoundingBox(m_boundingBox);

            m_orientedBoundingBox = BoundingOrientedBox.CreateFromBoundingBox(m_boundingBox);
            m_orientedBoundingBox.Orientation = Quaternion.CreateFromRotationMatrix(Position.Orientation);
        }

        public void TakeDamage(object sender, int damage)
        {
            m_healthMeter.DecreaseCurrentValue(damage);
            if (m_healthMeter.IsEmpty)
            {
                ((ICollisionManager)Game.Services.GetService(typeof(ICollisionManager))).UnregisterCollisionObject(this);
                m_postMan.SendMessage(MessageType.SpawnPowerup, this, null, m_position.Location.Translation);
                m_postMan.SendMessage(MessageType.Kill, this, sender);
                m_explode = true;
            }
        }

        public void AddAmmo(int amount)
        {
            m_ammoMeter.IncreaseCurrentValue(amount);
        }

        public void RecoverHealth(int recovery)
        {
            m_healthMeter.IncreaseCurrentValue(recovery);
        }


        /// <summary>
        /// Render our tank in the world
        /// </summary>
        /// <param name="gameTime">the current gameTime</param>
        public override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            if (!m_healthMeter.IsEmpty)
            {
                // Retrieve the current camera
                ICamera camera = Game.Services.GetService(typeof (ICamera)) as ICamera;

                if (camera != null)
                {
                    leftBackWheelBone.Transform = leftWheelRollMatrix * leftBackWheelTransform;
                    rightBackWheelBone.Transform = rightWheelRollMatrix * rightBackWheelTransform;
                    leftFrontWheelBone.Transform = leftWheelRollMatrix * leftFrontWheelTransform;
                    rightFrontWheelBone.Transform = rightWheelRollMatrix * rightFrontWheelTransform;

                    turretBone.Transform = Matrix.CreateRotationY(TurretYaw) * turretTransform;
                    cannonBone.Transform = Matrix.CreateRotationX(TurretPitch) * cannonTransform;

                    model.CopyAbsoluteBoneTransformsTo(bones);

                    // Draw the tank in our game world
                    foreach (ModelMesh mesh in model.Meshes)
                    {
                        foreach (BasicEffect effect in mesh.Effects)
                        {
                            // The world transform for our mesh is a
                            // combination of the bone transform and
                            // our world transform
                            effect.World = bones[mesh.ParentBone.Index] * m_position.LocationFacing;

                            // View and projection transforms come directly from our camera
                            effect.View = camera.View;
                            effect.Projection = camera.Projection;

                            if (lightingEffect == Vector3.Zero)
                                effect.EnableDefaultLighting();
                            else
                            {
                                effect.LightingEnabled = true;
                                effect.DiffuseColor = lightingEffect;
                            }

                            //TODO: Add code to flash for damage, not collisions.
                            //effect.AmbientLightColor = lightingEffect;

                        }

                        // Now we draw the mesh, using the effects that we just set
                        mesh.Draw();
                    }

                    //If we're in a debug build, show the tank's bounding box.
#if DEBUG
                    m_debugDraw.Begin(camera.View, camera.Projection);

                    m_debugDraw.DrawWireSphere(m_boundingSphere, Color.Fuchsia);
                    m_debugDraw.DrawWireBox(m_boundingBox, Color.Red);
                    m_debugDraw.DrawWireBox(m_orientedBoundingBox, Color.Blue);

                    m_debugDraw.End();

#endif

                    m_exhaustTrailLeft.Draw(gameTime);
                    m_exhaustTrailRight.Draw(gameTime);
                    m_dustTrailRight.Draw(gameTime);
                    m_dustTrailLeft.Draw(gameTime);

                    //Draw Game type text bleow score -gw
                    m_spriteBatch.Begin();
                    Vector2 DrawSpot = new Vector2(10, 85);
                    m_spriteBatch.DrawString(m_spriteFont, GameTypeString, DrawSpot, Color.White);
                    m_spriteBatch.End();

                    /*
                     * Taken from Shawn Hargraves' blog entry “Sprite Batch Billboards in a 3D World” and modified slightly to use the camera position
                     * http://blogs.msdn.com/b/shawnhar/archive/2011/01/12/spritebatch-billboards-in-a-3d-world.aspx 
                     */

                    m_basicEffect.World =
                        Matrix.CreateConstrainedBillboard(m_position.Location.Translation + Vector3.Up * 6,
                                                          camera.Position, Vector3.Down, null, null);
                    m_basicEffect.View = camera.View;
                    m_basicEffect.Projection = camera.Projection;

                    Vector2 textOrigin = m_spriteFont.MeasureString(name) / 2;
                    const float textSize = 0.05f;

                    m_spriteBatch.Begin(0, null, null, DepthStencilState.DepthRead, RasterizerState.CullNone,
                                        m_basicEffect);

                    m_spriteBatch.DrawString(m_spriteFont, name, Vector2.Zero, Color.White, 0, textOrigin, textSize, 0,
                                             0);
                    m_spriteBatch.End();
                }
            }
            //End code from Shawn Hargraves

            base.Draw(gameTime);
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
