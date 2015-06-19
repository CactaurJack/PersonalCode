#region File Description
//-----------------------------------------------------------------------------
// JoinSessionScreen.cs
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
    class JoinSessionScreen : GameScreen
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

        string errorMessage;
        TimeSpan errorDisplayTime;
        TimeSpan elapsedErrorTime;

        string availableNetworkSessions;
        int numAvailableSessions;
        AvailableNetworkSessionCollection availableSessions;

        int selected = 0;

        #endregion

        #region Initialization

        public JoinSessionScreen()
        {
            networkSession = null;

            errorMessage = "";
            errorDisplayTime = TimeSpan.FromSeconds(2);
            elapsedErrorTime = TimeSpan.FromSeconds(0);

            availableNetworkSessions = "Choose the session you would like to join:\n";
            numAvailableSessions = 0;
            availableSessions = null;
        }

        public override void LoadContent()
        {
            if (content == null)
            {
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            }

            spriteBatch = ScreenManager.SpriteBatch;

            font = content.Load<SpriteFont>("gameFont");
            fontPos = new Vector2(50, 50);

            game = ScreenManager.Game;

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            if (availableSessions != null)
            {
                availableSessions.Dispose();
                availableSessions = null;
            }
        }

        #endregion

        #region Update and Draw

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (errorMessage == "")
            {
                if (Gamer.SignedInGamers.Count == 0)
                {
                    // If there are no profiles signed in, we cannot proceed.
                    // Show the Guide so the user can sign in.
                    if (!Guide.IsVisible) Guide.ShowSignIn(maxLocalGamers, false);
                }
                else if (networkSession == null && numAvailableSessions == 0)
                {
                    // we enter this case if we have not found possible sessions yet

                    try
                    {
                        // Search for sessions.
                        availableSessions = NetworkSession.Find(NetworkSessionType.SystemLink, maxLocalGamers, null);
                        {
                            if (availableSessions.Count == 0)
                            {
                                errorMessage = "No network sessions found.";
                                return;
                            }
                            else
                            {
                                // populate the list of available sessions
                                for (int i = 0; i < availableSessions.Count; i++)
                                {
                                    string gameMode;
                                    switch ((GameMode)availableSessions[i].SessionProperties[0])
                                    {
                                        case GameMode.FFA:
                                            gameMode = "Free for all";
                                            break;
                                        case GameMode.CAPTURETHEFLAG:
                                            gameMode = "Capture the flag";
                                            break;
                                        case GameMode.SINGLE:
                                            gameMode = "Free roam";
                                            break;
                                        default:
                                            throw new NotImplementedException();
                                    }


                                    if (i == selected)
                                    {
                                        availableNetworkSessions += gameMode + " - " + "Hosted by " + availableSessions[i].HostGamertag + " <-" + "\n";
                                    }
                                    else
                                    {
                                        availableNetworkSessions += gameMode + " - "  + "Hosted by " + availableSessions[i].HostGamertag + "\n";
                                    }
                                    numAvailableSessions++;
                                }
                            }
                        }
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
                        ScreenManager.AddScreen(new GameplayScreen(networkSession){ScreenManager = ScreenManager}, null);
                        UnloadContent();
                        ScreenManager.RemoveScreen(this);
                    }
                }

                if (numAvailableSessions > 0 && availableSessions != null)
                {
                    availableNetworkSessions = "Choose the session you would like to join:\n";
                    for (int i = 0; i < availableSessions.Count; i++)
                    {
                        string gameMode;
                        switch ((GameMode)availableSessions[i].SessionProperties[0])
                        {
                            case GameMode.FFA:
                                gameMode = "Free for all";
                                break;
                            case GameMode.CAPTURETHEFLAG:
                                gameMode = "Capture the flag";
                                break;
                            case GameMode.SINGLE:
                                gameMode = "Free roam";
                                break;
                            default:
                                throw new NotImplementedException();
                        }

                        if (i == selected)
                        {
                            availableNetworkSessions += gameMode + " - " + "Hosted by " + availableSessions[i].HostGamertag + " <-" + "\n";
                        }
                        else
                        {
                            availableNetworkSessions += gameMode + " - " + "Hosted by " + availableSessions[i].HostGamertag + "\n";
                        }
                    }
                }
            }
            else
            {
                // we have an error finding a session to join, so display the message for some time
                // and return to the main menu to start over
                elapsedErrorTime += gameTime.ElapsedGameTime;
                if (elapsedErrorTime > errorDisplayTime)
                {
                    ScreenManager.RemoveScreen(this);
                }
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }


        public override void HandleInput(InputState input)
        {
            // let us pick from sessions if we have any available
            if (numAvailableSessions > 0)
            {
                PlayerIndex playerIndex;
                if (input.IsNewKeyPress(Keys.Down, ControllingPlayer, out playerIndex) && selected < numAvailableSessions-1)
                {
                    selected++;
                }
                else if (input.IsNewKeyPress(Keys.Up, ControllingPlayer, out playerIndex) && selected > 0)
                {
                    selected--;
                }
                else if (input.IsNewKeyPress(Keys.Enter, ControllingPlayer, out playerIndex)) 
                {
                    if (networkSession == null)
                    {
                        networkSession = NetworkSession.Join(availableSessions[selected]);
                        HookSessionEvents();
                    }
                }
                else if (input.IsNewKeyPress(Keys.Escape, ControllingPlayer, out playerIndex))
                {
                    ScreenManager.RemoveScreen(this);
                }

                //if (input.IsNewKeyPress(Keys.D0, ControllingPlayer, out playerIndex) && numAvailableSessions >= 1)
                //{
                //    networkSession = NetworkSession.Join(availableSessions[0]);
                //    HookSessionEvents();
                //}
                //else if (input.IsNewKeyPress(Keys.D1, ControllingPlayer, out playerIndex) && numAvailableSessions >= 2)
                //{
                //    networkSession = NetworkSession.Join(availableSessions[1]);
                //    HookSessionEvents();
                //}
                //else if (input.IsNewKeyPress(Keys.D2, ControllingPlayer, out playerIndex) && numAvailableSessions >= 3)
                //{
                //    networkSession = NetworkSession.Join(availableSessions[2]);
                //    HookSessionEvents();
                //}
                //else if (input.IsNewKeyPress(Keys.D3, ControllingPlayer, out playerIndex) && numAvailableSessions >= 4)
                //{
                //    networkSession = NetworkSession.Join(availableSessions[3]);
                //    HookSessionEvents();
                //}
                //else if (input.IsNewKeyPress(Keys.D4, ControllingPlayer, out playerIndex) && numAvailableSessions >= 5)
                //{
                //    networkSession = NetworkSession.Join(availableSessions[4]);
                //    HookSessionEvents();
                //}
            }
            base.HandleInput(input);
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.Black);

            if (errorMessage == "" && numAvailableSessions == 0)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(font, "Searching for available sessions to join...", fontPos, Color.Moccasin);
                spriteBatch.End();
            }
            else if (numAvailableSessions > 0)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(font, availableNetworkSessions, fontPos, Color.Moccasin);
                spriteBatch.End();
            }
            else
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(font, errorMessage, fontPos, Color.Beige);
                spriteBatch.End();
            }

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
           // e.Gamer.Tag = new Tank(ScreenManager.Game, e.Gamer);
            if (e.Gamer.IsLocal)
            {
                //there is absolutely no reason to create tanks outside of the player class if each tank is strictly associated with a player anyway.
                /*Tank tank = new Tank(game);
                tank.Initialize();*/
                //Components.Add(tank);
#warning For game modes code, look here. Game modes have been disabled for the time being.
                InputManager input = (InputManager)game.Services.GetService(typeof(InputManager));

                player = new Player(game, input, GameMode.NONESELECTED, e.Gamer);//(GameMode)networkSession.SessionProperties[0], e.Gamer);

            }
            else
            {
                
                /*Tank tank = new Tank(game, e.Gamer);
                tank.Initialize();*/
                //Components.Add(tank);

                player = new Player(game, null, GameMode.NONESELECTED, e.Gamer);
            }

            e.Gamer.Tag = player;
            //e.Gamer.Tag = new Tank(gamerIndex, content, ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height);
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
