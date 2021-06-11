
using UnityEngine;

[CreateAssetMenu(fileName = "ConsoleCommand", menuName = "Console/Help", order = 1)]
public class HelpCommand : ConsoleCommand
{
    public override void Execute(InGameLogUI inGameLog, string commandLine = "")
    {
        var commandList = inGameLog.GetCommandList();
        var helpText = "Command Format: \\*command* \n";
        helpText += " List of Possible Commands: \n";
        for (int i = 0; i < commandList.Count; i++)
        {
            helpText += commandList[i].GetCommandKey().Bolden() + ": " + commandList[i].GetDescription() + "\n";
        }
        inGameLog.ShowLog(helpText);
    }

    public override bool ParseCommand(string commandLine)
    {
        return base.ParseCommand(commandLine);
    }
}
