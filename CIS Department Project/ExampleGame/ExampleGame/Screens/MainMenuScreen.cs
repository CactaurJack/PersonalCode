#region File Description
//-----------------------------------------------------------------------------
// MainMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace ExampleGame
{
    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    class MainMenuScreen : MenuScreen
    {
        #region Fields
        
        #endregion

        #region Initialization

        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public MainMenuScreen()
            : base("Tank Combat")
        {
            // Create our menu entries.
            //MenuEntry playGameMenuEntry = new MenuEntry("Play Single Player Game");
            MenuEntry hostMenuEntry = new MenuEntry("Host a Networked Game");
            MenuEntry joinMenuEntry = new MenuEntry("Join a Networked Game");
            MenuEntry creditsMenuEntry = new MenuEntry("Credits");
            MenuEntry exitMenuEntry = new MenuEntry("Exit");

            // Hook up menu event handlers.
            //playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            hostMenuEntry.Selected += HostMenuEntrySelected;
            joinMenuEntry.Selected += JoinMenuEntrySelected;
            creditsMenuEntry.Selected += CreditsMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            //MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(hostMenuEntry);
            MenuEntries.Add(joinMenuEntry);
            MenuEntries.Add(creditsMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        #endregion

        #region Handle Input


        ///// <summary>
        ///// Event handler for when the Play Game menu entry is selected.
        ///// </summary>
        //void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        //{
        //    ScreenManager.AddScreen(new GameplayScreen(), e.PlayerIndex);
        //}

        /// <summary>
        /// Event handler for when the Host Game menu entry is selected.
        /// </summary>
        void HostMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            //ScreenManager.AddScreen(new HostSessionScreen(), e.PlayerIndex);
            ScreenManager.AddScreen(new GameTypeScreen(), e.PlayerIndex);
        }

        /// <summary>
        /// Event handler for when the Join Game menu entry is selected.
        /// </summary>
        void JoinMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new JoinSessionScreen(), e.PlayerIndex);
        }

        /// <summary>
        /// Event handler for when the Credits menu entry is selected.
        /// </summary>
        void CreditsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new CreditScreen(), e.PlayerIndex);
        }

        /// <summary>
        /// Exit the game
        /// </summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {
            ScreenManager.Game.Exit();
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
        #endregion
    }
}
