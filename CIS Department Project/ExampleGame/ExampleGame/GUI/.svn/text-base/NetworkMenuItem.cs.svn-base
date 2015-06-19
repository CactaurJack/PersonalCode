using Microsoft.Xna.Framework.Net;

namespace ExampleGame
{
    internal class NetworkMenuItem
    {
        public NetworkMenuItem(string menuText, GameState returnedState, AvailableNetworkSession networkGame)
        {
            MenuText = menuText;
            ReturnedState = returnedState;
            NetworkGame = networkGame;
        }

        public string MenuText { get; private set; }
        public AvailableNetworkSession NetworkGame { get; private set; }
        public GameState ReturnedState { get; private set; }
    }
}