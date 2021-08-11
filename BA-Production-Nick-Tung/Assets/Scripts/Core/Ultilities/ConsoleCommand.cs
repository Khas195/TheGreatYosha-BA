using NaughtyAttributes;
using UnityEngine;

public abstract class ConsoleCommand : ScriptableObject
{
	[SerializeField]
	protected string commandKey = "";
	[SerializeField]
	[TextArea()]
	protected string description = "";
	public virtual bool ParseCommand(string commandLine)
	{
		var words = commandLine.Split(' ');


		var commandType = words[0].ToLower();

		if (commandType.Equals(this.commandKey.ToLower()))
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public string GetDescription()
	{
		return description;
	}

	public string GetCommandKey()
	{
		return commandKey;
	}

	public abstract void Execute(InGameLogUI inGameLog = null, string commandLine = "");
}
