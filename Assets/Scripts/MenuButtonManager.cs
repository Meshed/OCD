using UnityEngine;
using System.Collections;

public class MenuButtonManager : MonoBehaviour {
	public GameStateController.GameDifficulty GameDifficulty;

	void OnMouseDown()
	{
		MainMenuManager mainMenuManager = transform.parent.GetComponent<MainMenuManager>();
		mainMenuManager.SetGameDifficulty(GameDifficulty);

		Application.LoadLevel("Game");
	}
}
