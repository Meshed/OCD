using UnityEngine;
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
    public AudioClip ValidMove = null;
    public AudioClip InvalidMove = null;
    public GameStateController GameStateController = null;

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

    private readonly DotService _dotService = new DotService();

	void Start()
	{
		SetupTheGame();
	}
    void Update()
    {
        // If no dot has been selected then we need to set the state for all of the Dots to Idle
        if (SelectedDot == null)
        {
            _dotService.SetAllDotsIdle();
        }
    }
    void OnEnable()
    {
        SwipeDetector.OnDotSwipe += OnDotSwipe;
    }
    void OnDisable()
    {
        SwipeDetector.OnDotSwipe -= OnDotSwipe;
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
                            DotStateController dotStateController = _dotService.GetDotStateController(blueDot);

                            SetupDotStateController(dotStateController, gridNodeIndex, randomDotColor);

                            blueDotSpawnCount++;
                            dotCreated = true;
                        }
                        break;
                    case DotColor.Green:
                        if (greenDotSpawnCount < MaxDotsPerColor)
                        {
                            var greenDot = (GameObject)Instantiate(GreenDot);
                            DotStateController dotStateController = _dotService.GetDotStateController(greenDot);

                            SetupDotStateController(dotStateController, gridNodeIndex, DotColor.Green);

                            greenDotSpawnCount++;
                            dotCreated = true;
                        }
                        break;
                    case DotColor.Red:
                        if (redDotSpawnCount < MaxDotsPerColor)
                        {
                            var redDot = (GameObject)Instantiate(RedDot);
                            DotStateController dotStateController = _dotService.GetDotStateController(redDot);

                            SetupDotStateController(dotStateController, gridNodeIndex, DotColor.Red);

                            redDotSpawnCount++;
                            dotCreated = true;
                        }
                        break;
                    case DotColor.Orange:
                        if (orangeDotSpawnCount < MaxDotsPerColor)
                        {
                            var orangeDot = (GameObject)Instantiate(OrangeDot);
                            DotStateController dotStateController = _dotService.GetDotStateController(orangeDot);

                            SetupDotStateController(dotStateController, gridNodeIndex, DotColor.Orange);

                            orangeDotSpawnCount++;
                            dotCreated = true;
                        }
                        break;
                    case DotColor.Purple:
                        if (purpleDotSpawnCount < MaxDotsPerColor)
                        {
                            var purpleDot = (GameObject)Instantiate(PurpleDot);
                            DotStateController dotStateController = _dotService.GetDotStateController(purpleDot);

                            SetupDotStateController(dotStateController, gridNodeIndex, DotColor.Purple);

                            purpleDotSpawnCount++;
                            dotCreated = true;
                        }
                        break;
                }
            } while (!dotCreated);
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
            MoveValidationManager moveValidationManager = new MoveValidationManager(SelectedDot, GameStateController.CurrentGameDifficulty);

			if(moveValidationManager.IsDotSelectionAllowed(newSelectedDot,ref SelectedDot))
			{
				AddMove();
				DotStateController selectedDotStateController = _dotService.GetDotStateController(SelectedDot);
				DotStateController newDotStateController = _dotService.GetDotStateController(newSelectedDot);
				
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

                audio.PlayOneShot(ValidMove, 1);
			}
			else
			{
			    audio.PlayOneShot(InvalidMove, 1);
			}

            LockDots(newSelectedDot);
            SelectedDot = null;
			Destroy(GameObject.FindGameObjectWithTag("DotHightlight"));
			GameWon();
		}
		else
		{
			SelectedDot = newSelectedDot;

			var dotHightlight = (GameObject)Instantiate(DotHightlight);
			dotHightlight.transform.position = SelectedDot.transform.position;
		}
	
	}
    public void GameWon()
    {
        bool allInOrder = false;

        bool blueDotsComplete = AllDotsForColorInOrder(DotColor.Blue);
        bool greenDotsComplete = AllDotsForColorInOrder(DotColor.Green);
        bool redDotsComplete = AllDotsForColorInOrder(DotColor.Red);
        bool orangeDotsComplete = AllDotsForColorInOrder(DotColor.Orange);
        bool purpleDotsComplete = AllDotsForColorInOrder(DotColor.Purple);

        if (blueDotsComplete &&
           greenDotsComplete &&
           orangeDotsComplete &&
           purpleDotsComplete &&
           redDotsComplete)
        {
            allInOrder = true;
        }

        GridInOrder = allInOrder;
    }

	private void LockDots(GameObject dot)
	{

		switch(GameStateController.CurrentGameDifficulty)
		{
			case GameStateController.GameDifficulty.Easy:
				break;
			case GameStateController.GameDifficulty.Normal:
                LockDotsForNormalDifficulty(dot);
				break;
			case GameStateController.GameDifficulty.Hard:
				break;
			case GameStateController.GameDifficulty.IceCream:
				break;
		}
	}

    private void LockDotsForNormalDifficulty(GameObject newDot)
    {
        DotStateController newDotStateController = _dotService.GetDotStateController(newDot);
        DotStateController selectedDotStateController = _dotService.GetDotStateController(SelectedDot);

        bool newDotColorInOrder = AllDotsForColorInOrder(newDotStateController.DotColor);
        bool selectedDotColorInOrder = AllDotsForColorInOrder(selectedDotStateController.DotColor);

        if (newDotColorInOrder || selectedDotColorInOrder)
        {
            if (newDotColorInOrder)
            {
                LockColumn(newDot);
            }
            else if (selectedDotColorInOrder)
            {
                LockColumn(SelectedDot);
            }

            LockColumn(newDotColorInOrder ? newDot : SelectedDot);
        }
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
    private void SetupDotStateController(DotStateController dotStateController, int gridNodeIndex, DotColor dotColor)
    {
        dotStateController.DotColor = dotColor;
        dotStateController.GridLocation = GetGridLocation(gridNodeIndex);
        dotStateController.GridNode = GridNodes[gridNodeIndex];
        dotStateController.GridManager = this;
        dotStateController.IsLocked = false;
        dotStateController.OnStateChange(DotStateController.DotStates.Idle);
    }
    /// <summary>
    /// Sets the x and y location for the node the ball will spawn at
    /// </summary>
    /// <returns>The grid location: Vector2</returns>
    /// <param name="gridNode">Grid node.</param>
    private Vector2 GetGridLocation(int gridNode)
    {
        var gridLocation = new Vector2()
        {
            x = 0,
            y = 0
        };

        if (gridNode < 5)
        {
            gridLocation.x = gridNode + 1;
            gridLocation.y = 1;
        }
        else if (gridNode > 4 && gridNode < 10)
        {
            gridLocation.x = (gridNode + 1) - 5;
            gridLocation.y = 2;
        }
        else if (gridNode > 9 && gridNode < 15)
        {
            gridLocation.x = (gridNode + 1) - 10;
            gridLocation.y = 3;
        }
        else if (gridNode > 14 && gridNode < 20)
        {
            gridLocation.x = (gridNode + 1) - 15;
            gridLocation.y = 4;
        }
        else if (gridNode > 19 && gridNode < 25)
        {
            gridLocation.x = (gridNode + 1) - 20;
            gridLocation.y = 5;
        }
        else if (gridNode > 24 && gridNode < 30)
        {
            gridLocation.x = (gridNode + 1) - 25;
            gridLocation.y = 6;
        }
        else if (gridNode > 29 && gridNode < 35)
        {
            gridLocation.x = (gridNode + 1) - 30;
            gridLocation.y = 7;
        }

        return gridLocation;
    }
    private bool DotIsInHomeColumn(GameObject dot)
    {
        bool result = false;
        DotStateController dotStateController = _dotService.GetDotStateController(dot);

        if (dotStateController)
        {
            // Get color of current column
            var dotColumnColor = (DotColor)dotStateController.GridLocation.x;
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
		var dots = _dotService.GetAllDots();
		// Cycle through all the grid dots
		foreach(GameObject dot in dots)
		{
			// Get the dot's controller
			DotStateController dotStateController = _dotService.GetDotStateController(dot);

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
    private void ResetScoreAndMoves()
    {
        ResetScore(0);
        ResetMoves();
    }
    private void DestroyAllDots()
    {
        var dots = _dotService.GetAllDots();

        foreach (var dot in dots)
        {
            Destroy(dot);
        }
    }

    private void LockColumn(GameObject dot)
    {
        List<GameObject> columnDots = _dotService.GetColumnForDot(dot);

		foreach (var columnDot in columnDots) 
		{
			_dotService.GetDotStateController(columnDot).IsLocked = true;
		}
    }

    private void LockRow(GameObject dot)
    {
		List<GameObject> rowDows = _dotService.GetRowForDot(dot);

		foreach (var rowDot in rowDows) 
		{
			_dotService.GetDotStateController(rowDot).IsLocked = true;
		}
        
    }

    private void LockDiagonal()
    {
        
    }

    void OnDotSwipe(SwipeDetector.SwipeDirection swipeDirection)
    {
        GameObject newDot = null;

        switch (swipeDirection)
        {
            case SwipeDetector.SwipeDirection.Up:
                newDot = _dotService.GetDotUpFromSelectedDot(SelectedDot);
                break;
            case SwipeDetector.SwipeDirection.Down:
                newDot = _dotService.GetDotDownFromSelectedDot(SelectedDot);
                break;
            case SwipeDetector.SwipeDirection.Left:
                newDot = _dotService.GetDotLeftOfSelectedDot(SelectedDot);
                break;
            case SwipeDetector.SwipeDirection.Right:
                newDot = _dotService.GetDotRightOfSelectedDot(SelectedDot);
                break;
        }

        if (newDot)
        {
            DotSelected(newDot);
        }
    }

}
