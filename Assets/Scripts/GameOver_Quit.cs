using UnityEngine;
using System.Collections;

public class GameOver_Quit : MonoBehaviour {
	void OnMouseDown()
	{
		Application.LoadLevel("Menu");
	}
}
