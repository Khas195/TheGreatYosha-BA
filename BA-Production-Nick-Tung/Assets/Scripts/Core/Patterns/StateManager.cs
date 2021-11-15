using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class StateManager : MonoBehaviour
{
	[SerializeField]
	[ReadOnly]
	State currentState = null;
	[SerializeField]
	[ReadOnly]
	List<State> availableStates = new List<State>();


	void Start()
	{
		currentState = null;
		FindAvailableStates();

	}
	[Button("Find Available States")]
	private void FindAvailableStates()
	{
		LogHelper.Log(this + " clearing available states");
		availableStates.Clear();

		LogHelper.Log(this + " finding available states in children");
		var states = this.GetComponentsInChildren<State>();

		if (states.Length <= 0)
		{
			LogHelper.LogWarning(this + " has no available states to operate");
			return;
		}

		LogHelper.Log(this + " adding available states in children");
		availableStates.AddRange(states);

	}
	public bool RequestState(Enum requestedStateEnum)
	{
		var requestedState = LookUpAvailableState(requestedStateEnum);
		if (requestedState == null)
		{
			LogHelper.LogWarning(this + " trying to request a NULL state");
			return false;
		}

		if (currentState == null)
		{
			CompleteTransition(ref currentState, requestedState);
			return true;
		}

		if (currentState.CanTransitionTo(requestedState.GetEnum()))
		{
			CompleteTransition(ref currentState, requestedState);
			return true;
		}
		else
		{
			LogHelper.LogWarning(this + " trying to make an unsupported transition to " + requestedState);
			return false;
		}
	}

	public State LookUpAvailableState(Enum requestedStateEnum)
	{
		LogHelper.Log(this + " looking up available states for " + requestedStateEnum);
		State result = null;
		for (int i = 0; i < availableStates.Count; i++)
		{
			var curEnum = availableStates[i].GetEnum();
			if (curEnum.Equals(requestedStateEnum))
			{
				result = availableStates[i];
				break;
			}
		}
		if (result)
		{
			LogHelper.Log(this + " found a match for " + requestedStateEnum);
		}
		else
		{

			LogHelper.LogWarning(this + " CANNOT found a match for " + requestedStateEnum);
		}
		return result;
	}

	private void CompleteTransition(ref State currentState, State requestedState)
	{
		if (currentState)
		{
			LogHelper.Log(this + " call State exit for " + currentState);
			currentState.OnStateExit();
		}
		if (requestedState)
		{
			LogHelper.Log(this + " call State enter for " + requestedState);
			requestedState.OnStateEnter();
		}
		LogHelper.Log(this + " completed transition from " + currentState + " to " + requestedState);
		currentState = requestedState;
	}
	public State GetCurrentState()
	{
		return currentState;
	}
	public T GetCurrentState<T>() where T : State
	{
		return (T)currentState;
	}
	public bool IsCurrentState(Enum stateEnum)
	{
		return currentState.GetEnum().Equals(stateEnum);
	}
}
