using UnityEngine;
using System.Collections;

public class ScoreWatcher : MonoBehaviour {
	public TextMesh scoreMesh = null;
	public int Score = 0;

	// Use this for initialization
	void Start () {
		scoreMesh = gameObject.GetComponent<TextMesh>();
		scoreMesh.text = "Score: " + Score;
		GridManager.AddScore += AddScore;
		GridManager.LowerScore += LowerScore;
		GridManager.ResetScore += ResetScore;
	}

	void AddScore()
	{
		Score++;
		scoreMesh.text = "Score: " + Score.ToString();
	}

	void LowerScore()
	{
		Score--;
		scoreMesh.text = "Score: " + Score.ToString();
	}

	void ResetScore()
	{
		Score = 0;
		scoreMesh.text = "Score: " + Score.ToString();
	}
}
