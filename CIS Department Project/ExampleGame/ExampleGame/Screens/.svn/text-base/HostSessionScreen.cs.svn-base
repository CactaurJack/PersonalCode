#region File Description
//-----------------------------------------------------------------------------
// HostSessionScreen.cs
//
// Based off an example of the Peer to Peer Game Project from the following:
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Threading;
using System.Collections.Generic;
using EmawEngineLibrary.Cameras;
using EmawEngineLibrary.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using System.Diagnostics;
#endregion

namespace ExampleGame
{
    class HostSessionScreen : GameScreen
    {
        #region Fields

        Game game;
        ContentManager content;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Vector2 fontPos;

        NetworkSession networkSession;

        const int maxGamers = 4;
        const int maxLocalGamers = 4;
        GameMode gameType;

        string errorMessage;

        #endregion

        #region Initialization

        public HostSessionScreen()
        {
            networkSession = null;

        }

        public HostSessionScreen(GameMode _gameType)
        {
            networkSession = null;
            gameType = _gameType;

        }

        public override void LoadContent()
        {
            if (content == null)
            {
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            }

            spriteBatch = ScreenManager.SpriteBatch;

            font = content.Load<SpriteFont>("gameFont");
            fontPos = new Vector2(100, 50);

            game = ScreenManager.Game;


            base.LoadContent();
        }

        public override void UnloadContent()
        {
        }

        #endregion

        #region Update and Draw

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (Gamer.SignedInGamers.Count == 0)
            {
                // If there are no profiles signed in, we cannot proceed.
                // Show the Guide so the user can sign in.
                if (!Guide.IsVisible) Guide.ShowSignIn(maxLocalGamers, false);
            }
            else if (networkSession == null)
            {
                try
                {
                    //if (!networkSession.IsDisposed) networkSession.Dispose();
                    NetworkSessionProperties networkSessionProperties = new NetworkSessionProperties();
                    networkSessionProperties[0] = (int)gameType;
                    //networkSessionProperties[1] = gameworld;  uncomment this when you add gameworlds

                    networkSession = NetworkSession.Create(NetworkSessionType.SystemLink, maxLocalGamers, maxGamers,0 , networkSessionProperties);

                    HookSessionEvents();
                }
                catch (Exception e)
                {
                    errorMessage = e.Message;
                }
            }
            else
            {
                if (networkSession != null && !Guide.IsVisible)
                {
                    //networkSession.StartGame();
                    ScreenManager.AddScreen(new GameplayScreen(networkSession) {ScreenManager = ScreenManager}, null);
                    ScreenManager.RemoveScreen(this);
                }
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Creating Session...", fontPos, Color.Moccasin);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// After creating or joining a network session, we must subscribe to
        /// some events so we will be notified when the session changes state.
        /// </summary>
        void HookSessionEvents()
        {
            networkSession.GamerJoined += GamerJoinedEventHandler;
            networkSession.SessionEnded += SessionEndedEventHandler;
            networkSession.GamerLeft += GamerLeftEventHandler;
        }


        /// <summary>
        /// This event handler will be called whenever a new gamer joins the session.
        /// We use it to allocate a Tank object, and associate it with the new gamer.
        /// </summary>
        void GamerJoinedEventHandler(object sender, GamerJoinedEventArgs e)
        {
            Player player;

            //If this is a network tank, we don't need an input manager.
            //Also, we won't want to create that text billboard. That would be annoying.
            if (e.Gamer.IsLocal)
            {
                //New Tank constructor was written so that Tank now holds a game type
                //Constructor located in Tank.cs
                //All old constructors still functional -gw

                //there is absolutely no reason to create tanks outside of the player class if each tank is strictly associated with a player anyway.
                /*Tank tank = new Tank(game, gameType);
                tank.Initialize();*/
                //Components.Add(tank);

                #warning Game modes are now active
                // gameMode of 0 = Free Roam = NONESELECTED
                // gameMode of 1 = FFA
                // gameMode of 2 = CTF
                // -gw


                /* This is handled in the networkSessionProperties now
                //Switch statement linking input ints to the enum struct defined earlier, doesn't really do much though -gw
                switch (gameType) {
                    case 0:
                        GameMode = GameMode.NONESELECTED;
                        break;
                    case 1:
                        GameMode = GameMode.FFA;
                        break;
                    case 2:
                        GameMode = GameMode.CAPTURETHEFLAG;
                        break;
                }
                */
                InputManager input = (InputManager) game.Services.GetService(typeof (InputManager));
                //Uses same player constructor -gw
                player = new Player(game, input, gameType, e.Gamer);//(GameMode)networkSession.SessionProperties[0], e.Gamer);

            }
            else
            {
                /*//there is absolutely no reason to create tanks outside of the player class if each tank is strictly associated with a player anyway.
                 Tank tank = new Tank(game, e.Gamer, gameType);
                 tank.Initialize();*/
                //Components.Add(tank);

                //player = new Player(game, tank, null, (GameMode)networkSession.SessionProperties[0], e.Gamer);
                player = new Player(game, null, gameType, e.Gamer);
            }

            e.Gamer.Tag = player;
            //if ((GameMode)m_networkSession.SessionProperties[0] == GameMode.TEAM) player.SetTeam(ChooseTeam());
        }


        /// <summary>
        /// Event handler notifies us when the network session has ended.
        /// </summary>
        void SessionEndedEventHandler(object sender, NetworkSessionEndedEventArgs e)
        {
            errorMessage = e.EndReason.ToString();

            networkSession.Dispose();
            networkSession = null;
        }


        /// <summary>
        /// Event handler notifies us when a gamer has left the session.
        /// Doesn't do anything until we get into gameplay.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void GamerLeftEventHandler(object sender, GamerLeftEventArgs e)
        {
            Player player = e.Gamer.Tag as Player;

            player.Dispose();
        }

        #endregion
    }
}
