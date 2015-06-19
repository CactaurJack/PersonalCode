using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ExampleGame
{
    class SplashScreen
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i">How long to display the screen in milliseconds.</param>
        public SplashScreen(int i)
        {
            DisplayTime = i;
            DisplayedTime = 0;
        }

        public void LoadContent(ContentManager contentManager)
        {
            m_logo = contentManager.Load<Texture2D>("ui/splash");
        }

        public void Draw(SpriteBatch batch)
        {
            int height = batch.GraphicsDevice.Viewport.Height;
            int width = batch.GraphicsDevice.Viewport.Width;
            height -= m_logo.Height;
            height /= 2;
            width -= m_logo.Height;
            width /= 2;
            batch.Begin();
            batch.Draw(m_logo, new Vector2(width, height), Color.White);
            batch.End();
        }

        public void Reset()
        {
            DisplayedTime = 0;
        }

        public bool ShouldStop()
        {
            return DisplayedTime >= DisplayTime;
        }

        public int DisplayTime{ get; set; }
        public int DisplayedTime { get; set; }

        private Texture2D m_logo;
    }
}
