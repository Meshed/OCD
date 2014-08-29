using UnityEngine;
using System.Collections;

public class GameStateListener : MonoBehaviour {
	public GameStateController.GameState _currentGameState;

	// Use this for initialization
	void Start () {
	
	}

	void LateUpdate()
	{
		OnStateCycle();
	}

	void OnStateCycle()
	{

	}

	void OnStateChange(GameStateController.GameState newGameState)
	{
		if(newGameState == _currentGameState)
			return;

		if(!GameStateChangeAllowed(newGameState))
		{
			return;
		}

		switch(newGameState)
		{
			case GameStateController.GameState.Playing:
				break;
			case GameStateController.GameState.GameOver:
				break;
			case GameStateController.GameState.Quit:
				break;
		}

		_currentGameState = newGameState;
	}

	bool GameStateChangeAllowed(GameStateController.GameState newGameState)
	{
		bool stateChangeAllowed = false;

		switch(_currentGameState)
		{
			case GameStateController.GameState.GameOver:
				if(newGameState == GameStateController.GameState.Playing)
					stateChangeAllowed = true;
				break;
			case GameStateController.GameState.Playing:
				if(newGameState == GameStateController.GameState.GameOver ||
			   		newGameState == GameStateController.GameState.Quit)
					stateChangeAllowed = true;
				break;
			case GameStateController.GameState.Quit:
				if(newGameState == GameStateController.GameState.GameOver)
					stateChangeAllowed = true;
				break;
		}

		return stateChangeAllowed;
	}
}
