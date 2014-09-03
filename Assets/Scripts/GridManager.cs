﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridManager : MonoBehaviour {

	public GameObject[] GridNodes;
	public GameObject BlueDot = null;
	public GameObject GreenDot = null;
	public GameObject RedDot = null;
	public GameObject OrangeDot = null;
	public GameObject PurpleDot = null;
	public GameObject DotHightlight = null;
	public int MaxDotsPerColor = 1;
	public GameObject SelectedDot = null;
	public bool GridInOrder = false;

	private int _score = 0;


	// This is the list of colors we have available for dots
	public enum DotColor
	{
		Blue = 1,
		Green,
		Red,
		Orange,
		Purple,
		EnumLength
	}

	public delegate void ScoreHandler(int score);
	public static event ScoreHandler AdjustScore;
    public static event ScoreHandler ResetScore;
	public delegate void MoveHandler();
	public static event MoveHandler AddMove;
	public static event MoveHandler ResetMoves;

	void Start()
	{
		SetupTheGame();
	}
    // Update is called once per frame
    void Update()
    {
        // If no dot has been selected then we need to set the state for all of the Dots to Idle
        if (SelectedDot == null)
        {
            var dots = GetAllDots();

            foreach (var dot in dots)
            {
                DotStateController dotStateController = GetDotStateController(dot);

                if (dotStateController)
                {
                    dotStateController.OnStateChange(DotStateController.DotStates.Idle);
                }
            }
        }
    }

	public void Reset()
	{
		DestroyAllDots();
		SetupTheGame();
		ResetScoreAndMoves();
		GridInOrder = false;
	}
	/// <summary>
	/// Handles when a dot has been selected and handles the validation
	/// </summary>
	/// <param name="newSelectedDot">New selected dot.</param>
	public void DotSelected(GameObject newSelectedDot)
	{
		if(SelectedDot)
		{
			if(IsDotSelectionAllowed(newSelectedDot))
			{
				AddMove();
				DotStateController selectedDotStateController = GetDotStateController(SelectedDot);
				DotStateController newDotStateController = GetDotStateController(newSelectedDot);
				
				GameObject tempGridNode = selectedDotStateController.GridNode;
				selectedDotStateController.GridNode = newDotStateController.GridNode;
				newDotStateController.GridNode = tempGridNode;

				Vector2 tempGridLocation = selectedDotStateController.GridLocation;
				selectedDotStateController.GridLocation = newDotStateController.GridLocation;
				newDotStateController.GridLocation = tempGridLocation;

			    if (DotIsInHomeColumn(newSelectedDot) || DotIsInHomeColumn(SelectedDot))
			    {
			        AdjustScore(5);
			    }
			    else
			    {
			        AdjustScore(-1);
			    }
			}

			SelectedDot = null;
			Destroy(GameObject.FindGameObjectWithTag("DotHightlight"));
			GameWon();
		}
		else
		{
			SelectedDot = newSelectedDot;

			GameObject dotHightlight = (GameObject)Instantiate(DotHightlight);
			dotHightlight.transform.position = SelectedDot.transform.position;
		}
	
	}
	public void GameWon()
	{
		bool allInOrder = false;
		bool blueDotsComplete = false;
		bool greenDotsComplete = false;
		bool orangeDotsComplete = false;
		bool purpleDotsComplete = false;
		bool redDotsComplete = false;

		blueDotsComplete = AllDotsForColorInOrder(DotColor.Blue);
		greenDotsComplete = AllDotsForColorInOrder(DotColor.Green);
		redDotsComplete = AllDotsForColorInOrder(DotColor.Red);
		orangeDotsComplete = AllDotsForColorInOrder(DotColor.Orange);
		purpleDotsComplete = AllDotsForColorInOrder(DotColor.Purple);

		if(blueDotsComplete &&
		   greenDotsComplete &&
		   orangeDotsComplete &&
		   purpleDotsComplete &&
		   redDotsComplete)
		{
			allInOrder = true;
		}

		GridInOrder = allInOrder;
	}

    private bool DotIsInHomeColumn(GameObject dot)
    {
        bool result = false;
        DotStateController dotStateController = GetDotStateController(dot);

        if (dotStateController)
        {
            // Get color of current column
            DotColor dotColumnColor = (DotColor)dotStateController.GridLocation.x;
            // Compare current column color with dot color
            if (dotColumnColor == dotStateController.DotColor)
            {
                result = true;
            }
        }

        return result;
    }
	private bool AllDotsForColorInOrder(DotColor dotColor)
	{
		bool allDotsInOrder = false;
		int dotLocationX = 0;

		switch(dotColor)
		{
			case DotColor.Blue:
				dotLocationX = 1;
				break;
			case DotColor.Green:
				dotLocationX = 2;
				break;
			case DotColor.Red:
				dotLocationX = 3;
				break;
			case DotColor.Orange:
				dotLocationX = 4;
				break;
			case DotColor.Purple:
				dotLocationX = 5;
				break;
		}

		// Get all dots
		var dots = GetAllDots();
		// Cycle through all the grid dots
		foreach(GameObject dot in dots)
		{
			// Get the dot's controller
			DotStateController dotStateController = GetDotStateController(dot);

			if(dotStateController)
			{
				// Make sure the dot is in the same column as the color we are checking
				if(dotStateController.GridLocation.x == dotLocationX)
				{
					// Make sure the color of the dot is the same color we are checking for
					if(dotStateController.DotColor == dotColor)
					{
						allDotsInOrder = true;
					}
					else
					{
						allDotsInOrder = false;
						break;
					}
				}
			}
		}

		return allDotsInOrder;
	}
    private void SetupDotStateController(DotStateController dotStateController, int gridNodeIndex, DotColor dotColor)
    {
		dotStateController.DotColor = dotColor;
        dotStateController.GridLocation = GetGridLocation(gridNodeIndex);
        dotStateController.GridNode = GridNodes[gridNodeIndex];
        dotStateController.GridManager = this;
        dotStateController.OnStateChange(DotStateController.DotStates.Idle);
    }
    /// <summary>
	/// Sets the x and y location for the node the ball will spawn at
	/// </summary>
	/// <returns>The grid location: Vector2</returns>
	/// <param name="gridNode">Grid node.</param>
	private Vector2 GetGridLocation(int gridNode)
	{
		Vector2 gridLocation = new Vector2()
		{
			x = 0, 
			y = 0
		};
		
		if(gridNode < 5)
		{
			gridLocation.x = gridNode + 1;
			gridLocation.y = 1;
		}
		else if(gridNode > 4 && gridNode < 10)
		{
			gridLocation.x = (gridNode + 1) - 5;
			gridLocation.y = 2;
		}
		else if(gridNode > 9 && gridNode < 15)
		{
			gridLocation.x = (gridNode + 1) - 10;
			gridLocation.y = 3;
		}
		else if(gridNode > 14 && gridNode < 20)
		{
			gridLocation.x = (gridNode + 1) - 15;
			gridLocation.y = 4;
		}
		else if(gridNode > 19 && gridNode < 25)
		{
			gridLocation.x = (gridNode + 1) - 20;
			gridLocation.y = 5;
		}
		else if(gridNode > 24 && gridNode < 30)
		{
			gridLocation.x = (gridNode + 1) - 25;
			gridLocation.y = 6;
		}
		else if(gridNode > 29 && gridNode < 35)
		{
			gridLocation.x = (gridNode + 1) - 30;
			gridLocation.y = 7;
		}

		return gridLocation;
	}
	/// <summary>
	/// Gets the random color of the dot from the DotColor list
	/// </summary>
	/// <returns>The random dot color: DotColor</returns>
	private DotColor GetRandomDotColor()
	{
		int dotColor = Random.Range(1, (int)DotColor.EnumLength);
		
		return (DotColor)dotColor;
	}
	/// <summary>
	/// Gets all game objects with the tag of Dot
	/// </summary>
	/// <returns>Returns a List of GameObjects</returns>
	private IEnumerable<GameObject> GetAllDots()
	{
		return GameObject.FindGameObjectsWithTag("Dot");
	}
	/// <summary>
	/// Determines if the dot selection is allowed. There are different rules for each difficulty level.
	/// 	Normal: Any dot is allowed to be selected if there is no dot selected. Only a dot next to the 
	/// 		already selected dot is allowed.
	/// 	Difficult: A dot may not be selected from a locked column
	/// 	Hard: A dot may not be selected from a locked column or row
	/// </summary>
	/// <returns><c>true</c> if this instance is dot selection allowed the specified selectedDot; otherwise, <c>false</c>.</returns>
	/// <param name="selectedDot">Selected dot.</param>
	private bool IsDotSelectionAllowed(GameObject dot)
	{
		bool isSelectionAllowed = false;

		// TODO: Implement a strategy to support various levels of business rules, one for each difficulty
		if(SelectedDot == null)
			isSelectionAllowed = true;
		else
		{
			// Use the location of the Dot passed in. Any valid Dot should be 1 above, bellow, to
			// the left, or to the right of it.
			DotStateController newDotStateController = GetDotStateController(dot);
			DotStateController currentDotStateController = GetDotStateController(SelectedDot);


			isSelectionAllowed = IsNewDotOnTopOfOrBellowCurrentDot(newDotStateController.GridLocation, 
			                                                       currentDotStateController.GridLocation);

			if(isSelectionAllowed == false)
			{
				isSelectionAllowed = IsNewDotToLeftOfOrRightOfCurrentDot(newDotStateController.GridLocation, 
				                                                         currentDotStateController.GridLocation);
			}
		}

		return isSelectionAllowed;
	}
	/// <summary>
	/// Determines whether this instance is new dot on top of or bellow current dot the specified newDotGridLocation currentDotGridLocation.
	/// </summary>
	/// <returns><c>true</c> if this instance is new dot on top of or bellow current dot the specified newDotGridLocation
	/// currentDotGridLocation; otherwise, <c>false</c>.</returns>
	/// <param name="newDotGridLocation">New dot grid location.</param>
	/// <param name="currentDotGridLocation">Current dot grid location.</param>
	private bool IsNewDotOnTopOfOrBellowCurrentDot(Vector2 newDotGridLocation, Vector2 currentDotGridLocation)
	{
		bool result = false;

		if(newDotGridLocation.x == currentDotGridLocation.x)
		{
			if((newDotGridLocation.y == currentDotGridLocation.y + 1) ||
			   (newDotGridLocation.y == currentDotGridLocation.y - 1))
			{
				result = true;
			}
		}

		return result;
	}
	/// <summary>
	/// Determines whether this instance is new dot to left of or right of current dot the specified newDotGridLocation currentDotGridLocation.
	/// </summary>
	/// <returns><c>true</c> if this instance is new dot to left of or right of current dot the specified newDotGridLocation
	/// currentDotGridLocation; otherwise, <c>false</c>.</returns>
	/// <param name="newDotGridLocation">New dot grid location.</param>
	/// <param name="currentDotGridLocation">Current dot grid location.</param>
	private bool IsNewDotToLeftOfOrRightOfCurrentDot(Vector2 newDotGridLocation, Vector2 currentDotGridLocation)
	{
		bool result = false;
		
		if(newDotGridLocation.y == currentDotGridLocation.y)
		{
			if((newDotGridLocation.x == currentDotGridLocation.x + 1) ||
			   (newDotGridLocation.x == currentDotGridLocation.x - 1))
			{
				result = true;
			}
		}
		
		return result;
	}
    /// <summary>
    /// Gets the state controller component from the Game Object
    /// </summary>
    /// <returns>The dot state controller.</returns>
    /// <param name="dotGameObject">Game Object</param>
    private DotStateController GetDotStateController(GameObject dotGameObject)
	{
		DotStateController dotStateController = null;

		if(dotGameObject)
		{
			dotStateController = dotGameObject.GetComponent<DotStateController>();
		}

		return dotStateController;
	}
    /// <summary>
    /// This is where we setup the game board, create all of the dots, and assign them to nodes
    /// </summary>
    void SetupTheGame()
    {
        int blueDotSpawnCount = 0;
        int greenDotSpawnCount = 0;
        int redDotSpawnCount = 0;
        int orangeDotSpawnCount = 0;
        int purpleDotSpawnCount = 0;

        for (int gridNodeIndex = 0; gridNodeIndex < GridNodes.GetLength(0); gridNodeIndex++)
        {
            bool dotCreated = false;

            do
            {
                DotColor randomDotColor = GetRandomDotColor();

                switch (randomDotColor)
                {
                    case DotColor.Blue:
                        // Create a new prefab and assign node gridNodeIndex to it
                        if (blueDotSpawnCount < MaxDotsPerColor)
                        {
                            var blueDot = (GameObject)Instantiate(BlueDot);
                            DotStateController dotStateController = GetDotStateController(blueDot);

                            SetupDotStateController(dotStateController, gridNodeIndex, randomDotColor);

                            blueDotSpawnCount++;
                            dotCreated = true;
                        }
                        break;
                    case DotColor.Green:
                        if (greenDotSpawnCount < MaxDotsPerColor)
                        {
                            var greenDot = (GameObject)Instantiate(GreenDot);
                            DotStateController dotStateController = GetDotStateController(greenDot);

                            SetupDotStateController(dotStateController, gridNodeIndex, DotColor.Green);

                            greenDotSpawnCount++;
                            dotCreated = true;
                        }
                        break;
                    case DotColor.Red:
                        if (redDotSpawnCount < MaxDotsPerColor)
                        {
                            var redDot = (GameObject)Instantiate(RedDot);
                            DotStateController dotStateController = GetDotStateController(redDot);

                            SetupDotStateController(dotStateController, gridNodeIndex, DotColor.Red);

                            redDotSpawnCount++;
                            dotCreated = true;
                        }
                        break;
                    case DotColor.Orange:
                        if (orangeDotSpawnCount < MaxDotsPerColor)
                        {
                            var orangeDot = (GameObject)Instantiate(OrangeDot);
                            DotStateController dotStateController = GetDotStateController(orangeDot);

                            SetupDotStateController(dotStateController, gridNodeIndex, DotColor.Orange);

                            orangeDotSpawnCount++;
                            dotCreated = true;
                        }
                        break;
                    case DotColor.Purple:
                        if (purpleDotSpawnCount < MaxDotsPerColor)
                        {
                            var purpleDot = (GameObject)Instantiate(PurpleDot);
                            DotStateController dotStateController = GetDotStateController(purpleDot);

                            SetupDotStateController(dotStateController, gridNodeIndex, DotColor.Purple);

                            purpleDotSpawnCount++;
                            dotCreated = true;
                        }
                        break;
                }
            } while (!dotCreated);
        }
    }
    void DestroyAllDots()
    {
        var dots = GetAllDots();

        foreach (var dot in dots)
        {
            Destroy(dot);
        }
    }
    void ResetScoreAndMoves()
    {
        ResetScore(0);
        ResetMoves();
    }
}
