using EmawEngineLibrary.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ExampleGame
{
    internal class NetworkGameNotFoundMenu
    {
        public NetworkGameNotFoundMenu(InputManager input)
        {
            m_menuItems = new MenuItem("Ok", GameState.FindGame);
            m_inputManager = input;
        }


        public void Draw(SpriteBatch batch)
        {
            int height = 100;
            int width = batch.GraphicsDevice.Viewport.Width;

            width /= 2;

            batch.Begin();
            Vector2 itemSize = MenuFonts.TitleFont.MeasureString("Selected game no longer exists");
            batch.DrawString(MenuFonts.TitleFont, "Selected game no longer exists",
                             new Vector2(width - (itemSize.X / 2), height), MenuFonts.TitleColor);
            height += (int) itemSize.Y + 50;

            itemSize = MenuFonts.TitleFont.MeasureString(m_menuItems.MenuText);
            batch.DrawString(MenuFonts.TitleFont, m_menuItems.MenuText, new Vector2(width - (itemSize.X / 2), height),
                             MenuFonts.SelectedColor);

            batch.End();
        }

        public GameState Update(GameTime gameTime)
        {
            m_debounce -= gameTime.ElapsedGameTime.Milliseconds;

            if (m_inputManager.IsKeyDown(Keys.Enter) && m_debounce < 0)
            {
                m_debounce = 200;
                return m_menuItems.ReturnedState;
            }
            return GameState.NetworkGameDoesNotExist;
        }


        private int m_debounce = 200;
        private InputManager m_inputManager;
        private MenuItem m_menuItems;
    }
}