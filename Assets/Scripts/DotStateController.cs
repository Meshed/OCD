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

	public DotStates CurrentState = DotStates.Idle;
	public GameObject GridNode = null;
	public GridManager GridManager = null;
	public Vector2 GridLocation;
	public GridManager.DotColor DotColor;
	public GameStateController.GameState CurrentGameState;

    public bool IsLocked { get; set; }

	void OnEnable()
	{
		GameStateController.OnStateChange += OnGameStateChanged;
	}

	void OnDisable()
	{
		GameStateController.OnStateChange -= OnGameStateChanged;
	}

	void OnMouseDown()
	{
		if(CurrentGameState == GameStateController.GameState.GameOver)
		{
			return;
		}

		switch(CurrentState)
		{
			case DotStates.Idle:
				OnStateChange(DotStates.Selected);
				break;
			case DotStates.Selected:
				GridManager.DotSelected(gameObject);
				OnStateChange(DotStates.Idle);
				break;
			case DotStates.Moving:
				break;
			case DotStates.Locked:
				break;
		}
	}

	void Update()
	{
		if(IsLocked)
		{
			GetComponent<SpriteRenderer>().color = Color.black;
		}
	}

	void LateUpdate()
	{
		OnStateCycle();
		OnGameStateCycle();
	}

    void OnGameStateChanged(GameStateController.GameState newGameState)
    {
        CurrentGameState = newGameState;
    }
    void OnGameStateCycle()
    {
        if (CurrentGameState == GameStateController.GameState.GameOver)
        {
            if (collider2D)
                collider2D.enabled = false;
        }
        else
        {
            if (collider2D)
                collider2D.enabled = true;
        }
    }
    void OnStateCycle()
	{
		switch (CurrentState) 
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
		if(newState == CurrentState)
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
		
		CurrentState = newState;
	}
}
