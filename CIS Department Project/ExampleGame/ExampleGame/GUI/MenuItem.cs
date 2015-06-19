using ExampleGame;

internal class MenuItem
{
    public MenuItem(string menuText, GameState returnedState)
    {
        MenuText = menuText;
        ReturnedState = returnedState;
    }

    public string MenuText { get; private set; }
    public GameState ReturnedState { get; private set; }
}