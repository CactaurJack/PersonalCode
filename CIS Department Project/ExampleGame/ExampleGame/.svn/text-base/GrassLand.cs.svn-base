using System;
using System.Threading;
using System.Collections.Generic;
using EmawEngineLibrary.Input;
using EmawEngineLibrary.Logging;
using EmawEngineLibrary.Performance;
using EmawEngineLibrary.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using System.Diagnostics;
using EmawEngineLibrary;
using EmawEngineLibrary.Graphics;
using EmawEngineLibrary.Graphics.Particles;
using EmawEngineLibrary.Graphics.Primitives;
using EmawEngineLibrary.Graphics.Billboards;
using EmawEngineLibrary.Cameras;
using EmawEngineLibrary.Worlds;
using EmawEngineLibrary.Terrain;

namespace ExampleGame
{
    class GrassLand : DrawableGameComponent, IWorld
    {
        private Skybox skybox;
        private ICamera camera;
        private CollisionBox worldBox;

        public GrassLand(Game game) : base(game)
        {
            camera = (ICamera)Game.Services.GetService(typeof(ICamera));
        }

        public override void Initialize()
        {
            worldBox = new CollisionBox(new Vector3(-200, -256, -200), new Vector3(200, 256, 200));
            Game.Components.Add(new Terrain(Game, "GrassLand"));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            skybox = Game.Content.Load<Skybox>("Skybox/sky");
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            Game.Services.RemoveService(typeof(IWorld));

            base.UnloadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            skybox.Draw(gameTime, camera.View);
            
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public CollisionBox GetWorldbox()
        {
                return worldBox;
        }
       
    }
}
