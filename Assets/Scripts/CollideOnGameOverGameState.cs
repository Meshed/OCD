using UnityEngine;
using System.Collections;

public class CollideOnGameOverGameState : MonoBehaviour {
	GameStateController.GameState CurrentGameState;
	
	void OnEnable()
	{
		GameStateController.OnStateChange += OnGameStateChanged;
	}
	
	void OnDisable()
	{
		GameStateController.OnStateChange -= OnGameStateChanged;
	}
	
	void OnGameStateChanged(GameStateController.GameState newGameState)
	{
		CurrentGameState = newGameState;
	}
	
	// Update is called once per frame
	void Update () {
		if(CurrentGameState == GameStateController.GameState.GameOver)
		{
			collider2D.enabled = true;
		}
		else
		{
			collider2D.enabled = false;
		}
	}
}
