using UnityEngine;
using System.Collections;

public class GameOver_New : MonoBehaviour {

	void OnMouseDown()
	{
		GameObject grid = GameObject.Find("Grid");

		if(grid)
		{
			var gridManager = grid.GetComponent<GridManager>();
			
			if(gridManager)
			{
				gridManager.Reset();
			}
		}
	}
}
