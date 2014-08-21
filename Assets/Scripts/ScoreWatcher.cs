using UnityEngine;
using System.Collections;

public class ScoreWatcher : MonoBehaviour {
	public TextMesh scoreMesh = null;
	public int _score = 0;

	// Use this for initialization
	void Start () {
		scoreMesh = gameObject.GetComponent<TextMesh>();
		scoreMesh.text = "Score: " + _score;
		GridManager.AddScore += AddScore;
		GridManager.LowerScore += LowerScore;
	}

	void AddScore()
	{
		_score++;
		scoreMesh.text = "Score: " + _score.ToString();
	}

	void LowerScore()
	{
		_score--;
		scoreMesh.text = "Score: " + _score.ToString();
	}
}
