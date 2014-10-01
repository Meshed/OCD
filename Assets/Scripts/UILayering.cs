using UnityEngine;
using System.Collections;

public class UILayering : MonoBehaviour {
	public string SortLayeringString = "";

	void Start()
	{
		renderer.sortingLayerName = SortLayeringString;
	}
}
