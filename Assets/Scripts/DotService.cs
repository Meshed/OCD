using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class DotService
{
    public DotService()
    {
    }

    public void SetAllDotsIdle()
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
    /// <summary>
    /// Gets the state controller component from the Game Object
    /// </summary>
    /// <returns>The dot state controller.</returns>
    /// <param name="dotGameObject">Game Object</param>
    public DotStateController GetDotStateController(GameObject dotGameObject)
    {
        DotStateController dotStateController = null;

        if (dotGameObject)
        {
            dotStateController = dotGameObject.GetComponent<DotStateController>();
        }

        return dotStateController;
    }
    /// <summary>
    /// Gets all game objects with the tag of Dot
    /// </summary>
    /// <returns>Returns a List of GameObjects</returns>
    public IEnumerable<GameObject> GetAllDots()
    {
        return GameObject.FindGameObjectsWithTag("Dot");
    }
    public GameObject GetDotUpFromSelectedDot(GameObject selectedDot)
    {
        if (!selectedDot)
        {
            return null;
        }
        DotStateController selectedDotStateController = GetDotStateController(selectedDot);

        var selectedGridLocationX = (int)selectedDotStateController.GridLocation.x;
        var selectedGridLocationY = (int)selectedDotStateController.GridLocation.y;

        var dots = GetAllDots();

        foreach (var dot in dots)
        {
            DotStateController newDotStateController = GetDotStateController(dot);
            var newGridLocationX = (int)newDotStateController.GridLocation.x;
            var newGridLocationY = (int)newDotStateController.GridLocation.y;

            if (newGridLocationX == selectedGridLocationX &&
                newGridLocationY == selectedGridLocationY - 1)
            {
                return dot;
            }
        }

        return null;
    }
    public GameObject GetDotDownFromSelectedDot(GameObject selectedDot)
    {
        if (!selectedDot)
        {
            return null;
        }
        DotStateController selectedDotStateController = GetDotStateController(selectedDot);

        var selectedGridLocationX = (int)selectedDotStateController.GridLocation.x;
        var selectedGridLocationY = (int)selectedDotStateController.GridLocation.y;

        var dots = GetAllDots();

        foreach (var dot in dots)
        {
            DotStateController newDotStateController = GetDotStateController(dot);
            var newGridLocationX = (int)newDotStateController.GridLocation.x;
            var newGridLocationY = (int)newDotStateController.GridLocation.y;

            if (newGridLocationX == selectedGridLocationX &&
                newGridLocationY == selectedGridLocationY + 1)
            {
                return dot;
            }
        }

        return null;
    }
    public GameObject GetDotLeftOfSelectedDot(GameObject selectedDot)
    {
        if (!selectedDot)
        {
            return null;
        }
        DotStateController selectedDotStateController = GetDotStateController(selectedDot);

        var selectedGridLocationX = (int)selectedDotStateController.GridLocation.x;
        var selectedGridLocationY = (int)selectedDotStateController.GridLocation.y;

        var dots = GetAllDots();

        foreach (var dot in dots)
        {
            DotStateController newDotStateController = GetDotStateController(dot);
            var newGridLocationX = (int)newDotStateController.GridLocation.x;
            var newGridLocationY = (int)newDotStateController.GridLocation.y;

            if (newGridLocationX == selectedGridLocationX - 1 &&
                newGridLocationY == selectedGridLocationY)
            {
                return dot;
            }
        }

        return null;
    }
    public GameObject GetDotRightOfSelectedDot(GameObject selectedDot)
    {
        if (!selectedDot)
        {
            return null;
        }
        DotStateController selectedDotStateController = GetDotStateController(selectedDot);

        var selectedGridLocationX = (int)selectedDotStateController.GridLocation.x;
        var selectedGridLocationY = (int)selectedDotStateController.GridLocation.y;

        var dots = GetAllDots();

        foreach (var dot in dots)
        {
            DotStateController newDotStateController = GetDotStateController(dot);
            var newGridLocationX = (int)newDotStateController.GridLocation.x;
            var newGridLocationY = (int)newDotStateController.GridLocation.y;

            if (newGridLocationX == selectedGridLocationX + 1 &&
                newGridLocationY == selectedGridLocationY)
            {
                return dot;
            }
        }

        return null;
    }
	public List<GameObject> GetColumnForDot(GameObject selectedDot)
	{
		var dotsInColumn = new List<GameObject>();
		IEnumerable<GameObject> dots = GetAllDots();
		var selectedDotX = (int)GetDotStateController(selectedDot).GridLocation.x;

		foreach (var dot in dots) 
		{
			DotStateController dotStateController = GetDotStateController(dot);

			if(dotStateController)
			{
				var dotX = (int)dotStateController.GridLocation.x;

				if(dotX == selectedDotX)
				{
					dotsInColumn.Add(dot);
				}
			}
		}

		return dotsInColumn;
	}
	public List<GameObject> GetRowForDot(GameObject selectedDot)
	{
		var dotsInRow = new List<GameObject>();
		IEnumerable<GameObject> dots = GetAllDots();
		var selectedDotY = (int)GetDotStateController(selectedDot).GridLocation.y;

		foreach(var dot in dots)
		{
			DotStateController dotStateController = GetDotStateController(dot);

			if(dotStateController)
			{
				var dotX = (int)dotStateController.GridLocation.y;

				if(dotX == selectedDotY)
				{
					dotsInRow.Add(dot);
				}
			}
		}

		return dotsInRow;
	}

    public List<GameObject> GetDiagonalDotsTopDown(GameObject selectedDot)
    {
        List<GameObject> dotList = new List<GameObject>();
        DotStateController selectedDotStateController = GetDotStateController(selectedDot);

        if (DotNotTooCloseToEdgeForDiagonalTopDown(selectedDotStateController))
        {
            IEnumerable<GameObject> dots = GetAllDots();
            int selectedDotX = (int)selectedDotStateController.GridLocation.x;
            int selectedDotY = (int)selectedDotStateController.GridLocation.y;

            foreach (var dot in dots)
            {
                DotStateController dotStateController = GetDotStateController(dot);
            }
        }

        return dotList;
    }

    private bool DotNotTooCloseToEdgeForDiagonalTopDown(DotStateController dotStateController)
    {
        bool validDot = false;

        var selectedDotX = (int)dotStateController.GridLocation.x;
        var selectedDotY = (int)dotStateController.GridLocation.y;

        if ((selectedDotX + selectedDotY) + ((5 - selectedDotX)*2) == 12 ||
            (selectedDotX + selectedDotY) + ((5 - selectedDotX)*2) == 11 ||
            (selectedDotX + selectedDotY) + ((5 - selectedDotX)*2) == 10)
        {
            validDot = true;
        }

        if (selectedDotY < selectedDotX)
        {
            validDot = false;
        }

        return validDot;
    }
}
