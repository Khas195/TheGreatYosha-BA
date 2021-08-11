
using UnityEngine;

[CreateAssetMenu(fileName = "ConsoleCommand", menuName = "Console/UpdateScenario", order = 1)]
public class UpdateScenarioCommand : ConsoleCommand
{
	public override void Execute(InGameLogUI inGameLog = null, string commandLine = "")
	{
		GameMaster.GetInstance().UpdateScenario();
	}
}
