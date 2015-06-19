using System.Collections.Generic;
using System.Linq;
using EmawEngineLibrary.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;

namespace ExampleGame
{
    internal class NetworkGameSelectionMenu
    {
        public NetworkGameSelectionMenu(InputManager input)
        {
            m_inputManager = input;
            m_refresh = 0;
            m_menuItems = new List<NetworkMenuItem>();

            //Loading games item is a quick hack to give a visual prompt that we are loading.
            //Before, it would give a blank screen for a few seconds until the list is created.
            //Now, we have some communication. The hack part comes in because the user can technically
            //select this option. However, it won't do anything if they select it.
            m_menuItems.Add(new NetworkMenuItem("Loading games...", GameState.FindGame, null));
            m_menuItems.Add(new NetworkMenuItem("Back", GameState.NetworkMenu, null));
        }

        public void Draw(SpriteBatch batch)
        {
            int height = 50;
            int width = batch.GraphicsDevice.Viewport.Width;

            width /= 2;

            batch.Begin();
            Vector2 itemSize = MenuFonts.TitleFont.MeasureString("Games");
            batch.DrawString(MenuFonts.TitleFont, "Games", new Vector2(width - (itemSize.X / 2), height),
                             MenuFonts.TitleColor);
            height += (int) itemSize.Y + 100;

            //Could have used a for loop, but the foreach makes things read nicer.
            //So, we are also keeping track of a int to check the selected index.
            int i = 0;
            foreach (NetworkMenuItem item in m_menuItems)
            {
                itemSize = MenuFonts.TitleFont.MeasureString(item.MenuText);
                batch.DrawString(MenuFonts.TitleFont, item.MenuText, new Vector2(width - (itemSize.X / 2), height),
                                 i == m_selectedMenuItem ? MenuFonts.SelectedColor : MenuFonts.MenuColor);
                height += (int) itemSize.Y + 50;
                i++;
            }
            batch.End();
        }

        //This needs a good chunk of refinement.
        //When we perform a refresh, we hang for a bit.
        //Also, the list will reorder itsself.
        public void Refresh()
        {
            
            AvailableNetworkSessionCollection availableGames = NetworkSession.Find(NetworkSessionType.SystemLink, 1,
                                                                                   null);

            m_menuItems = new List<NetworkMenuItem>();

            foreach (AvailableNetworkSession game in availableGames)
            {
                string gameMode = string.Empty;
                if (game.SessionProperties[0] != null)
                {
                    switch ((GameMode)game.SessionProperties[0])
                    {
                        case GameMode.FFA:
                            gameMode = "Free-For-All";
                            break;
                        case GameMode.TEAM:
                            gameMode = "Team";
                            break;
                    }

                    m_menuItems.Add(
                                    new NetworkMenuItem(
                                        string.Format("Name: {0}  Players:{1}   Mode: {2}", game.HostGamertag, game.CurrentGamerCount, gameMode),
                                        GameState.JoinNetwork, game));
                }
            }

            m_menuItems.Add(new NetworkMenuItem("Back", GameState.NetworkMenu, null));
            m_menuCount = m_menuItems.Count() - 1;
        }

        public GameState Update(GameTime gameTime, out AvailableNetworkSession game)
        {
            game = null;
            m_debounce -= gameTime.ElapsedGameTime.Milliseconds;
            m_refresh -= gameTime.ElapsedGameTime.Milliseconds;

            if (m_refresh <= 0)
            {
                Refresh();
                m_refresh = 5000;
            }


            if (m_inputManager.IsKeyDown(Keys.Up) && m_debounce < 0)
            {
                m_selectedMenuItem--;
                if (m_selectedMenuItem < 0)
                    m_selectedMenuItem = m_menuCount;
                m_debounce = 200;
            }
            if (m_inputManager.IsKeyDown(Keys.Down) && m_debounce < 0)
            {
                m_selectedMenuItem++;
                if (m_selectedMenuItem > m_menuCount)
                    m_selectedMenuItem = 0;
                m_debounce = 200;
            }

            if (m_inputManager.IsKeyDown(Keys.Enter) && m_debounce < 0)
            {
                game = m_menuItems[m_selectedMenuItem].NetworkGame;
                m_debounce = 200;
                return m_menuItems[m_selectedMenuItem].ReturnedState;
            }
            return GameState.FindGame;
        }


        private int m_debounce = 200;
        private InputManager m_inputManager;
        private int m_menuCount;
        private List<NetworkMenuItem> m_menuItems;
        private int m_refresh;
        private int m_selectedMenuItem;
    }
}