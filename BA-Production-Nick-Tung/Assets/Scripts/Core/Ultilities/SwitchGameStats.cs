
using UnityEngine;

[CreateAssetMenu(fileName = "ConsoleCommand", menuName = "Console/SwitchGameStats", order = 1)]
public class SwitchGameStats : ConsoleCommand
{
    public static string SWITCH_GAME_STATS_EVENT = "SWITCH_GAME_STATS";
    public static bool STATS_ON = true;

    public override void Execute(InGameLogUI inGameLog = null, string commandLine = "")
    {
        STATS_ON = !STATS_ON;
        PostOffice.SendData(null, SWITCH_GAME_STATS_EVENT);
    }
}