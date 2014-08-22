using UnityEngine;
using System.Collections;

public class GameStateController : MonoBehaviour {
	public GameState CurrentGameState;
	public GridManager GridManager;

	public delegate void GameStateHandler(GameStateController.GameState newGameState);
	public static event GameStateHandler OnStateChange;

	public enum GameState
	{
		Playing,
		Won,
		Lost
	}

	// Use this for initialization
	void Start () {
		CurrentGameState = GameState.Playing;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(PlayerWon())
		{
			OnStateChange(GameState.Won);
		}
		else if(PlayerLost())
		{
			OnStateChange(GameState.Lost);
		}
	}

	bool PlayerWon()
	{
		return false;
	}

	bool PlayerLost()
	{
		return false;
	}
}
