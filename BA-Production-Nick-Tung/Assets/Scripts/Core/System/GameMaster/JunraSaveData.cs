public class JunraSaveData
{
	public GameScenario currentScenario = null;
	public string databaseData;
	public bool IsValid()
	{
		return currentScenario != null && databaseData != string.Empty;
	}
}

