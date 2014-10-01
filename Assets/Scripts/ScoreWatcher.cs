using System.Runtime.CompilerServices;
using UnityEngine;

public class ScoreWatcher : MonoBehaviour {
	public TextMesh scoreMesh = null;
	public int Score = 0;

	// Use this for initialization
	void Start () {
		scoreMesh = gameObject.GetComponent<TextMesh>();
		scoreMesh.text = "Score: " + Score;
	    GridManager.AdjustScore += AdjustScore;
	    GridManager.ResetScore += ResetScore;
	}

	void AdjustScore(int score)
	{
        Score += score;
        UpdateScoreDisplay();
	}

    void ResetScore(int score)
    {
        Score = score;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        scoreMesh.text = "Score: " + Score;
    }
}
