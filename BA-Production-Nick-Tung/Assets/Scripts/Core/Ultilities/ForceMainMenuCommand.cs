
using UnityEngine;
[CreateAssetMenu(fileName = "ConsoleCommand", menuName = "Console/ForceMainMenu", order = 1)]

public class ForceMainMenuCommand : ConsoleCommand
{
	[SerializeField]
	GameInstance mainMenu = null;
	public override void Execute(InGameLogUI inGameLog = null, string commandLine = "")
	{
		GameMaster.GetInstance().RequestInstance(mainMenu);
	}
}