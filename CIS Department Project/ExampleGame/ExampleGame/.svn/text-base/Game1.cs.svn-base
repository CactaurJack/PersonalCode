using EmawEngineLibrary.Cameras;
using EmawEngineLibrary.Input;
using EmawEngineLibrary.Logging;
using EmawEngineLibrary.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using System.Collections.Generic;

namespace ExampleGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ScreenManager screenManager;

        GamerServicesComponent gamerServicesComponent;

        // The music manager
        MusicManager musicManager;

        public Game1()
            : base()
        {
            // Initialize the GraphicsDeviceManager 
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 700;

            // Set the content directory
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // The music manager service
            musicManager = new MusicManager(this);
            Components.Add(musicManager);
            Services.AddService(typeof(MusicManager),musicManager);

            // Stuff I added for the Game State screens
            screenManager = new ScreenManager(this);
            Components.Add(screenManager);

            screenManager.AddScreen(new SplashScreen("splash"), null);
            
            gamerServicesComponent = new GamerServicesComponent(this);
            Components.Add(gamerServicesComponent);

            //Post office and the log auto registers themselves.
            //MUST be done before anything else.
            new FileSystemLog(this);
            new MessageHandler(this);

            new CollisionManager(this);

            new InputManager(this);

            ICamera camera = new ThirdPersonCamera(this);
            Services.AddService(typeof(ICamera), camera);
            Components.Add(camera);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // clear our screen before drawing things
            GraphicsDevice.Clear(Color.Black);

            // triggers drawing of all the registered components
            base.Draw(gameTime);

            // draws things from the gameplayscreen like our 2D HUD on top of everything else
            screenManager.Draw(gameTime);
        }
    }
}