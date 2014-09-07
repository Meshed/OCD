﻿using System.Collections.Generic;
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
		List<GameObject> dotsInColumn = new List<GameObject>();
		IEnumerable<GameObject> dots = GetAllDots();
		int selectedDotY = (int)GetDotStateController(selectedDot).GridLocation.y;

		foreach (var dot in dots) 
		{
			DotStateController dotStateController = GetDotStateController(dot);

			if(dotStateController)
			{
				int dotY = (int)dotStateController.GridLocation.y;
				if(dotY == selectedDotY)
				{
					dotsInColumn.Add(dot);
				}
			}
		}

		return dotsInColumn;
	}

	public List<GameObject> GetRowForDot(GameObject selectedDot)
	{
		List<GameObject> dotsInRow = new List<GameObject>();
		IEnumerable<GameObject> dots = GetAllDots();
		int selectedDotX = (int)GetDotStateController(selectedDot).GridLocation.x;

		foreach(var dot in dots)
		{
			DotStateController dotStateController = GetDotStateController(dot);

			if(dotStateController)
			{
				int dotX = (int)dotStateController.GridLocation.x;
				if(dotX == selectedDotX)
				{
					dotsInRow.Add(dot);
				}
			}
		}

		return dotsInRow;
	}
}
