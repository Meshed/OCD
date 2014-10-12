using UnityEngine;

public class MenuButtonManager : MonoBehaviour {
	public GameStateController.GameDifficulty GameDifficulty;
    public AudioClip MainMenuSelect;

	void OnMouseDown()
	{
		switch(this.name)
		{
			case "MainMenu_Credits":
				HandleCreditClick();
				break;
			case "MainMenu_Instructions":
				HandleInstructionsClick();
				break;
			case "MainMenu_Exit":
				Application.Quit();
				break;
			case "Credits_Back":
			case "Instructions_Back":
			case "Game_Back":
				HandleBackClick();
				break;
			default:
				HandleGameDifficultyClick();
				break;
		}
	}

	private void HandleGameDifficultyClick()
	{
		var mainMenuManager = transform.parent.GetComponent<MainMenuManager>();
		mainMenuManager.SetGameDifficulty(GameDifficulty);
		
		audio.PlayOneShot(MainMenuSelect);
		
		Application.LoadLevel("Game");
	}

	private void HandleCreditClick()
	{
		audio.PlayOneShot(MainMenuSelect);
		Application.LoadLevel("Credits");
	}

	private void HandleInstructionsClick()
	{
		audio.PlayOneShot(MainMenuSelect);
		Application.LoadLevel("Instructions");
	}

	private void HandleBackClick()
	{
		audio.PlayOneShot(MainMenuSelect);
		Application.LoadLevel("Menu");
	}
}
