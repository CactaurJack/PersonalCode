using System.Linq;
using EmawEngineLibrary.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace ExampleGame
{
    internal class GameModeMenu
    {

        private readonly int m_menuCount;

        private int m_debounce = 200;
        private InputManager m_inputManager;
        private GameModeMenuItem[] m_menuItems;
        private int m_selectedMenuItem;

        public GameModeMenu(InputManager input)
        {
            m_menuItems = new[]
                          {
                              new GameModeMenuItem("Free-For-All", GameState.CreateNetwork, GameMode.FFA),
                              new GameModeMenuItem("Team", GameState.CreateNetwork, GameMode.TEAM),
                              new GameModeMenuItem("Back", GameState.NetworkMenu, GameMode.NONESELECTED)
                          };
            m_menuCount = m_menuItems.Count() - 1;

            m_inputManager = input;
        }

        public void Draw(SpriteBatch batch)
        {
            int height = 50;
            int width = batch.GraphicsDevice.Viewport.Width;

            width /= 2;

            batch.Begin();
            Vector2 itemSize = MenuFonts.TitleFont.MeasureString("Game Mode");
            batch.DrawString(MenuFonts.TitleFont, "Game Mode", new Vector2(width - (itemSize.X / 2), height),
                             MenuFonts.TitleColor);
            height += (int) itemSize.Y + 100;

            //Could have used a for loop, but the foreach makes things read nicer.
            //So, we are also keeping track of a int to check the selected index.
            int i = 0;
            foreach (GameModeMenuItem item in m_menuItems)
            {
                itemSize = MenuFonts.TitleFont.MeasureString(item.MenuText);
                batch.DrawString(MenuFonts.TitleFont, item.MenuText, new Vector2(width - (itemSize.X / 2), height),
                                 i == m_selectedMenuItem ? MenuFonts.SelectedColor : MenuFonts.MenuColor);
                height += (int) itemSize.Y + 50;
                i++;
            }
            batch.End();
        }

        public GameState Update(GameTime gameTime)
        {
            m_debounce -= gameTime.ElapsedGameTime.Milliseconds;

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
                m_debounce = 200;
                return m_menuItems[m_selectedMenuItem].ReturnedState;
            }
            return GameState.GameModeMenu;
        }

        public GameMode CurrentGameMode()
        {
            return m_menuItems[m_selectedMenuItem].Mode;
        }

    }
}
