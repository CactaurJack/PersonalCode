using ExampleGame;

internal class GameModeMenuItem
{
    public GameModeMenuItem(string menuText, GameState returnedState, GameMode gameMode)
    {
        MenuText = menuText;
        ReturnedState = returnedState;
        Mode = gameMode;    
    }

    public GameMode Mode { get; private set; }
    public string MenuText { get; private set; }
    public GameState ReturnedState { get; private set; }
}