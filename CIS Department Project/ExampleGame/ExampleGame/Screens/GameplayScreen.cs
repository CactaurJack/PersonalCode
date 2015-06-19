#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Modified code originally from the following:
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Threading;
using System.Collections.Generic;
using EmawEngineLibrary.Input;
using EmawEngineLibrary.Logging;
using EmawEngineLibrary.Performance;
using EmawEngineLibrary.Physics;
using EmawEngineLibrary.Terrain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using System.Diagnostics;
using EmawEngineLibrary.Graphics;
using EmawEngineLibrary.Graphics.Particles;
using EmawEngineLibrary.Graphics.Primitives;
using EmawEngineLibrary.Graphics.Billboards;
using EmawEngineLibrary.Cameras;
#endregion

namespace ExampleGame
{
    /// <summary>
    /// m_game screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class GameplayScreen : GameScreen
    {
        private ICamera camera;

        private SpriteBatch m_spriteBatch;
        private SpriteFont m_spriteFont;

        private InputManager m_input;

        private NetworkSession m_networkSession;
        private PacketReader m_packetReader;
        private PacketWriter m_packetWriter;

        private Skybox sky;
        private GameState m_gameState;

        private Texture2D m_healthBar;
        private Texture2D m_healthBarBackground;
        private Texture2D m_bullet;

        private ILog m_log;

        private Game m_game;

        public GameplayScreen (NetworkSession networkSession)
        {
            m_packetReader = new PacketReader();
            m_packetWriter = new PacketWriter();

            m_networkSession = networkSession;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// m_game is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected void Initialize ()
        {
            // Updates to GameState now update global GameState service
            m_gameState = ScreenManager.GameState;

            CollisionManager colMan = (CollisionManager) m_game.Services.GetService(typeof (ICollisionManager));
            CollisionBox worldBox = new CollisionBox(new Vector3(-200, -256, -200), new Vector3(200, 256, 200));
            colMan.WorldBox = worldBox;

            m_input = (InputManager) m_game.Services.GetService(typeof (InputManager));

            m_game.Components.Add(new Terrain(m_game, "GrassLand"));

            // create our basic camera and add it to the components list
            camera = (ICamera) m_game.Services.GetService(typeof (ICamera));

            m_packetReader = new PacketReader ();
            m_packetWriter = new PacketWriter ();
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public override void LoadContent ()
        {
            m_game = ScreenManager.Game;

            m_spriteBatch = new SpriteBatch (m_game.GraphicsDevice);
            m_spriteFont = m_game.Content.Load<SpriteFont>("DemoFont");

            sky = m_game.Content.Load<Skybox>("Skybox/sky");

            m_healthBar = m_game.Content.Load<Texture2D>("HealthBar");
            m_healthBarBackground = m_game.Content.Load<Texture2D>("HealthBarBackground");
            m_bullet = m_game.Content.Load<Texture2D>("Bullet");

             Initialize();
            base.LoadContent ();
        }


        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                m_game.Exit();

            int dt = gameTime.ElapsedGameTime.Milliseconds;

            if (m_input != null && m_input.IsKeyDown(Keys.Escape))
            {
                EndNetworkGame(m_game, null);
                ScreenManager.RemoveScreen(this);
                ScreenManager.RemoveScreen(this);
            }
            else if (m_networkSession == null)
                m_gameState = GameState.NetworkMenu;
            else
            {
               try
               {
                   ScreenManager.GameState = GameState.NetworkGame;
                    foreach (LocalNetworkGamer gamer in m_networkSession.LocalGamers)
                    {
                        Player player = gamer.Tag as Player;
                        player.Update(gameTime);

                        if (player._Team == null)
                            if ((GameMode) m_networkSession.SessionProperties[0] == GameMode.TEAM)
                                player.SetTeam(ChooseTeam());

                        if (camera != null)
                            camera.Focus(player.Tank);

                        //Broadcast our tank's state
                        m_packetWriter.Write(player.Tank.Facing);
                        m_packetWriter.Write(player.Tank.Position.Location.Translation);
                        m_packetWriter.Write(player.Tank.Input.Shoot);
                        m_packetWriter.Write((int) player._Team);
                        m_packetWriter.Write(player.Tank.CurrentHP);
                        m_packetWriter.Write(player.Tank.TurretYaw);
                        m_packetWriter.Write(player.Tank.TurretPitch);
                        m_packetWriter.Write(player.Tank.LeftWheelRollFloat);
                        m_packetWriter.Write(player.Tank.RightWheelRollFloat);


                        gamer.SendData(m_packetWriter, SendDataOptions.InOrder);
                    }

                    m_networkSession.Update();

                    foreach (LocalNetworkGamer gamer in m_networkSession.LocalGamers)
                    {
                        //Read any packets sent to us
                        //m_game is based on the Peer to Peer game example from the AppHub site.
                        //Not a copy; I did write m_game. But, there are only so many ways to write m_game,
                        //and the example was a heavy reference, so I figured I should reference it.
                        while (gamer.IsDataAvailable)
                        {
                            NetworkGamer sender;
                            gamer.ReceiveData(m_packetReader, out sender);

                            Player player = sender.Tag as Player;
                            float facing = m_packetReader.ReadSingle();
                            Vector3 position = m_packetReader.ReadVector3();
                            bool firing = m_packetReader.ReadBoolean();

                            player._Team = (Team)m_packetReader.ReadInt32();
                            player.Tank.CurrentHP = m_packetReader.ReadInt32();
                            player.Tank.TurretYaw = m_packetReader.ReadSingle();
                            player.Tank.TurretPitch = m_packetReader.ReadSingle();
                            player.Tank.LeftWheelRollFloat = m_packetReader.ReadSingle();
                            player.Tank.RightWheelRollFloat = m_packetReader.ReadSingle();

                            if ((gamer.Tag as Player) != player)
                                player.Update(gameTime, position, facing, firing);



                        }
                    }
                }//close bracket for try
                catch
                {
                    EndNetworkGame(m_game, null);
                    m_gameState = GameState.ResetState;
                }
            }


            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }


        private Team ChooseTeam()
        {
            int redCount = 0;
            int blueCount = 0;

            foreach (Gamer gamer in m_networkSession.RemoteGamers)
            {

                Player player = gamer.Tag as Player;
                if (player._Team == Team.BLUE) blueCount++;
                else if (player._Team == Team.RED) redCount++;

            }
            return redCount > blueCount ? Team.BLUE : Team.RED;
        }


        private void EndNetworkGame (object sender, NetworkSessionEndedEventArgs e)
        {
            if (m_networkSession != null)
            {
                foreach (Gamer gamer in m_networkSession.AllGamers)
                {
                    Player player = gamer.Tag as Player;
                    if (player != null) 
                        player.Dispose();
                }
                m_game.Services.RemoveService(typeof(ITerrain));

                //Clear out collisionmanager list
                ICollisionManager colMan = (ICollisionManager) m_game.Services.GetService(typeof (ICollisionManager));
                colMan.ClearCollisionList();

                m_networkSession.Dispose ();
                m_networkSession = null;
            }

            m_gameState = GameState.Menu;
        }

        /// <summary>
        /// m_game is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            if (camera != null)
            {
                if (m_networkSession == null)
                    m_gameState = GameState.NetworkMenu;
                else
                {
                    Player localPlayer = m_networkSession.LocalGamers[0].Tag as Player;

                    foreach (Gamer gamer in m_networkSession.AllGamers)
                    {
                        Player player = gamer.Tag as Player;
                        if (player != null)
                            player.Tank.Draw(gameTime);
                    }


                    // Render our reference grid
                    sky.Draw(gameTime, camera.View);
                    base.Draw(gameTime);
                    DrawHUD(localPlayer);
                }
            }
        }

        /// <summary>
        /// Draws the health bar and ammo bar
        /// </summary>
        /// <param name="tank"></param>
        private void DrawHUD(Player player)
        {
            if (player != null)
            {
                Rectangle backgroundRectangle = new Rectangle(5, 5, 200, 20);
                Rectangle healthBarRectangle = new Rectangle(backgroundRectangle.X, backgroundRectangle.Y,
                                                             (int)
                                                             ((double) backgroundRectangle.Width * player.Tank.PercentHP),
                                                             backgroundRectangle.Height);
                Rectangle ammoRectangle = new Rectangle(backgroundRectangle.X,
                                                        backgroundRectangle.Y + backgroundRectangle.Height + 3, 50, 5);
                int ammoPositionX = backgroundRectangle.X + 7;
                int ammoPositionY = backgroundRectangle.Y + backgroundRectangle.Height + 5;
                m_spriteBatch.Begin();
                m_spriteBatch.Draw(m_healthBarBackground, backgroundRectangle, Color.White);
                m_spriteBatch.Draw(m_healthBar, healthBarRectangle, Color.White);
                for (int i = 0; i < player.Tank.CurrentAmmo; i++)
                {
                    m_spriteBatch.Draw(m_bullet, new Rectangle(ammoPositionX, ammoPositionY, 6, 34), Color.White);
                    ammoPositionX += 7;
                }
                m_spriteBatch.DrawString(m_spriteFont, "Score: " + player.PlayerScore, new Vector2(11, 60), Color.White);
                //if(m_networkSession != null && (GameMode)m_networkSession.SessionProperties[0] == GameMode.TEAM)
                //{
                //    m_spriteBatch.DrawString(m_spriteFont, "Team Score: " + GetScore(player._Team), new Vector2(11, 90), player._Team == Team.BLUE ? Color.Blue : Color.Red);
                //}
                m_spriteBatch.End();
            }
        }

        private int GetScore(Team team)
        {
            int totalScore = 0;
            foreach (Gamer gamer in m_networkSession.AllGamers)
            {
                Player player = gamer.Tag as Player;
                if (player._Team == team) totalScore += player.PlayerScore;
            }
            return totalScore;
        }

    }
}
