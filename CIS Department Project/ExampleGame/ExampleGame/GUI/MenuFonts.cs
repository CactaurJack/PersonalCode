using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ExampleGame
{
    //Font class idea taken from the Network game example from class.
    //Some stylistic decisions (font colors) also taken.
    internal static class MenuFonts
    {
        public static void LoadContent(ContentManager content)
        {
            TitleFont = content.Load<SpriteFont>("Fonts/TitleFont");
            MenuFont = content.Load<SpriteFont>("Fonts/MenuFont");
            SelectedFont = content.Load<SpriteFont>("Fonts/SelectedFont");
        }

        public static SpriteFont MenuFont { get; private set; }
        public static SpriteFont SelectedFont { get; private set; }
        public static SpriteFont TitleFont { get; private set; }

        public static readonly Color MenuColor = Color.OliveDrab;
        public static readonly Color SelectedColor = Color.Sienna;
        public static readonly Color TitleColor = Color.OliveDrab;
    }
}