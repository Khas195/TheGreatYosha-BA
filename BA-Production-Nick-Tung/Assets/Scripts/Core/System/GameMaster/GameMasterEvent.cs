public static class GameMasterEvent
{
	public static string ON_GAMESTATE_CHANGED = "ON_GAMESTATE_CHANGED";
	public static string GAME_LOAD_EVENT = "GAME_LOAD_EVENT";
	public struct GameStateChangeEvent
	{
		public static string New_Game_State = "GameState";
	}
	public static class InGameLogConsoleEvent
	{
		public static string CONSOLE_SWITCH_ON_OFF = "CONSOLE_SWITCH";
	}
}

