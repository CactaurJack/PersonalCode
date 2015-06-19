using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;

//New Menu screen to handle the selection of game types.  This screen is only visable to the host.
//To add new game types:
//new MenuEntry must be made
//An event handle specific to the MenuEntry must be made
//The Entry must be added to the MenuEntries List
//Enumeration of game types needs to be hard coded -gw
namespace ExampleGame
{
    class GameTypeScreen : MenuScreen
    {
        public GameTypeScreen()
            : base("Game Type")
        {
            MenuEntry FreeRoam = new MenuEntry("Free Roam");
            MenuEntry FFA = new MenuEntry("Free For All");
            MenuEntry CTF = new MenuEntry("Capture The Flag");
            MenuEntry Exit = new MenuEntry("Cancel");

            FreeRoam.Selected += FreeRoamSelected;
            FFA.Selected += FFASelected;
            CTF.Selected += CTFSelected;
            Exit.Selected += CancelSelected;

            MenuEntries.Add(FreeRoam);
            MenuEntries.Add(FFA);
            MenuEntries.Add(CTF);
            MenuEntries.Add(Exit);
            
        }

        void FreeRoamSelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new HostSessionScreen(GameMode.SINGLE), e.PlayerIndex);
        }

        void FFASelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new HostSessionScreen(GameMode.FFA), e.PlayerIndex);
        }

        void CTFSelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new HostSessionScreen(GameMode.CAPTURETHEFLAG), e.PlayerIndex);
        }

        void CancelSelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.RemoveScreen(this);
        }

    }
}
