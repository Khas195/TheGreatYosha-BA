
using UnityEngine;

[CreateAssetMenu(fileName = "ConsoleCommand", menuName = "Console/Quit", order = 1)]
public class QuitCommand : ConsoleCommand
{
    public override void Execute(InGameLogUI inGameLog = null, string commandLine = "")
    {
        GameMaster.GetInstance().ExitGame();
    }
}
