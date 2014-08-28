using UnityEngine;
using System.Collections;

public class GameStateController : MonoBehaviour {
	public GameState CurrentGameState;
	public GridManager GridManager;

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
		Won,
		Lost
	}

	// Use this for initialization
	void Start () {
		CurrentGameState = GameState.Playing;
		GameObject menu = GameObject.Find("Menu");

		if(menu)
		{
			Debug.Log("Menu found");
			MainMenuManager mainMenuManager = menu.GetComponent<MainMenuManager>();

			if(mainMenuManager)
			{
				Debug.Log("Main Menu Manager found");
			}
			else
			{
				Debug.Log("Main Menu Manager not found");
			}
		}
		else
		{
			Debug.Log("Menu not found");
		}

		Destroy(menu);
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
		return GridManager.GameWon();
	}

	bool PlayerLost()
	{
		return false;
	}
}
