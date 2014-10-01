using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {
	public GameStateController.GameDifficulty GameDifficulty;

	void Awake () {
		DontDestroyOnLoad(this);
	}

	public void SetGameDifficulty(GameStateController.GameDifficulty gameDifficulty)
	{
		GameDifficulty = gameDifficulty;
	}

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
