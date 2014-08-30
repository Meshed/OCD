using UnityEngine;
using System.Collections;

public class GameStateController : MonoBehaviour {
	public GameState CurrentGameState;
	public GridManager GridManager;
	public GameObject GameWonContainer;
	public GameObject HUDContainer;

	public delegate void GameStateHandler(GameStateController.GameState newGameState);
	public static event GameStateHandler OnStateChange;
	public GameDifficulty CurrentGameDifficulty;

	public enum GameDifficulty
	{
		Easy,
		Normal,
		Hard,
		IceCream
	}

	public enum GameState
	{
		Playing,
		GameOver,
		Quit
	}

	// Use this for initialization
	void Start () {
		CurrentGameState = GameState.Playing;
		GameObject menu = GameObject.Find("Menu");
		GameWonContainer.SetActive(false);
		HUDContainer.SetActive(false);

		if(menu)
		{
			MainMenuManager mainMenuManager = menu.GetComponent<MainMenuManager>();

			if(mainMenuManager)
			{
				CurrentGameDifficulty = mainMenuManager.GameDifficulty;
			}
		}

		Destroy(menu);
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch(CurrentGameState)
		{
			case GameState.Playing:
			if(PlayerWon())
			{
				GameWonContainer.SetActive(true);
				OnStateChange(GameState.GameOver);
			}
			else if(PlayerLost())
			{
				GameWonContainer.SetActive(true);
				OnStateChange(GameState.GameOver);
			}
			else
			{
				HUDContainer.SetActive(true);
			}
			break;
			case GameState.GameOver:
				break;
			case GameState.Quit:
				break;
		}
	}

	bool PlayerWon()
	{
		return GridManager.GridInOrder;
	}

	bool PlayerLost()
	{
		return false;
	}
}
