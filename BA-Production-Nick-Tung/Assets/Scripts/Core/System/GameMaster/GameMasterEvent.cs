public static class GameMasterEvent
{
	public static string ON_LOAD_NEW_STANCE_START = "ON_LOAD_NEW_INSTANCE";
	public static string ON_GAMESTATE_CHANGED = "ON_GAMESTATE_CHANGED";
	public static string ON_INSTANCE_LOADED = "GAME_LOAD_EVENT";
	public struct GameStateChangeEvent
	{
		public static string New_Game_State = "GameState";
	}
	public static class InGameLogConsoleEvent
	{
		public static string CONSOLE_SWITCH_ON_OFF = "CONSOLE_SWITCH";
	}
}

