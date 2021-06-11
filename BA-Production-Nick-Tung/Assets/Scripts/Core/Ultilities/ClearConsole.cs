
using UnityEngine;

[CreateAssetMenu(fileName = "ConsoleCommand", menuName = "Console/Clear", order = 1)]
public class ClearConsole : ConsoleCommand
{
    public override void Execute(InGameLogUI inGameLog, string commandLine = "")
    {
        inGameLog.ClearConsole();
    }

    public override bool ParseCommand(string commandLine)
    {
        return base.ParseCommand(commandLine);
    }
}