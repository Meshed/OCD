using UnityEngine;
using System.Collections;

public class MoveWatcher : MonoBehaviour {
	private int _moves = 0;
	private TextMesh moveMesh = null;

	// Use this for initialization
	void Start () {
		moveMesh = gameObject.GetComponent<TextMesh>();
		moveMesh.text = "Moves: 0";
		GridManager.AddMove += AddMove;
	}

	void AddMove()
	{
		_moves++;
		moveMesh.text = "Moves: " + _moves.ToString();
	}
}
