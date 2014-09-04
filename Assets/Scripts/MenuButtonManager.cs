using UnityEngine;

public class MenuButtonManager : MonoBehaviour {
	public GameStateController.GameDifficulty GameDifficulty;
    public AudioClip MainMenuSelect;

	void OnMouseDown()
	{
		var mainMenuManager = transform.parent.GetComponent<MainMenuManager>();
		mainMenuManager.SetGameDifficulty(GameDifficulty);

	    audio.PlayOneShot(MainMenuSelect);

		Application.LoadLevel("Game");
	}
}
