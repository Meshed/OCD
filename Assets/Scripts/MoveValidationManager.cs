using UnityEngine;

public class MoveValidationManager
{
    private readonly GameObject _selectedDot;
    private readonly GameStateController.GameDifficulty _gameDifficulty;
    private readonly DotService _dotService;

    public MoveValidationManager(GameObject selectedDot, GameStateController.GameDifficulty gameDifficulty)
    {
        _selectedDot = selectedDot;
        _gameDifficulty = gameDifficulty;
        _dotService = new DotService();
    }

    /// <summary>
    /// Determines if the dot selection is allowed. There are different rules for each difficulty level.
	/// 	This checks for two things, first that the selected dot is to the top, bottom, left, or right
	/// 	of the already selected dot. Then it checks to see if the dot is locked. Dots get locked after
	/// 	a valid dot selected is processed using the following rules:
	/// 
    /// 	Normal: Any dot is allowed to be selected if there is no dot selected. Only a dot next to the 
    /// 		already selected dot is allowed.
    /// 	Difficult: A dot may not be selected from a locked column
    /// 	Hard: A dot may not be selected from a locked column or row
    /// </summary>
    /// <returns><c>true</c> if this instance is dot selection allowed the specified selectedDot; otherwise, <c>false</c>.</returns>
    public bool IsDotSelectionAllowed(GameObject dot, ref GameObject selectedDot)
    {
        bool isSelectionAllowed = false;

        if (_selectedDot == null)
            isSelectionAllowed = true;
        else
        {
            bool dotLocked = _dotService.GetDotStateController(dot).IsLocked;
            bool selectedDotLocked = _dotService.GetDotStateController(selectedDot).IsLocked;

            if (!IsNewDotLeftRightUpDownFromCurrentDot(dot) ||
			    dotLocked ||
                selectedDotLocked)
                isSelectionAllowed = false;
			else
				isSelectionAllowed = true;
        }

        return isSelectionAllowed;
    }

    private bool IsNewDotLeftRightUpDownFromCurrentDot(GameObject dot)
    {
        // Use the location of the Dot passed in. Any valid Dot should be 1 above, bellow, to
        // the left, or to the right of it.
        DotStateController newDotStateController = _dotService.GetDotStateController(dot);
        DotStateController currentDotStateController = _dotService.GetDotStateController(_selectedDot);

        bool isSelectionAllowed = IsNewDotOnTopOfOrBellowCurrentDot(newDotStateController.GridLocation, currentDotStateController.GridLocation);

        if (!isSelectionAllowed) 
		{
			isSelectionAllowed = IsNewDotToLeftOfOrRightOfCurrentDot (newDotStateController.GridLocation, currentDotStateController.GridLocation);
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

        if (newDotGridLocation.x == currentDotGridLocation.x)
        {
            if ((newDotGridLocation.y == currentDotGridLocation.y + 1) ||
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

        if (newDotGridLocation.y == currentDotGridLocation.y)
        {
            if ((newDotGridLocation.x == currentDotGridLocation.x + 1) ||
               (newDotGridLocation.x == currentDotGridLocation.x - 1))
            {
                result = true;
            }
        }

        return result;
    }
}
