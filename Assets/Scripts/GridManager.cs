using UnityEngine;
using System.Collections;

public class GridManager : MonoBehaviour {

	public GameObject[] GridNodes;
	public GameObject BlueDot = null;
	public GameObject GreenDot = null;
	public GameObject RedDot = null;
	public GameObject DotHightlight = null;
	public int MaxDotsPerColor = 1;
	public GameObject selectedDot = null;

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
						dotStateController.GridManager = this;
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
						dotStateController.GridManager = this;
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
						dotStateController.GridManager = this;
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
	void Update () 
	{
		if(selectedDot == null)
		{
			var dots = GameObject.FindGameObjectsWithTag("Dot");

			foreach(var dot in dots)
			{
				DotStateController dotStateController = dot.GetComponent<DotStateController>();

				if(dotStateController)
				{
					dotStateController.OnStateChange(DotStateController.DotStates.Idle);
				}
			}
		}
	}

	public void DotSelected(GameObject newSelectedDot)
	{
		if(selectedDot)
		{
			DotStateController selectedDotStateController = selectedDot.GetComponent<DotStateController>();
			DotStateController newDotStateController = newSelectedDot.GetComponent<DotStateController>();

			GameObject tempGridNode = selectedDotStateController.GridNode;
			selectedDotStateController.GridNode = newDotStateController.GridNode;
			newDotStateController.GridNode = tempGridNode;
			selectedDot = null;
			Destroy(GameObject.FindGameObjectWithTag("DotHightlight"));
		}
		else
		{
			selectedDot = newSelectedDot;

			GameObject dotHightlight = (GameObject)Instantiate(DotHightlight);
			dotHightlight.transform.position = selectedDot.transform.position;
		}
	}
}
