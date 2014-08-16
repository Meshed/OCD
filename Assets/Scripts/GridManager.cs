using UnityEngine;
using System.Collections;

public class GridManager : MonoBehaviour {

	public GameObject[] GridNodes;
	public GameObject BlueDot = null;
	public GameObject GreenDot = null;
	public GameObject RedDot = null;
	public int MaxDotsPerColor = 1;

	private int blueDotSpawnCount = 0;
	private int greenDotSpawnCount = 0;
	private int redDotSpawnCount = 0;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < GridNodes.GetLength(0); i++)
		{
			bool dotCreated = false;

			do
			{
				int spawnColor = Random.Range(1, 4);

				switch(spawnColor)
				{
				case 1:
					// Create a new prefab and assign node i to it
					if(blueDotSpawnCount < MaxDotsPerColor)
					{
						GameObject blueDot = (GameObject)Instantiate(BlueDot);
						DotStateController dotStateController = blueDot.GetComponent<DotStateController>();

						dotStateController.GridNode = GridNodes[i];
						dotStateController.OnStateChange(DotStateController.DotStates.Idle);
						
						blueDotSpawnCount++;
						dotCreated = true;
					}
					break;
				case 2:
					if(greenDotSpawnCount < MaxDotsPerColor)
					{
						GameObject greenDot = (GameObject)Instantiate(GreenDot);
						DotStateController dotStateController = greenDot.GetComponent<DotStateController>();

						dotStateController.GridNode = GridNodes[i];
						dotStateController.OnStateChange(DotStateController.DotStates.Idle);
						
						greenDotSpawnCount++;
						dotCreated = true;
					}
					break;
				case 3:
					if(redDotSpawnCount < MaxDotsPerColor)
					{
						GameObject redDot = (GameObject)Instantiate(RedDot);
						DotStateController dotStateController = redDot.GetComponent<DotStateController>();

						dotStateController.GridNode = GridNodes[i];
						dotStateController.OnStateChange(DotStateController.DotStates.Idle);

						redDotSpawnCount++;
						dotCreated = true;
					}
					break;
				}
			}while(!dotCreated);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
