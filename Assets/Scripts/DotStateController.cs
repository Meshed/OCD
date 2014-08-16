using UnityEngine;
using System.Collections;

public class DotStateController : MonoBehaviour {
	public enum DotStates
	{
		Idle = 0,
		Selected,
		Moving,
		Locked
	}

	public DotStates currentState = DotStates.Idle;
	public GameObject GridNode = null;
	public GridManager GridManager = null;

	void OnMouseDown()
	{
		switch(currentState)
		{
			case DotStateController.DotStates.Idle:
				OnStateChange(DotStateController.DotStates.Selected);
				break;
			case DotStateController.DotStates.Selected:
				OnStateChange(DotStateController.DotStates.Idle);
				break;
			case DotStates.Moving:
				break;
			case DotStates.Locked:
				break;
		}
	}

	void LateUpdate()
	{
		OnStateCycle();
	}

	void OnStateCycle()
	{
		switch (currentState) 
		{
			case DotStates.Idle:
				if(GridNode)
				{
					transform.position = GridNode.transform.position;
				}
				break;
			case DotStates.Moving:
				break;
			case DotStates.Selected:
				break;
			case DotStates.Locked:
				break;
		}
	}

	public void OnStateChange(DotStateController.DotStates newState)
	{
		if(newState == currentState)
			return;
		
		switch(newState)
		{
			case DotStateController.DotStates.Idle:
				break;
			case DotStateController.DotStates.Selected:
				GridManager.DotSelected(gameObject);
			break;
			case DotStates.Moving:
				break;
			case DotStates.Locked:
				break;
		}
		
		currentState = newState;
	}
}
