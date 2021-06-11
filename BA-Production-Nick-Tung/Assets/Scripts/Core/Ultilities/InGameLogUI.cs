using System;
using System.Collections.Generic;
using System.Diagnostics;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class InGameLogUI : SingletonMonobehavior<InGameLogUI>, IObserver
{
    [SerializeField]
    [Required]
    Text consoleText = null;
    [SerializeField]
    [Required]
    ScrollRect consoleScrollRect = null;
    [SerializeField]
    [Required]
    InputField controlInputField = null;
    [SerializeField]
    List<ConsoleCommand> possibleCommands = new List<ConsoleCommand>();



    protected override void Awake()
    {
        base.Awake();
        PostOffice.Subscribes(this, GameMasterEvent.InGameLogConsoleEvent.CONSOLE_SWITCH_ON_OFF);
    }
    void OnDestroy()
    {
        PostOffice.Unsubscribes(this, GameMasterEvent.InGameLogConsoleEvent.CONSOLE_SWITCH_ON_OFF);
    }
    void Start()
    {
        TurnOff();
    }
    public void ShowLog(string log)
    {
        var currentTime = System.DateTime.Now;
        consoleText.text += "\n" + "[" + currentTime.Hour + ":" + currentTime.Minute + ":" + currentTime.Second + "] - " + log + ".";

        consoleScrollRect.verticalNormalizedPosition = -1;
    }

    public void InputCommand(string command)
    {
        if (command == "")
        {
            controlInputField.DeactivateInputField();
            return;
        }

        bool recognizedCommand = false;
        for (int i = 0; i < this.possibleCommands.Count; i++)
        {
            if (this.possibleCommands[i].ParseCommand(command))
            {
                recognizedCommand = true;
                this.possibleCommands[i].Execute(this, command);
                break;
            }
        }

        if (recognizedCommand == false)
        {
            ShowLog("Unrecognized command");
        }

        controlInputField.text = "";
        controlInputField.Select();
        controlInputField.ActivateInputField();

    }
    public void ClearConsole()
    {
        consoleText.text = "";
    }
    private bool ProcessCommand(string command)
    {
        var words = command.Split(' ');
        var commandSignal = words[0].ToLower();
        if (commandSignal.Equals("command") == false) return false;
        var commandType = words[1].ToLower();
        if (commandType.Equals("clear"))
        {
            consoleText.text = "";
        }
        else
        {
            return false;
        }
        return true;
    }

    public void ReceiveData(DataPack pack, string eventName)
    {
        if (eventName.Equals(GameMasterEvent.InGameLogConsoleEvent.CONSOLE_SWITCH_ON_OFF))
        {
            if (this.gameObject.activeSelf)
            {
                TurnOff();
            }
            else
            {
                TurnOn();
            }

        }
        if (eventName.Equals(GameMasterEvent.ON_GAMESTATE_CHANGED))
        {
            var newState = pack.GetValue<GameState.GameStateEnum>(valueName: GameMasterEvent.GameStateChangeEvent.New_Game_State);
            // do something with newState
        }
    }

    private void TurnOn()
    {
        this.gameObject.SetActive(true);
    }

    private void TurnOff()
    {
        this.gameObject.SetActive(false);
    }

    public List<ConsoleCommand> GetCommandList()
    {
        return this.possibleCommands;
    }
}
