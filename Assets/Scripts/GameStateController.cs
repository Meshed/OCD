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
		HUDContainer.SetActive(false);

		if(menu)
		{
			MainMenuManager mainMenuManager = menu.GetComponent<MainMenuManager>();

			if(mainMenuManager)
			{
				CurrentGameDifficulty = mainMenuManager.GameDifficulty;
            }

            Destroy(menu);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch(CurrentGameState)
		{
			case GameState.Playing:
		        if (Application.platform == RuntimePlatform.Android)
		        {
		            if (Input.GetKey(KeyCode.Escape))
		            {
		                Application.LoadLevel("Menu");
		            }
		        }

				if(PlayerWon())
				{
					var gameOver = (GameObject) Instantiate(GameWonContainer);
					GameWonContainer.SetActive(true);
					HUDContainer.SetActive(false);
					CurrentGameState = GameState.GameOver;
					OnStateChange(GameState.GameOver);
				}
				else
				{
					HUDContainer.SetActive(true);
					var gameWonContainer = GameObject.FindGameObjectWithTag("GameWonWindow");
					if(gameWonContainer)
					{
						Destroy(gameWonContainer);
					}
				}
				break;
			case GameState.GameOver:
				if(!PlayerWon())
				{
					CurrentGameState = GameState.Playing;
				}
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
